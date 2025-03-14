using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turn : MonoBehaviour
{
    public bool inInventory;
    private ItemCarousel carrousel;

    private float speed = 50f, rotateSpeed = 10f, zoomSpeed = 0.2f, minScale = 0.5f, maxScale = 2.0f;
    private Vector3 initialScale;
    private bool isBeingInspected = false;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        carrousel = FindObjectOfType<ItemCarousel>();
    }

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (inInventory)
        {
            transform.Rotate(Vector3.up * Speed * Time.deltaTime, Space.World);
        }

        if (isBeingInspected && carrousel != null && carrousel.Inspection)
        {
            if (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                OnDrag();
            }
        }
    }

    private void OnDrag()
    {
        float rotationX = Input.GetAxis("Mouse X") * rotateSpeed;
        float rotationY = Input.GetAxis("Mouse Y") * rotateSpeed;
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.Rotate(Vector3.up * rotationX, Space.World);
        transform.Rotate(Vector3.right * -rotationY, Space.World);

        Vector3 newScale = transform.localScale + Vector3.one * scroll * zoomSpeed;
        newScale = Vector3.Max(initialScale * minScale, Vector3.Min(initialScale * maxScale, newScale));
        transform.localScale = newScale;
    }

    public void SetInspectionState(bool state)
    {
        isBeingInspected = state;

        if (!state)
        {
            transform.localScale = initialScale;  // Restaurar la escala si deja de inspeccionarse
        }
    }
}
