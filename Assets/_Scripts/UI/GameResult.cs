using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject panelLost;
    public GameObject panelVictory;
    public GameObject panelPause;
    public Button continueButton;

    private bool isPaused = false;

    [Header("Player Settings")]
    public GameObject player;
    private Vector3 startPosition;
    private CharacterStats characterStats;

    private void Start()
    {
        if (player != null)
        {
            startPosition = player.transform.position;
            characterStats = player.GetComponent<CharacterStats>();
        }

        if (panelPause != null)
            panelPause.SetActive(false);

        if (continueButton == null && panelPause != null)
        {
            Transform found = panelPause.transform.Find("Button Continue");
            if (found != null)
                continueButton = found.GetComponent<Button>();
        }

        if (continueButton != null)
            continueButton.onClick.AddListener(OnPause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ShowFailPanel();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ShowVictoryPanel();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnPause();
        }
    }
    private void UpdateTimeScale()
    {
        bool anyPanelActive =
            (panelPause != null && panelPause.activeSelf) ||
            (panelLost != null && panelLost.activeSelf) ||
            (panelVictory != null && panelVictory.activeSelf);

        Time.timeScale = anyPanelActive ? 0f : 1f;
    }


    public void ShowFailPanel()
    {
        if (panelLost != null)
            panelLost.SetActive(true);

        UpdateTimeScale();
    }

    public void ShowVictoryPanel()
    {
        if (panelVictory != null)
            panelVictory.SetActive(true);

        UpdateTimeScale();
    }

    public void OnReplay()
    {
        if (panelLost != null) panelLost.SetActive(false);
        if (panelVictory != null) panelVictory.SetActive(false);

        if (player != null)
        {
            player.transform.position = startPosition;

            if (characterStats != null)
            {
                characterStats.currentHealth = characterStats.maxHealth;
                characterStats.currentMana = characterStats.maxMana;

                characterStats.healthBar.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);
            }
        }
        UpdateTimeScale();
    }

    public void OnPause()
    {
        isPaused = !isPaused;

        if (panelPause != null)
            panelPause.SetActive(isPaused);

        UpdateTimeScale();
    }

    public void OnNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
}
