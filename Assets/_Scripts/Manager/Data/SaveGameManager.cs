using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats playerStats;
    public PlayerData curData;

    //public WeaponData[] weaponsData;
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

        playerStats.equippedWeapons[0].RankUpdateControl();
        playerStats.equippedWeapons[1].RankUpdateControl();

        playerStats.healthBar.UpdateHealth((int)playerStats.currentHealth, (int)playerStats.maxHealth);

        isCharacterSpawned = false;
    }

    private void LoadWeapons()
    {
        if (curData.weapons == null || curData.weapons.Length < equippedWeapons.Length)
        {
            Debug.LogWarning("Save file không có đủ thông tin vũ khí. Khởi tạo mặc định.");
            curData.weapons = new WeaponData[equippedWeapons.Length];

            for (int i = 0; i < equippedWeapons.Length; i++)
            {
                curData.weapons[i] = new WeaponData
                {
                    weaponID = "weapon_" + i,
                    weaponName = "DefaultWeapon",
                    weaponLevel = 1,
                    maxWeaponLevel = 60,
                    breakthroughLevel = 1,
                    curDamage = 10,
                    damagePerLevel = 2,
                    damagePerBreakthrough = 5,
                    curDefense = 0,
                    defensePerLevel = 0,
                    defensePerBreakthrough = 0
                };
            }

            SaveGame(); 
        }

        for (int i = 0; i < equippedWeapons.Length; i++)
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


    public void LoadStats()
    {
        if (curData == null) return;
        playerStats.playerId = curData.playerId;
        playerStats.playerName = curData.playerName;
        playerStats.healthBar.nameText.text = curData.playerName;
        playerStats.characterClass = curData.characterClass;
        playerStats.level = curData.level;
        playerStats.currentHealth = curData.health; 
        playerStats.baseDefense = (int)curData.defense;
        playerStats.baseDamage = (int)curData.damage;
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

        SaveWeapons();

        SaveSystem.SaveGame(curData);
    }

    public void SaveWeapons()
    {
        curData.weapons = new WeaponData[equippedWeapons.Length];

        for (int i = 0; i < equippedWeapons.Length; i++)
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
}
