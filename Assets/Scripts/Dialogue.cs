using System;
using UnityEngine;

public class Dialogue : Interraction
{
    public TextAsset inkFile;
    public bool interractionStarted = false;

    public int numberPlanet = 0; // Numéro de la planète actuelle

    public override void Interract()
    {
        if (!interractionStarted)
        {
            interractionStarted = true;
            FindObjectOfType<DialogueManager>().DisplayDialogue(inkFile, this.gameObject, numberPlanet);
        }
    }

    public void SetDialogue(TextAsset inkFile)
    {
        this.inkFile = inkFile;
        interractionStarted = false;
    }
}