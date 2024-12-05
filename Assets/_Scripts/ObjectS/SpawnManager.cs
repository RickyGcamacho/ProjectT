using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnableObject> spawnableObjects; // Lista de objetos que pueden spawnear
    public List<Transform> spawnPoints;            // Lista de puntos de spawn (Transforms)

    private Dictionary<GameObject, int> activeObjectCounts = new Dictionary<GameObject, int>(); // Conteo de objetos activos
    private HashSet<GameObject> spawnedOnceObjects = new HashSet<GameObject>(); // Objetos que solo se spawnean una vez
    private HashSet<Transform> occupiedSpawnPoints = new HashSet<Transform>(); // Para hacer seguimiento de puntos ocupados

    void Start()
    {
        // Inicializar los objetos al inicio
        foreach (var spawnableObject in spawnableObjects)
        {
            if (!spawnableObject.spawnOnlyOnce)
            {
                EnsureObjectsInScene(spawnableObject);
            }
            else
            {
                SpawnObject(spawnableObject); // Spawn de los objetos que solo deben aparecer una vez al inicio
            }
        }
    }

    void Update()
    {
        // Asegurarse de que los objetos que se pueden volver a generar se mantengan en la escena
        foreach (var spawnableObject in spawnableObjects)
        {
            if (!spawnableObject.spawnOnlyOnce)
            {
                EnsureObjectsInScene(spawnableObject);
            }
        }
    }

    void EnsureObjectsInScene(SpawnableObject spawnableObject)
    {
        if (activeObjectCounts.ContainsKey(spawnableObject.prefab))
        {
            if (activeObjectCounts[spawnableObject.prefab] < spawnableObject.maxObjectsOnScreen)
            {
                SpawnObject(spawnableObject);
            }
        }
        else
        {
            activeObjectCounts[spawnableObject.prefab] = 0;
        }
    }

    public void SpawnObject(SpawnableObject spawnableObject)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available. Please add spawn points in the Inspector.");
            return;
        }

        Transform selectedSpawnPoint = null;

        // Intentar encontrar un punto de spawn no ocupado
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint != null && !occupiedSpawnPoints.Contains(spawnPoint))
            {
                selectedSpawnPoint = spawnPoint;
                break; // Salir del bucle si se encuentra un punto libre
            }
        }

        if (selectedSpawnPoint == null)
        {
            Debug.LogWarning("No available spawn points found.");
            return;
        }

        // Marcar el punto como ocupado
        occupiedSpawnPoints.Add(selectedSpawnPoint);

        // Instanciar el objeto
        Quaternion grade = Quaternion.Euler(-90f, transform.rotation.y, transform.rotation.z);
        GameObject newObj = Instantiate(spawnableObject.prefab, selectedSpawnPoint.position, grade);

        // Asegurar el conteo en el diccionario
        if (!activeObjectCounts.ContainsKey(spawnableObject.prefab))
        {
            activeObjectCounts[spawnableObject.prefab] = 0;
        }
        activeObjectCounts[spawnableObject.prefab]++;

        // Suscribir al evento OnPickedUp
        var interactable = newObj.GetComponent<ItemObject>();
        if (interactable != null)
        {
            interactable.OnPickedUp += () =>
            {
                Debug.Log($"OnPickedUp triggered for {newObj.name}");
                HandleObjectPickedUp(newObj, spawnableObject, selectedSpawnPoint);
            };
        }
    }

    void HandleObjectPickedUp(GameObject pickedObject, SpawnableObject spawnableObject, Transform spawnPoint)
    {
        Debug.Log($"HandleObjectPickedUp called for {pickedObject.name}");

        if (occupiedSpawnPoints.Contains(spawnPoint))
        {
            occupiedSpawnPoints.Remove(spawnPoint);
            Debug.Log($"Spawn point {spawnPoint.name} is now free.");
        }

        if (activeObjectCounts.ContainsKey(spawnableObject.prefab))
        {
            activeObjectCounts[spawnableObject.prefab]--;
        }

        Destroy(pickedObject);
        Debug.Log($"{pickedObject.name} destroyed.");

        if (!spawnableObject.spawnOnlyOnce && activeObjectCounts[spawnableObject.prefab] < spawnableObject.maxObjectsOnScreen)
        {
            SpawnObject(spawnableObject);
        }
    }
}


