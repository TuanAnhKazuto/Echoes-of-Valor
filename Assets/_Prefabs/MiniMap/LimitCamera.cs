using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LimitCamera : MonoBehaviour
{
    public GameObject Player;
    public SaveGameManager saveGameManager;

    private void Update()
    {
        Invoke(nameof(GetPlayerObj), 0.2f);
    }

    private void GetPlayerObj()
    {
        if (saveGameManager.isCharacterSpawned)
        {
            Player = FindAnyObjectByType<CharacterStats>().gameObject;
        }
    }
}
