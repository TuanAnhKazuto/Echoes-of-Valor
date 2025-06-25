using System;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5);
    }
}
