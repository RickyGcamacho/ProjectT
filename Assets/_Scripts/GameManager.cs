using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject luces;

    private float timeLight;
    private bool lucesEncendidas = true;

    public bool LucesEncendidas { get => lucesEncendidas; set => lucesEncendidas = value; }
    public float TimeLight { get => timeLight; set => timeLight = value; }

    private void Awake()
    {
        TimeLight = 10f;
    }
    // Update is called once per frame
    void Update()
    {
        if (LucesEncendidas == true)
        {
            luces.SetActive(true);
        }
        else
        {
            luces.SetActive(false);
        }
        Debug.Log(LucesEncendidas);
        StartCoroutine(LightOff());
    }

    IEnumerator LightOff()
    {
        if (LucesEncendidas)
        {
            yield return new WaitForSeconds(TimeLight);
            LucesEncendidas = false;
            
        }
    }

}
