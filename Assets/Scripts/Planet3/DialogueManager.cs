using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
