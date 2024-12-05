using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject prefab;                // Prefab del objeto
    public float spawnChance;                // Probabilidad de aparición (0-1)
    public int maxObjectsOnScreen;       // Número máximo de objetos que pueden aparecer al mismo tiempo
    public bool spawnOnlyOnce = false;       // Si el objeto solo debe aparecer una vez
}