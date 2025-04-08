using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text; // TextMeshPro pour afficher le dialogue
    public Image image; // Zone autour du texte
    public TextMeshProUGUI personName; // Texte pour afficher le nom de la personne
    public GameObject choix1; // Bouton choix 1
    public GameObject choix2; // Bouton choix 2
    public GameObject choix3; // Bouton choix 3

    private GameObject currentGameObject; // Objet contenant le dialogue
    private Story story; // Instance de l'histoire Ink
    private GameObject princess; // Référence à l'objet Princess

    public string planet; // Nom de la planète actuelle -> lien vers les dialogues

    void Start()
    {
        text.gameObject.SetActive(false);
        personName.gameObject.SetActive(false);
        image.gameObject.SetActive(false);

        // Configuration des boutons de choix
        ConfigureButton(choix1, 0);
        ConfigureButton(choix2, 1);
        ConfigureButton(choix3, 2);

        // Référence à Princess
        princess = GameObject.Find("PrincessBody");
        if (princess == null)
        {
            Debug.LogError("L'objet 'Princess' n'a pas été trouvé dans la scène !");
        }
    }

    private void ConfigureButton(GameObject button, int choiceIndex)
    {
        if (button != null)
        {
            button.SetActive(false);
            button.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }
    }

    public void DisplayDialogue(TextAsset inkFile, GameObject gameObjectParam)
    {
        currentGameObject = gameObjectParam;

        // Charger l'histoire Ink
        story = new Story(inkFile.text);

        // Bloquer les mouvements de Princess pendant le dialogue
        if (princess != null)
        {
            princess.SendMessage("SetCanWalk", false);
        }

        // Activer le texte et commencer le dialogue
        image.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        
        try {
            object nameValue = story.variablesState["name"];
            personName.text = nameValue.ToString();
            personName.gameObject.SetActive(true);
        }
        catch {
            personName.gameObject.SetActive(false);
        }
        ProcessDialogue();
    }

     private void ProcessDialogue()
    {
        // Afficher le dialogue tant qu'il peut continuer
        if (story.canContinue)
        {
            string currentText = story.Continue();
            text.text = currentText; // Affiche dans TextMeshProUGUI
        }

        // Si des choix sont disponibles, afficher les boutons
        if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else if (!story.canContinue)
        {
            image.gameObject.SetActive(false); // Désactiver l'image
            personName.gameObject.SetActive(false); // Désactiver le nom
            UpdateNextDialogue();
            HideChoices(); // Cache les boutons si le dialogue est terminé

            // Appeler SetCanWalk(true) à la fin du dialogue
            if (princess != null)
            {
                princess.SendMessage("SetCanWalk", true);
            }
        }
    }

    private void UpdateNextDialogue()
    {
        // Vérifie si la variable "nextDialogue" existe
        if (story.variablesState["nextDialogue"] != null)
        {
            string newNextDialogue = story.variablesState["nextDialogue"].ToString();

            // Démarre une coroutine pour attendre avant de charger le nouveau dialogue
            StartCoroutine(WaitAndLoadDialogue(newNextDialogue));
        }
        else
        {
            Debug.LogError("La variable 'nextDialogue' est introuvable dans l'histoire Ink !");
        }
    }

    private System.Collections.IEnumerator WaitAndLoadDialogue(string newNextDialogue)
    {
        // Attendre 5 secondes
        yield return new WaitForSeconds(5f);
        TextAsset newInkFile = Resources.Load<TextAsset>("Planet3/" + newNextDialogue);
        if (newInkFile != null)
        {
            Debug.Log($"Dialogue chargé via Resources : {newNextDialogue}");
            Dialogue dialogueComponent = currentGameObject.GetComponent<Dialogue>();
            if (dialogueComponent != null)
            {
                dialogueComponent.SetDialogue(newInkFile);
            }
        }
        else
        {
            Debug.LogError($"Le dialogue '{newNextDialogue}' est introuvable dans Resources !");
        }
    }

    private void DisplayChoices()
    {
        HideChoices();
        int choiceCount = story.currentChoices.Count;
        switch(choiceCount){
            case 1 : {
                ActivateChoiceButton(choix1, 0, story.currentChoices[0].text, 0);
                break;
            }
            case 2 : {
                ActivateChoiceButton(choix1, 0, story.currentChoices[0].text, -90);
                ActivateChoiceButton(choix2, 1, story.currentChoices[1].text, 90);
                break;
            }
            case 3 : {
                ActivateChoiceButton(choix1, 0, story.currentChoices[0].text, -90);
                ActivateChoiceButton(choix2, 1, story.currentChoices[1].text, 0);
                ActivateChoiceButton(choix3, 2, story.currentChoices[2].text, 90);
                break;
            }
        } 
    }

    private void ActivateChoiceButton(GameObject button, int choiceIndex, string choiceText, int xValue)
    {
        if (button != null && choiceText != null)
        {
            button.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
           Vector3 currentPosition = button.transform.localPosition;
            currentPosition.x = xValue; // Modifie uniquement la composante x
            button.transform.localPosition = currentPosition; // Applique la nouvelle position
        }
    }


    private void HideChoices()
    {
        choix1?.SetActive(false);
        choix2?.SetActive(false);
        choix3?.SetActive(false);
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        if (story == null)
        {
            return;
        }

        if (choiceIndex < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(choiceIndex);
            HideChoices();
            ProcessDialogue();
        }
    }

}