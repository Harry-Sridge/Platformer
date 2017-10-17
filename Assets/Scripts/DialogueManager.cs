using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;

    public Animator animator;
    public Text nameText;
    public Text bodyText;

    private void Start()
    {
        sentences = new Queue<string>();
    }
        
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.dialogues)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string currentSentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeDialogue(currentSentence));
    }

    IEnumerator TypeDialogue (string sentence)
    {
        bodyText.text = "";

        foreach(char character in sentence.ToCharArray())
        {
            bodyText.text += character;
            yield return null;
        }
    }

    public void EndDialogue ()
    {
        animator.SetBool("isOpen", false);
        sentences.Clear();
    }
}
