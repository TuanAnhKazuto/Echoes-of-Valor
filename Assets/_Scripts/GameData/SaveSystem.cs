using UnityEngine;
using System.IO;
public static class SaveSystem
{
    static string GetSavePath(int playerId)
    {
        return Path.Combine(Application.persistentDataPath, $"playerId_{playerId}.json");
    }

    public static void SaveGame(PlayerData gameData)
    {
        string path = GetSavePath(gameData.playerId);
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
        Debug.Log($"Game saved to {path}");
    }

    public static PlayerData LoadGame(int playerId)
    {
        string path = GetSavePath(playerId);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData gameData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log($"Game loaded from {path}");
            return gameData;
        }

        Debug.LogWarning($"No save file found for player ID {playerId}");
        return null;
    }
}