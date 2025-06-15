using System.IO;
using UnityEngine;

public static class PlayerIdGenerator
{
    static string directoryPath = Application.persistentDataPath;

    public static int GetNextAvailableId()
    {
        int id = 101;
        while (File.Exists(Path.Combine(directoryPath, $"playerId_{id}.json")))
        {
            id++;
        }
        return id;
    }
}
