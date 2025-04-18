using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour
{
    public TextMeshProUGUI actionText; // Texte pour afficher "Prendre l'élément"
    public Image image; // Zone autour du texte
    public GameObject boutonAction;
    public GameObject boutonAnnuler; 
    private GameObject currentGameObject; // Objet actuellement sélectionné
    private Action currentAction; // Type d'action
    private string currentDescription; // Description de l'objet

    private string currentNameGameObject; // Nom de l'objet
    private GameObject player; // Référence au personnage (par exemple, la Princess)
    public DataPlanet3 data;
    public VideoController videoController;
    private Animator mAnimator;

    private Dictionary<string, string> sceneNameMap = new Dictionary<string, string>()
    {
        { "planète 0", "Planete0" },
        { "planète 1", "Planete1" },
        { "planète 2", "Planete2" },
        { "planète 3", "Planete3" },
        { "l'espace", "Interactive Menu" },
        { "race", "Race" }
    };


    void Start()
    {
        actionText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);

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
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Gestionnaire d'animation introuvable !");
        }
    }
    public void DisplayAction(GameObject gameObject, Action typeOfAction, string description, string nameGameObject)
    {
        // Stocker les informations actuelles
        currentGameObject = gameObject;
        currentAction = typeOfAction;
        currentDescription = description;
        currentNameGameObject = nameGameObject;

        // Empêcher le personnage de bouger
        if (player != null)
        {
            player.SendMessage("SetCanWalk", false);
        }

        // Afficher le texte et les boutons
        actionText.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        switch(currentAction.typeOfAction)
        {
            case Action.ActionType.Prendre:
                actionText.text = $"Prendre l'élément : {description} ?";
                break;

            case Action.ActionType.Aller:
                actionText.text = $"Aller vers {description} ?";
                break;

            case Action.ActionType.Animer:
                actionText.text = $"Animer : {description} ?";
                break;

            default:
                actionText.text = $"Action inconnue : {description} ?";
                break;
        }
        boutonAction.SetActive(true);
        boutonAction.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentAction.typeOfAction.ToString();
        boutonAnnuler.SetActive(true);
    }

    private void OnChoiceSelected(bool takeAction)
    {
        bool walk = true;
        if (takeAction)
        {
            switch (currentAction.typeOfAction)
            {
                case Action.ActionType.Prendre:
                    mAnimator.SetTrigger("TrPickUp");
                    Debug.Log("Objet " + currentDescription + " récupéré");
                    data.AddPickUpItem(currentNameGameObject);
                    Debug.Log("Objets ramassés : " + string.Join(", ", data.GetPickUpItems()));
                    Destroy(currentGameObject); // Supprime l'objet du jeu
                    break;

                case Action.ActionType.Aller:
                    Debug.Log("Direction : " + currentDescription);

                    
                    if (sceneNameMap.ContainsKey(currentDescription))
                    {
                        if (currentDescription.StartsWith("planète"))
                        {
                            // Extrait le numéro après "planète"
                            string numberPart =
                                currentDescription.Substring("planète "
                                    .Length); // Retirer "planète " du début de la chaîne
                            int planetNumber;
                            if (int.TryParse(numberPart, out planetNumber))
                            {
                                // Maintenant tu as le numéro de la planète dans planetNumber
                                Debug.Log("Planète trouvée: " + planetNumber);
                            }
                            else
                            {
                                Debug.Log("Numéro de planète invalide.");
                            }

                            if (SceneDataTransfer.Instance != null)
                            {
                                if(planetNumber == 1){ 
                                    SceneDataTransfer.Instance.SetPlanetFinished(true, 1);
                                }
                                SceneDataTransfer.Instance.FromPlanet = planetNumber;
                                Debug.Log(SceneDataTransfer.Instance.FromPlanet);
                            }
                        }

                        Debug.Log("Trying to load scene: " + sceneNameMap[currentDescription]);
                        SceneManager.LoadScene(sceneNameMap[currentDescription]);
                    }
                    
                    else
                    {
                        Debug.LogWarning("Aucune scène trouvée pour : " + currentDescription);
                    }
                    if (SceneDataTransfer.Instance != null)
                    {
                        Debug.Log(SceneDataTransfer.Instance.FromPlanet);
                    }
                    break;

                case Action.ActionType.Animer:
                    walk = false;
                    if (videoController != null)
                    {
                        videoController.PlayVideo(currentGameObject.name,player,currentAction);
                    }
                    else
                    {
                        Debug.LogWarning("VideoController non trouvé !");
                    }
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
        if (player != null && walk)
        {
            player.SendMessage("SetCanWalk", true);
        }

        // Cacher les boutons et le texte
        actionText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        boutonAction.SetActive(false);
        boutonAnnuler.SetActive(false);
    }

    public IEnumerator ResetInterractionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendre le délai spécifié
        if (currentGameObject != null)
        {
            currentGameObject.SendMessage("resetInterraction"); // Envoie le message après 5 secondes
        }
    }
}