using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject prefab;
    public bool spawnOnlyOnce = false;
    public int maxObjectsOnScreen = 1;

    [Tooltip("Rotación en grados al instanciar")]
    public Vector3 rotation = Vector3.zero;

    [Tooltip("Prioridad para aparecer. Más alto = mayor prioridad.")]
    public int priority = 0;

    [Tooltip("Tiempo de espera en segundos para respawn después de recoger")]
    public float respawnDelay = 0f;
}