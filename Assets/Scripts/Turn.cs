using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public float rotationSpeed = 5.0f;  // Velocidad de rotación
    private float mouseX, mouseY;

    private void Update()
    {
        TurnInspection();
    }
    // Start is called before the first frame update
    public void TurnInspection()
    {

        if (Input.GetMouseButtonDown(0))  // Si se mantiene presionado el botón izquierdo del mouse
        {
            mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Aplica la rotación en los ejes X y Y del objeto
            transform.Rotate(Vector3.up, -mouseX, Space.World);   // Rota en el eje Y (izquierda/derecha)
            transform.Rotate(Vector3.right, mouseY, Space.World); // Rota en el eje X (arriba/abajo)
        }
    }
}

