using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveScanner : MonoBehaviour
{
    public static List<PlayerData> LoadAllSaves()
    {
        string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path, "playerId_*.json");

        List<PlayerData> saves = new List<PlayerData>();

        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            saves.Add(data);
        }

        return saves;
    }
}
