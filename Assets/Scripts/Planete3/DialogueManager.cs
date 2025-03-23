using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text; // TextMeshPro pour afficher le dialogue
    public GameObject choix1; // Bouton choix 1
    public GameObject choix2; // Bouton choix 2
    public GameObject choix3; // Bouton choix 3

    private TextAsset currentInkFile; // Fichier Ink actif
    private Story story; // Instance de l'histoire Ink
    private GameObject princess; // Référence à l'objet Princess

    void Start()
    {
        text.gameObject.SetActive(false);

        if(choix1 != null){
            choix1.SetActive(false);
            choix1.transform.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(0));

        }
        if(choix2 != null){
            choix2.SetActive(false);
            choix2.transform.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(1));

        }
        if(choix3 != null){
            choix3.SetActive(false);
            choix3.transform.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(2));

        }
       
        princess = GameObject.Find("Princess");
        if (princess == null)
        {
            Debug.LogError("L'objet 'Princess' n'a pas été trouvé dans la scène !");
        }
    }

    public void DisplayDialogue(TextAsset inkFile)
    {
        // Charger l'histoire Ink
        story = new Story(inkFile.text);

        // Appeler SetCanWalk(false) au début du dialogue
        if (princess != null)
        {
            princess.SendMessage("SetCanWalk", false);
        }

        // Activer l'élément de texte
        text.gameObject.SetActive(true);

        // Commencer le dialogue
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
            text.text = "Fin du dialogue."; // Affiche la fin dans TextMeshProUGUI
            HideChoices(); // Cache les boutons si le dialogue est terminé

            // Appeler SetCanWalk(true) à la fin du dialogue
            if (princess != null)
            {
                princess.SendMessage("SetCanWalk", true);
            }
        }
    }

    private void DisplayChoices()
    {
        // Cacher tous les boutons avant d'afficher les nouveaux choix
        HideChoices();

        // Ajuster les positions et afficher les boutons en fonction du nombre de choix
        int choiceCount = story.currentChoices.Count;

        if (choiceCount == 1)
        {
            choix1.gameObject.SetActive(true);
            choix1.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[0].text;
            choix1.transform.localPosition = new Vector3(0, choix1.transform.localPosition.y, 0);
        }
        else if (choiceCount == 2)
        {
            choix1.gameObject.SetActive(true);
            choix2.gameObject.SetActive(true);

            choix1.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[0].text;
            choix2.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[1].text;

            choix1.transform.localPosition = new Vector3(90, choix1.transform.localPosition.y, 0);
            choix2.transform.localPosition = new Vector3(-90, choix2.transform.localPosition.y, 0);
        }
        else if (choiceCount == 3)
        {
            choix1.gameObject.SetActive(true);
            choix2.gameObject.SetActive(true);
            choix3.gameObject.SetActive(true);

            choix1.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[0].text;
            choix2.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[1].text;
            choix3.GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[2].text;

            choix1.transform.localPosition = new Vector3(180, choix1.transform.localPosition.y, 0);
            choix2.transform.localPosition = new Vector3(0, choix2.transform.localPosition.y, 0);
            choix3.transform.localPosition = new Vector3(-180, choix3.transform.localPosition.y, 0);
        }
    }

    private void HideChoices()
    {
        // Cacher tous les boutons
        choix1.gameObject.SetActive(false);
        choix2.gameObject.SetActive(false);
        choix3.gameObject.SetActive(false);
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        // Sélectionner le choix et continuer le dialogue
        if (choiceIndex < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(choiceIndex); // Appliquer le choix
            HideChoices(); // Cache les boutons après sélection
            ProcessDialogue(); // Continue le dialogue
        }
    }
}