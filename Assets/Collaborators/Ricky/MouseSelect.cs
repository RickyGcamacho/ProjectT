using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_MouseSelect : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScale = -1.03f;
    [SerializeField] private Color outlineColor = Color.blue;
    [Range(0.0f, 2.0f)][SerializeField] private float RotateSpeed = 0.2f;
    [Range(-1.0f, 1.0f)][SerializeField] private float RotateDirection = 1.0f;
    private Renderer outlineRenderer;

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScale, outlineColor);
    }

    Renderer CreateOutline(Material m, float s, Color c)
    {
        
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);

        outlineObject.name = this.gameObject.name + "_Outline";

        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = m;
        rend.material.SetColor("_OutlineColor", c);
        rend.material.SetFloat("_OutlineScale", s);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        
        outlineObject.GetComponent<Outline_MouseSelect>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        rend.enabled = false;
        return rend;
    }

    private void OnMouseEnter()
    {
        outlineRenderer.enabled = true;
    }
    private void OnMouseOver()
    {
        if (RotateDirection > 0)
            transform.Rotate(Vector3.up, RotateSpeed, Space.World);
        else
            transform.Rotate(Vector3.down, RotateSpeed, Space.World);

        outlineRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        outlineRenderer.enabled = false;
    }
}