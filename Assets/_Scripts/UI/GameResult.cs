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
    public SaveGameManager saveGameManager;
    private Vector3 startPosition;
    public CharacterStats characterStats;
    public Transform respawnPoint;

    private void Start()
    {
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
        if (saveGameManager.isCharacterSpawned)
        {
            if (characterStats == null)
                characterStats = saveGameManager.playerStats;
            else
                return;
        }

        if (Input.GetKeyDown(KeyCode.P))
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateTimeScale();
    }

    public void ShowVictoryPanel()
    {
        if (panelVictory != null)
            panelVictory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateTimeScale();
    }

    public void OnReplay()
    {
        if (panelLost != null) panelLost.SetActive(false);
        if (panelVictory != null) panelVictory.SetActive(false);

        if (characterStats == null && saveGameManager != null)
        {
            characterStats = saveGameManager.playerStats;
        }

        if (characterStats != null)
        {
            // Tắt NavMeshAgent (nếu có) để tránh override vị trí
            var agent = characterStats.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.enabled = false;

            // Tắt GameObject trước khi thay đổi vị trí
            characterStats.gameObject.SetActive(false);

            // Đặt lại vị trí
            if (respawnPoint != null)
                characterStats.transform.position = respawnPoint.position;

            // Bật lại GameObject
            characterStats.gameObject.SetActive(true);

            // Bật lại NavMeshAgent
            if (agent != null) agent.enabled = true;

            // Reset máu và mana
            characterStats.currentHealth = characterStats.maxHealth;
            characterStats.currentMana = characterStats.maxMana;

            characterStats.healthBar.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);
            characterStats.manaBar.UpdateMana(characterStats.currentMana, characterStats.maxMana);

            characterStats.animator.SetFloat("HP", characterStats.currentHealth);
            characterStats.isDied = false;
            characterStats.CursorTarget.SetActive(true);
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
