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

    public GameObject adminPanel;
    bool isAdminPanelOpen = false;

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
            continueButton.onClick.AddListener(OnContinue);
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

        OpenAdminPanel();
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
        {
            //if (panelLost != null) panelLost.SetActive(false);
            //if (panelVictory != null) panelVictory.SetActive(false);

            //if (characterStats == null && saveGameManager != null)
            //{
            //    characterStats = saveGameManager.playerStats;
            //}

            //if (characterStats != null)
            //{
            //    var agent = characterStats.GetComponent<UnityEngine.AI.NavMeshAgent>();
            //    if (agent != null) agent.enabled = false;

            //    characterStats.gameObject.SetActive(false);

            //    if (respawnPoint != null)
            //        characterStats.transform.position = respawnPoint.position;

            //    characterStats.gameObject.SetActive(true);

            //    if (agent != null) agent.enabled = true;

            //    characterStats.currentHealth = characterStats.maxHealth;
            //    characterStats.currentMana = characterStats.maxMana;

            //    characterStats.healthBar.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);
            //    characterStats.manaBar.UpdateMana(characterStats.currentMana, characterStats.maxMana);

            //    characterStats.animator.SetFloat("HP", characterStats.currentHealth);
            //    characterStats.isDied = false;
            //    characterStats.CursorTarget.SetActive(true);
            //}
        }
        FindAnyObjectByType<UpgradeSystem>().ResetUpgrades();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UpdateTimeScale();
    }
    private void OnSceneLoadedReplay(Scene scene, LoadSceneMode mode)
    {
        var upgradeSystem = FindAnyObjectByType<UpgradeSystem>();
        if (upgradeSystem != null)
            upgradeSystem.ResetUpgrades();

        SceneManager.sceneLoaded -= OnSceneLoadedReplay;
    }

    public void OnPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (panelPause != null)
            panelPause.SetActive(isPaused);

        UpdateTimeScale();
    }

    public void OnContinue()
    {
        if (panelPause != null && panelPause.activeSelf)
        {
            isPaused = false;
            panelPause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UpdateTimeScale();
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
        FindAnyObjectByType<UpgradeSystem>().ResetUpgrades();
    }

    public void OpenAdminPanel()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (!isAdminPanelOpen)
            {
                adminPanel.SetActive(true);
                isAdminPanelOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                adminPanel.SetActive(false);
                isAdminPanelOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}