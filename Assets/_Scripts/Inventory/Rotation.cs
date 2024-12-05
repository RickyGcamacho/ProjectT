using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x,speed * Time.deltaTime,transform.rotation.z);
    }
}
