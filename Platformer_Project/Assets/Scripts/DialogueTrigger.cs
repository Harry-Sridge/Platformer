using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    private void Start()
    {
        dialogueManager = GameObject.Find("Player").GetComponent<DialogueManager>();
    }

    public void TriggerDialogue ()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    public void ExitDialogue ()
    {
        dialogueManager.EndDialogue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExitDialogue();
        }
    }
}
