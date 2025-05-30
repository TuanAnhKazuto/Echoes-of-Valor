using UnityEngine;
using System.IO;
public static class SaveSystem
{
    static string saveFilePath = Path.Combine(Application.persistentDataPath, "gameSave.json");

    public static void SaveGame(PlayerData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public static PlayerData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData gameData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game loaded from " + saveFilePath);
            return gameData;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + saveFilePath);
            return new PlayerData() ;
        }
    }
}