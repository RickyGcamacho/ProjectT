using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogues;
    [SerializeField] private bool[] requiereLuzEncendida; // Debe tener el mismo largo que dialogues
    private int currentDialogueIndex = 0;
    private float timeBetweenCalls = 10f;
    private bool isPlayerInside = false;
    private bool isDialogueRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialogueRunning && currentDialogueIndex < dialogues.Length)
        {
            isPlayerInside = true;

            // Verificar que la condición de luz esté OK (si usás la versión con requiereLuzEncendida)
            if (requiereLuzEncendida.Length > currentDialogueIndex && requiereLuzEncendida[currentDialogueIndex])
            {
                if (!GameManager.Instance.LucesEncendidas)
                    return;

            }
            StartNextDialogue();
          
        }
    }

    private void StartNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            // Chequear si necesita que la luz esté encendida
            if (requiereLuzEncendida.Length > currentDialogueIndex && requiereLuzEncendida[currentDialogueIndex])
            {
                if (!GameManager.Instance.LucesEncendidas)
                    return; // Salir si no se cumple la condición
            }

            isDialogueRunning = true;
            DialogueManager.Instance.StartDialogue(dialogues[currentDialogueIndex]);
            StartCoroutine(WaitBetweenCalls());
        }
    }

    private void HandleDialogueEnd()
    {
        DialogueManager.Instance.OnDialogueEnded -= HandleDialogueEnd;
        isDialogueRunning = false;
        currentDialogueIndex++;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    IEnumerator WaitBetweenCalls()
    {
        DialogueManager.Instance.OnDialogueEnded += HandleDialogueEnd;
        Debug.Log("LLamada entrante");
        yield return new WaitForSeconds(timeBetweenCalls);
    }
}