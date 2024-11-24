using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public GameObject dialogo;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
    private PlayerActions playerActions;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

   // public Animator animator;

    private void Awake()
    {
     //   playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        dialogo.SetActive(false);
        if (Instance == null)
            Instance = this;
      
        lines = new Queue<DialogueLine>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextDialogueLine();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {  
        dialogo.SetActive(true);
        isDialogueActive = true;
      //  playerActions.GetComponent<Rigidbody>().isKinematic = true;
       // Cursor.visible = true;
       // Cursor.lockState = CursorLockMode.Confined;


        // animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
       
            DialogueLine currentLine = lines.Dequeue();

            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;

            //StopAllCoroutines();

            StartCoroutine(TypeSentence(currentLine));
        
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogo.SetActive(false);
      //  playerActions.GetComponent<Rigidbody>().isKinematic = false;
        //  Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
        //  Cursor.visible = false; // Asegúrate de que sea visible.
        //  animator.Play("hide");
    }
}