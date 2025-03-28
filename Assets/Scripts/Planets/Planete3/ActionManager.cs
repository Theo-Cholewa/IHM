using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public TextMeshProUGUI actionText; // Texte pour afficher "Prendre l'élément"
    public GameObject boutonAction;
    public GameObject boutonAnnuler; 
    private GameObject currentGameObject; // Objet actuellement sélectionné
    private Action.ActionType currentActionType; // Type d'action
    private string currentDescription; // Description de l'objet
    private GameObject player; // Référence au personnage (par exemple, la Princess)
    public DataPlanet3 data;

    void Start()
    {
        actionText.gameObject.SetActive(false);

        if (boutonAction != null)
        {
            boutonAction.SetActive(false);
            boutonAction.transform.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(true));
        }

        if (boutonAnnuler != null)
        {
            boutonAnnuler.SetActive(false);
            boutonAnnuler.transform.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(false));
        }

        player = GameObject.Find("PrincessBody");
        if (player == null)
        {
            Debug.LogError("L'objet 'Princess' n'a pas été trouvé dans la scène !");
        }
    }

    public void DisplayAction(GameObject gameObject, Action.ActionType typeOfAction, string description)
    {
        // Stocker les informations actuelles
        currentGameObject = gameObject;
        currentActionType = typeOfAction;
        currentDescription = description;

        // Empêcher le personnage de bouger
        if (player != null)
        {
            player.SendMessage("SetCanWalk", false);
        }

        // Afficher le texte et les boutons
        actionText.gameObject.SetActive(true);
        switch(currentActionType)
        {
            case Action.ActionType.Prendre:
                actionText.text = $"Prendre l'élément : {description} ?";
                break;

            case Action.ActionType.Aller:
                actionText.text = $"Aller vers : {description} ?";
                break;

            case Action.ActionType.Animer:
                actionText.text = $"Animer : {description} ?";
                break;

            default:
                actionText.text = $"Action inconnue : {description} ?";
                break;
        }
        boutonAction.SetActive(true);
        boutonAction.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentActionType.ToString();
        boutonAnnuler.SetActive(true);
    }

    private void OnChoiceSelected(bool takeAction)
    {
        if (takeAction)
        {
            switch (currentActionType)
            {
                case Action.ActionType.Prendre:
                    Debug.Log("Objet " + currentDescription + " récupéré");
                    data.AddPickUpItem(currentDescription);
                    Destroy(currentGameObject); // Supprime l'objet du jeu
                    break;

                case Action.ActionType.Aller:
                    Debug.Log("Direction : " + currentDescription);
                    break;

                case Action.ActionType.Animer:
                    Debug.Log("Animation : " + currentDescription);
                    break;

                default:
                    Debug.Log("Aucune action définie");
                    break;
            }
        }
        else
        {
            Debug.Log("Action annulée");
            StartCoroutine(ResetInterractionAfterDelay(5f));
        }

        // Réactiver le mouvement du personnage
        if (player != null)
        {
            player.SendMessage("SetCanWalk", true);
        }

        // Cacher les boutons et le texte
        actionText.gameObject.SetActive(false);
        boutonAction.SetActive(false);
        boutonAnnuler.SetActive(false);
    }

    private IEnumerator ResetInterractionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendre le délai spécifié
        if (currentGameObject != null)
        {
            currentGameObject.SendMessage("resetInterraction"); // Envoie le message après 5 secondes
        }
    }
}