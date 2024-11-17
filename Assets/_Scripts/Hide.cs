using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool enter; 
    
    [SerializeField]
    private Transform dentro, fuera;
    [SerializeField]
    private float time;

    private Transform playerT;
    private GameObject player;
    private bool exit;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (enter == true)
        {
            playerT.position = Vector3.Lerp(playerT.position, dentro.position, time * Time.deltaTime);
            playerT.rotation = Quaternion.Lerp(playerT.rotation, dentro.rotation, time * Time.deltaTime);
            player.GetComponent<Rigidbody>().isKinematic = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                enter = false;
                exit = true;
     
                
            }
        }
        if (exit == true)
        {
            playerT.position = Vector3.Lerp(playerT.position, fuera.position, time * Time.deltaTime);
            playerT.rotation = Quaternion.Lerp(playerT.rotation, fuera.rotation, time * Time.deltaTime);
            StartCoroutine(FinEscondite());
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    IEnumerator FinEscondite()
    {
        yield return new WaitForSeconds(2);
        exit = false;
    }


}
