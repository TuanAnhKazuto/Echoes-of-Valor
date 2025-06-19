using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats playerStats;
    public PlayerData curData;
    public WeaponData[] weaponsData;

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
                weaponsData[i] = curData.weapons[i];

                equippedWeapons[i].weaponID = weaponsData[i].weaponID;
                equippedWeapons[i].weaponName = weaponsData[i].weaponName;
                equippedWeapons[i].weaponLevel = weaponsData[i].weaponLevel;
                equippedWeapons[i].maxWeaponLevel = weaponsData[i].maxWeaponLevel;
                equippedWeapons[i].weaponBreakthrough = weaponsData[i].breakthroughLevel;

                equippedWeapons[i].baseDamage = weaponsData[i].curDamage;
                equippedWeapons[i].damagePerLevel = weaponsData[i].damagePerLevel;
                equippedWeapons[i].damagePerBreakthrough = weaponsData[i].damagePerBreakthrough;

                equippedWeapons[i].baseDefense = weaponsData[i].curDefense;
                equippedWeapons[i].defensePerLevel = weaponsData[i].defensePerLevel;
                equippedWeapons[i].defensePerBreakthrough = weaponsData[i].defensePerBreakthrough;
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

        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            if (i < curData.weapons.Length)
            {
                curData.weapons[i] = new WeaponData
                {
                    weaponID = equippedWeapons[i].weaponID,
                    weaponName = equippedWeapons[i].weaponName,
                    weaponLevel = equippedWeapons[i].weaponLevel,
                    maxWeaponLevel = equippedWeapons[i].maxWeaponLevel,
                    breakthroughLevel = equippedWeapons[i].weaponBreakthrough,
                    curDamage = equippedWeapons[i].baseDamage,
                    damagePerLevel = equippedWeapons[i].damagePerLevel,
                    damagePerBreakthrough = equippedWeapons[i].damagePerBreakthrough,
                    curDefense = equippedWeapons[i].baseDefense,
                    defensePerLevel = equippedWeapons[i].defensePerLevel,
                    defensePerBreakthrough = equippedWeapons[i].defensePerBreakthrough
                };
            }
        }

        SaveSystem.SaveGame(curData);
    }
}
