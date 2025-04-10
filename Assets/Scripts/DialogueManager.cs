using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.SceneManagement;

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

    private string currentStoryName = "";

    private bool loaderRace = false; 

    private int currentDialogPlanet; // Nom du dialogue actuel

    private GameObject princess; // Référence à l'objet Princess

    public string planet; // Nom de la planète actuelle -> lien vers les dialogues

    private bool isDialogueWithSin = false; // Indique si le dialogue est avec un pêché

    public DataPlanet3 data;

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

    public void DisplayDialogue(TextAsset inkFile, GameObject gameObjectParam, int numberPlanet)
    {
        currentGameObject = gameObjectParam;
        currentStoryName = inkFile.name; // Nom de l'histoire actuelle

        // Charger l'histoire Ink
        story = new Story(inkFile.text);
        currentDialogPlanet = numberPlanet; 

        // Bloquer les mouvements de Princess pendant le dialogue
        if (princess != null)
        {
            princess.SendMessage("SetCanWalk", false);
            data.AddStepDialogue(currentGameObject.name, inkFile.name, currentDialogPlanet+""); // Enregistrer le dialogue actuel
        }

        switch(currentStoryName){
            case "sculptor2" : {
                Debug.Log("Dialogue de la sculpture 2 chargé.");
                story.variablesState["pierres"] = data.GetNumberOfStone(); 
                break;
            }
            case "cook2" : {
                Debug.Log("Dialogue du cuisinier 2 chargé.");
                List<string> ingredients = data.GetIngredients(); // Récupérer les ingrédients ramassés
                story.variablesState["nb"] = ingredients.Count; // Nombre d'ingrédients ramassés
                if(ingredients.Count > 0){
                    data.AddPickUpItem("soupe"); 
                }
                
                switch (ingredients.Count)
                {
                    case 0:
                        Debug.Log("Aucun ingrédient ramassé.");
                        break;
                    case 1:
                        string a = "Ah, vous avez rapporté des " + ingredients[0] + ". Je reviens, gardez mon gâteau." ;
                        story.variablesState["phrase"] = a;  // Un seul ingrédient ramassé
                        break;
                    case 2:
                        string b = "Ah, vous avez rapporté des " + ingredients[0] + " et des " + ingredients[1] + ". Je reviens, gardez mon gâteau." ;
                        story.variablesState["phrase"] = b; // Deux ingrédients ramassés
                        break;
                    case 3:
                        string c = "Incroyable ! Vous avez trouvé trois ingrédients : des " + ingredients[0] + ", des " + ingredients[1] + " et des " + ingredients[2] + "! Quel festin ! Je reviens, gardez mon gâteau." ;
                        story.variablesState["phrase"] = c; // Trois ingrédients ramassés
                        break;
                }
                //Debug.Log(story.variablesState["nb"]);
                //Debug.Log(story.variablesState["phrase"]);
                break;
            }
            case "traveller1" : {
                Debug.Log("Dialogue du voyageur 1 chargé.");
                if(data.GetPickUpItems().Contains("soupe")){
                    Debug.Log("La soupe a été ramassée.");
                    story.variablesState["soupe"] = 1;
                }
                break;
            }
            case "mayor2" : {
                if(data.GetSculptorGood() && data.GetTravellerGood()){
                    story.variablesState["finish"] = 1; 
                    SceneDataTransfer.Instance.SetPlanetFinished(true, 3);
                    Debug.Log("La planète 3 est terminée.");
                }
                break;
            }
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

        try {
            object peche = story.variablesState["peche"];
            if(peche != null){
                isDialogueWithSin = true;
            }
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
            if(isDialogueWithSin){
                try {
                    object nameValue = story.variablesState["peche"];
                    if(nameValue.ToString() != ""){
                        SceneDataTransfer.Instance.SetStoryEnd(nameValue.ToString()); // Enregistrer le nom du pêché
                    }

                    
                }
                catch{}
            }

            try {
                if(currentStoryName == "race1" || currentStoryName == "race2"){
                    object nameValue = story.variablesState["course"];
                    if (nameValue.Equals(1))
                    {
                        SceneDataTransfer.Instance.SetPlanetFinished(true, 2); // Enregistrer le nom de la course
                        loaderRace = true; // Indiquer que la course est terminée
                    }
                }
                if(currentStoryName == "sculptor2"){
                    object nameValue = story.variablesState["nextDialogue"];
                    if (nameValue.Equals("sculptor3"))
                    {
                        data.SetSculptorGood();
                    }
                }
                if(currentStoryName == "traveller1"){
                    object nameValue = story.variablesState["nextDialogue"];
                    if (nameValue.Equals("traveller2"))
                    {
                        data.SetTravellerGood();
                    }
                }
            }
            catch{}


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
        yield return new WaitForSeconds(4f);
        TextAsset newInkFile = Resources.Load<TextAsset>("Planet"+currentDialogPlanet+"/" + newNextDialogue);
        if (newInkFile != null)
        {
            Debug.Log($"Dialogue chargé via Resources : {newNextDialogue}");
            Dialogue dialogueComponent = currentGameObject.GetComponent<Dialogue>();

            data.AddStepDialogue(currentGameObject.name, newInkFile.name, currentDialogPlanet+"");

            if (dialogueComponent != null)
            {
                dialogueComponent.SetDialogue(newInkFile);
            }
        }
        else
        {
            Debug.LogError($"Le dialogue '{newNextDialogue}' est introuvable dans Resources !");
        }

        if(loaderRace){
            loaderRace = false; 
            SceneDataTransfer.Instance.FromPlanet = 2;
            SceneManager.LoadScene("Scenes/Race");
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
                ActivateChoiceButton(choix1, 0, story.currentChoices[0].text, -120);
                ActivateChoiceButton(choix2, 1, story.currentChoices[1].text, 120);
                break;
            }
            case 3 : {
                ActivateChoiceButton(choix1, 0, story.currentChoices[0].text, -180);
                ActivateChoiceButton(choix2, 1, story.currentChoices[1].text, 0);
                ActivateChoiceButton(choix3, 2, story.currentChoices[2].text, 180);
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