using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public event Action OnDialogueEnded;

    [SerializeField] private GameObject dialogueUI; // Panel del diálogo
    [SerializeField] private Image characterIcon;
    [SerializeField] private TextMeshProUGUI characterName, dialogueArea;
    [SerializeField] private float typingSpeed = 0.2f; // Velocidad mejorada
    

    private Queue<DialogueLine> lines;
    private bool isTyping = false;

    public GameObject DialogueUI { get => dialogueUI; set => dialogueUI = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    private void Start()
    {
        lines = new Queue<DialogueLine>();
        DialogueUI.SetActive(false); // Ocultar el cuadro al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // O puedes usar un botón de UI
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueUI.SetActive(true);
        
        lines.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            lines.Enqueue(line);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (isTyping) return; // Evita que el jugador salte el tipeo

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    private void EndDialogue()
    {
        DialogueUI.SetActive(false);
        OnDialogueEnded?.Invoke(); // Notificar a otros scripts que terminó el diálogo
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
{
    dialogueArea.text = ""; // Borrar cualquier texto previo
    string sentence = dialogueLine.line;
    int maxCharactersPerPage = 101; // Ajusta según tu UI

    string currentText = ""; // Mantener texto actual que se está mostrando

    // Itera por cada carácter del diálogo
    for (int i = 0; i < sentence.Length; i++)
    {
        currentText += sentence[i]; // Agregar cada carácter

        dialogueArea.text = currentText; // Actualizar el texto en la UI

        yield return new WaitForSeconds(typingSpeed); // Efecto de tipeo

        // Si alcanzamos el límite de caracteres por página
        if (currentText.Length >= maxCharactersPerPage)
        {
                dialogueArea.text = currentText + "\n\nContinue....";
                
                // Esperar a que el jugador presione `Espacio`
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));

            // Limpiar para la siguiente parte
            currentText = ""; 
        }
    }

    // Esperar antes de finalizar
  yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
}
}