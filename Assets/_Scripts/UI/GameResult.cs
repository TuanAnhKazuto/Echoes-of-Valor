using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject panelLost;
    public GameObject panelVictory;

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
    }

    public void ShowFailPanel()
    {
        if (panelLost != null)
            panelLost.SetActive(true);
    }

    public void ShowVictoryPanel()
    {
        if (panelVictory != null)
            panelVictory.SetActive(true);
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
