using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> luces = new List<GameObject>();


    private float timeLight;
    private string objectoNombre = "Light";
    private bool lucesEncendidas = true;
    private bool yaSeCortoLaLuz = false;
    private bool yaVolvioLaLuz = false;

    public bool LucesEncendidas { get => lucesEncendidas; set => lucesEncendidas = value; }
    public float TimeLight { get => timeLight; set => timeLight = value; }
    public bool YaVolvioLaLuz => yaVolvioLaLuz;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        TimeLight = 30f;
    }

    private void Start()
    {
        StartCoroutine(LightCycle());
    }

    void Update()
    {
        foreach (GameObject luz in luces)
        {
            luz.SetActive(lucesEncendidas);
        }

    }

    IEnumerator LightCycle()
    {
        // Espera y corta la luz
        yield return new WaitForSeconds(TimeLight);
        LucesEncendidas = false;
        yaSeCortoLaLuz = true;
        Debug.Log("💡 Se cortó la luz");
    }
}