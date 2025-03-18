using System;
using UnityEngine;

public class Dialogue : Interraction
{
    public TextAsset inkFile;
    public string[] dialogues;
    public int dialogueIndex = 0; 

    public bool interractionStarted = false;
    public override void Interract()
    {
        if (!interractionStarted)
        {
            interractionStarted = true;
            FindObjectOfType<DialogueManager>().DisplayDialogue(inkFile);
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
