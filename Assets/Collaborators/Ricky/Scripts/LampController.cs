using UnityEngine;
using TMPro;

public class LampController : MonoBehaviour
{
    public Light lampLight; // Referencia a la luz de la lámpara
    public TextMeshProUGUI interactionText; // Referencia al texto en pantalla
    public float interactionDistance = 3.0f; // Distancia para interactuar
    public float viewAngleThreshold = 60f; // Ángulo máximo para que el jugador esté mirando a la lámpara

    private Transform player;
    private bool isPlayerNear;

    void Start()
    {
        // Obtener al jugador en la escena (asume que tiene la etiqueta "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        
    }

    void Update()
    {

        // Calcular la distancia entre el jugador y la lámpara
        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNear = distance <= interactionDistance;

        // Si el jugador está cerca, verifica si la lámpara está en su línea de visión
        if (isPlayerNear && IsLampInView())
        {
            print("funciono pito pitoooooo culo");
            
            interactionText.text = lampLight.enabled ? "Presiona 'E' para apagar" : "Presiona 'E' para encender";

            // Si el jugador presiona 'E', cambiar el estado de la luz
            if (Input.GetKeyDown(KeyCode.E))
            {
                lampLight.enabled = !lampLight.enabled;
            }
        }
        else
        {
            interactionText.text = "";
        }
    }

    // Método para verificar si la lámpara está en la línea de visión del jugador
    bool IsLampInView()
    {
        Vector3 directionToLamp = (transform.position - player.position).normalized;
        float angleToLamp = Vector3.Angle(player.forward, directionToLamp);

        // Verificar si la lámpara está dentro del campo de visión definido por el viewAngleThreshold
        if (angleToLamp <= viewAngleThreshold / 2)
        {
            // Realizar un Raycast para verificar que no haya obstrucciones
            RaycastHit hit;
            if (Physics.Raycast(player.position, directionToLamp, out hit, interactionDistance))
            {
                // Retorna true solo si el objeto golpeado por el raycast es la lámpara
                print(hit.transform == transform);
                return hit.transform == transform;
            }
        }

        return false;
    }
}
