using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats playerStats;
    public PlayerData curData;
    public WeaponStats[] equippedWeapons;
    public int selectedId;
    public bool isCharacterSpawned = false;

    public int loadPlayerId;

    private void Start()
    {
        selectedId = PlayerPrefs.GetInt("SelectedPlayerId");

        curData = SaveSystem.LoadGame(selectedId);        
    }

    private void Update()
    {
        if (isCharacterSpawned)
        {
            if (!isCharacterSpawned) return;
            Invoke(nameof(Load), 0.5f);
            return;
        }
    }

    private void Load()
    {
        playerStats = FindAnyObjectByType<CharacterStats>();
        equippedWeapons = playerStats.equippedWeapons;

        LoadPosition();
        LoadStats();
        LoadWeapons();

        isCharacterSpawned = false;
    }

    private void LoadWeapons()
    {
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            if (curData.weapons != null && i < curData.weapons.Length)
            {
                WeaponData data = curData.weapons[i];

                equippedWeapons[i].weaponID = data.weaponID;
                equippedWeapons[i].weaponName = data.weaponName;
                equippedWeapons[i].weaponLevel = data.weaponLevel;
                equippedWeapons[i].maxWeaponLevel = data.maxWeaponLevel;
                equippedWeapons[i].weaponBreakthrough = data.breakthroughLevel;

                equippedWeapons[i].baseDamage = data.curDamage;
                equippedWeapons[i].damagePerLevel = data.damagePerLevel;
                equippedWeapons[i].damagePerBreakthrough = data.damagePerBreakthrough;

                equippedWeapons[i].baseDefense = data.curDefense;
                equippedWeapons[i].defensePerLevel = data.defensePerLevel;
                equippedWeapons[i].defensePerBreakthrough = data.defensePerBreakthrough;
            }
        }
    }

    public void LoadStats()
    {
        if (curData == null) return;
        playerStats.playerId = curData.playerId;
        playerStats.playerName = curData.playerName;
        playerStats.characterClass = curData.characterClass;
        playerStats.level = curData.level;
        playerStats.maxHealth = curData.health; 
        playerStats.baseDefense = (int)curData.defense;
        playerStats.baseDamage = (int)curData.damage;
        // Load other stats if needed
        // e.g., player.exp = curData.exp; etc.
        LoadPosition();
    }

    private void LoadPosition()
    {
        playerStats.transform.SetPositionAndRotation(new Vector3
            (
                curData.positionX,
                curData.positionY,
                curData.positionZ
            ), Quaternion.Euler(0, curData.rotationY, 0));
    }

    public void SaveGame()
    {
        curData.playerId = playerStats.playerId;
        curData.playerName = playerStats.playerName;
        curData.characterClass = playerStats.characterClass;
        curData.level = playerStats.level;

        curData.health = playerStats.currentHealth;
        curData.defense = playerStats.TotalDefense;
        curData.damage = playerStats.TotalDamage;


        curData.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        curData.positionX = playerStats.transform.position.x;
        curData.positionY = playerStats.transform.position.y;
        curData.positionZ = playerStats.transform.position.z;
        curData.rotationY = playerStats.transform.rotation.eulerAngles.y;

        SaveSystem.SaveGame(curData);
    }
}
