using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelected : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;
    public GameObject handUI;
    public GameObject normalUI;

    private bool outlineAdded;
    private bool menuInteractuable;
    private Outline outline;
    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 2f;

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
        ray.origin += ray.direction * 2;

        // Asegúrate de que no estés pasando el cursor sobre un UI y que hay un objeto interactuable en el raycast
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit) && !interactMenuVisible)
        {
            highlight = raycastHit.transform;

            // Si el objeto es interactuable y no es el mismo que ya estaba seleccionado
            if (highlight.CompareTag("Selectable") || highlight.CompareTag("Pickup"))
            {
                normalUI.SetActive(false);
                handUI.SetActive(true);
                if (highlight.CompareTag("Selectable"))
                {
                    if (!outlineAdded) // Si aún no se ha agregado el outline, agregarlo
                    {
                        AddOutline();
                    }
                }
        
            }
            else
            {
                normalUI.SetActive(true);
                handUI.SetActive(false);

                if (outlineAdded) // Si el outline fue añadido pero no hay objeto seleccionable, eliminarlo
                {
                    RemoveOutline();
                    normalUI.SetActive(true);
                    handUI.SetActive(false);
                }

            }
        }
        else
        {
            normalUI.SetActive(true);
            handUI.SetActive(false);
        }

    }

    private void AddOutline()
    {
        if (highlight != null)
        {
            if (!highlight.gameObject.TryGetComponent(out outline))
            {
                outline = highlight.gameObject.AddComponent<Outline>(); // Añadir el Outline si no existe
            }
            if (outline != null)
            {
                outline = highlight.gameObject.GetComponent<Outline>();
                outline.OutlineWidth = outlineWidth;
                outline.enabled = true; // Habilitar el Outline
                outlineAdded = true;
            }

        }

   
    }

    private void RemoveOutline()
    {
        highlight = null;
        // Si el Outline existe, deshabilitarlo
        if (outline != null)
        {
            outline.enabled = false;
        }
        outlineAdded = false;
    }
}