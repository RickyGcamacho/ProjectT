using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDistance : MonoBehaviour
{
    public Transform otherObject;
    public float maxZDistance = 0.5f;

    public delegate void DistanceExceeded();
    public event DistanceExceeded OnDistanceExceeded;

    void Update()
    {
        float zDistance = Mathf.Abs(transform.position.z - otherObject.position.z);

        if (zDistance > maxZDistance)
        {
            OnDistanceExceeded?.Invoke();
            // Opcional: corregir posición
            transform.position = new Vector3(transform.position.x, transform.position.y, otherObject.position.z + Mathf.Sign(transform.position.z - otherObject.position.z) * maxZDistance);
        }
    }
}