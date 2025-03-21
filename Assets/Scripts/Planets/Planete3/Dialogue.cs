using System;
using UnityEngine;

public class Dialogue : Interraction
{
    public string[] dialogues;
    public int dialogueIndex = 0; 

    public bool interractionStarted = false;
    public override void Interract()
    {
        if (!interractionStarted)
        {
            Debug.Log("Dialogue");
            interractionStarted = true;
            FindObjectOfType<DialogueManager>().DisplayDialogue(this);
        }
    }

    public String GetDialogue()
    {
        return dialogues[dialogueIndex];
    }

    public bool IsDialogueFinished()
    {
        return dialogueIndex == dialogues.Length;
    }
}
