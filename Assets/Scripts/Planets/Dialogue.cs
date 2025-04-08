using System;
using UnityEngine;

public class Dialogue : Interraction
{
    public TextAsset inkFile;
    public bool interractionStarted = false;

    public override void Interract()
    {
        if (!interractionStarted)
        {
            interractionStarted = true;
            FindObjectOfType<DialogueManager>().DisplayDialogue(inkFile, this.gameObject);
        }
    }

    public void SetDialogue(TextAsset inkFile)
    {
        this.inkFile = inkFile;
        interractionStarted = false;
    }
}