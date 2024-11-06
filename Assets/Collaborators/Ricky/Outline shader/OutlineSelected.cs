using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelected : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;
    public GameObject handUI;
    public GameObject normalUI;
    public Material[] _mats;

    private MeshRenderer render;
    private bool materialsAdded;
    private bool menuInteractuable;

    public bool interactMenuVisible { get => menuInteractuable; set => menuInteractuable = value; }

    private void Start()
    {
        // Aseguramos que las interfaces de usuario estén correctamente inicializadas
        normalUI.SetActive(true);
        handUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Asegúrate de que no estés pasando el cursor sobre un UI y que hay un objeto interactuable en el raycast
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit) && !interactMenuVisible)
        {
            highlight = raycastHit.transform;

            // Si el objeto es interactuable y no es el mismo que ya estaba seleccionado
            if (highlight.CompareTag("Selectable") )
            {
                render = highlight.gameObject.GetComponent<MeshRenderer>();

                // Verificar si el MeshRenderer es válido y si aún no se han añadido los materiales
                if (render != null && !materialsAdded)
                {
                    AddOutline(); // Agregar los materiales
                }
            }
            else if (render != null && materialsAdded)
            {

                // Eliminar materiales si ya fueron añadidos
                RemoveOutline();
            }
        }
        else
        {
            RemoveOutline();
        }

    }


    private void AddOutline()
    {

        normalUI.SetActive(false);
        handUI.SetActive(true);
        // Verificar si el MeshRenderer tiene al menos 1 material (índice 0 ya tiene algo)
        if (render.materials.Length >= 1)
        {
            // Crear un nuevo array de materiales de tamaño 3
            Material[] newMaterials = new Material[3];
            newMaterials[0] = render.materials[0];
            newMaterials[1] = _mats[0]; // Primer material
            newMaterials[2] = _mats[1]; // Segundo material
            render.materials = newMaterials;
        }

        materialsAdded = true; // Marcar que los materiales fueron agregados
    }

    private void RemoveOutline()
    {
        highlight = null; // Resetear el highlight si no hay objeto bajo el puntero
        normalUI.SetActive(true);
        handUI.SetActive(false);

        if (render!=null && render.materials.Length >= 3 )
        {
            // Obtener los materiales actuales
            Material[] currentMaterials = render.materials;

            // Remover los materiales en los índices 1 y 2
            currentMaterials[1] = null; // Eliminar material en el índice 1
            currentMaterials[2] = null; // Eliminar material en el índice 2

            // Asignar el array modificado de vuelta al MeshRenderer
            render.materials = currentMaterials;

            materialsAdded = false; // Marcar que los materiales han sido eliminados
        }
    }

}
