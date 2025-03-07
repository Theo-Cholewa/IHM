using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO; 
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text; // TextMeshPro pour afficher le dialogue
    public Button nextButton; // Bouton "Suivant" ou "Fermer"
    public List<Dialogue> dialogues; // Liste des objets de type Dialogue
    private Dialogue currentDialogue; // Référence au Dialogue actif

    void Start()
    {
        text.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        
        string filePath = Path.Combine(Application.dataPath, "Datas/Planete3/dialogs.json");
        LoadAndAssignDialogues(filePath);
    }

    public void LoadAndAssignDialogues(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Lire le contenu du fichier JSON
            string jsonData = File.ReadAllText(filePath);

            // Désérialiser le JSON en un dictionnaire
            Dictionary<string, List<string>> dialogueData = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData);

            foreach (var entry in dialogueData)
            {
                Debug.Log($"Clé : {entry.Key}, Valeur : {entry.Value[0]}");
                // Trouver l'objet dans la scène portant le nom correspondant
                GameObject obj = GameObject.Find(entry.Key);
                if (obj != null && obj.TryGetComponent<Dialogue>(out Dialogue dialogueComponent))
                {
                    // Affecter les données au composant Dialogue
                    dialogueComponent.dialogues = entry.Value.ToArray();
                }
            }
        }
        else
        {
            Debug.LogError($"Fichier JSON introuvable : {filePath}");
        }
    }

    public void DisplayDialogue(Dialogue dialogue)
    {
        text.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);

        currentDialogue = dialogue;

        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        if (currentDialogue != null)
        {
            text.text = currentDialogue.GetDialogue();
            currentDialogue.dialogueIndex++;
            // Vérifie si le dialogue est terminé
            if (currentDialogue.IsDialogueFinished())
            {
                nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fermer";
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(CloseDialogue);
            }
            else
            {
                nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Suivant";
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => { OnNextDialogue(); });
            }
        }
    }

    private void OnNextDialogue()
    {
        UpdateDialogue();
    }

    private void CloseDialogue()
    {
        text.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        currentDialogue = null; // Réinitialise le dialogue actif
    }
}
