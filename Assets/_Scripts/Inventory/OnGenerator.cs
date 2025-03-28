using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGenerator : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Generador")
        {
            StartCoroutine(LightOn());
            Destroy(gameObject);

        }
    }

    IEnumerator LightOn()
    {
        if (!gameManager.LucesEncendidas)
        {
            gameManager.TimeLight = 10f;
            gameManager.LucesEncendidas = true;
            yield return new WaitForSeconds(gameManager.TimeLight);

        }
    }
}
