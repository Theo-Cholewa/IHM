using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloaderPlanet3 : MonoBehaviour
{
    [SerializeField] private DataPlanet3 data; // À assigner dans l'inspecteur
    [SerializeField] private string currentPlanet = "3"; // Numéro de planète actuelle

    void Start()
    {
        // 1. Suppression des objets déjà ramassés
        foreach (string itemName in data.GetPickUpItems())
        {
            GameObject objToRemove = GameObject.Find(itemName);

            if (objToRemove != null)
            {
                Debug.Log("Objet déjà ramassé trouvé dans la scène, suppression : " + itemName);
                Destroy(objToRemove);
            }
            else
            {
                Debug.Log("Objet " + itemName + " non trouvé dans la scène.");
            }
        }

        // 2. Mise à jour des dialogues
        foreach (var step in data.GetDialogueSteps())
        {
            if (step.numberPlanet != currentPlanet)
                continue;

            GameObject characterObj = GameObject.Find(step.character);
            if (characterObj == null)
            {
                Debug.LogWarning("Personnage non trouvé dans la scène : " + step.character);
                continue;
            }

            // Chemin du fichier Ink
            string path = $"Planet{step.numberPlanet}/{step.dialogueName}";
            TextAsset inkJSON = Resources.Load<TextAsset>(path);

            if (inkJSON == null)
            {
                Debug.LogError("Fichier Ink non trouvé à : " + path);
                continue;
            }

            Dialogue dialogue = characterObj.GetComponent<Dialogue>();
            if (dialogue != null)
            {
                dialogue.SetDialogue(inkJSON);
                Debug.Log($"Dialogue mis à jour pour {step.character} avec {step.dialogueName}");
            }
            else
            {
                Debug.LogWarning($"DialogueController manquant sur {step.character}");
            }
        }
    }
}