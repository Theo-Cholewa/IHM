using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/DataPlanet3", order = 1)]
public class DataPlanet3 : PlanetData
{
    public List<string> pickUpItems = new List<string>(); // Éléments ramassés

    [System.Serializable]
    public class StepDialogueEntry
    {
        public string character;
        public string dialogueName;

        public string numberPlanet;
    }

    public List<StepDialogueEntry> stepDialogues = new List<StepDialogueEntry>();

    // Ajouter un objet ramassé
    public void AddPickUpItem(string newItem)
    {
        if (!pickUpItems.Contains(newItem))
            pickUpItems.Add(newItem);
    }

    public List<string> GetPickUpItems()
    {
        return pickUpItems;
    }

    // Ajouter ou mettre à jour une étape de dialogue
    public void AddStepDialogue(string character, string dialogueName, string numberPlanet)
    {
        StepDialogueEntry existingEntry = stepDialogues.Find(entry => entry.character == character);

        if (existingEntry != null)
        {
            existingEntry.dialogueName = dialogueName; // Mise à jour
        }
        else
        {
            stepDialogues.Add(new StepDialogueEntry
            {
                character = character,
                dialogueName = dialogueName,
                numberPlanet = numberPlanet
            });
        }
    }

    // (Optionnel) Récupérer le dialogue en cours d'un personnage
    public string GetDialogueStep(string character)
    {
        StepDialogueEntry entry = stepDialogues.Find(e => e.character == character);
        return entry != null ? entry.dialogueName : null;
    }

    public List<StepDialogueEntry> GetDialogueSteps()
    {
        return stepDialogues;
    }
}