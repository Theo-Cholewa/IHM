using UnityEngine;

public class Action : Interraction
{
    [System.Serializable]
    public enum ActionType
    {
        Prendre, // -> objet à prendre
        Aller, // -> fusée ou planète
        Animer, // -> lancer une animation
        None // Valeur par défaut pour éviter des erreurs
    }

    public ActionType typeOfAction;
    public string description;

    public string nameGameObjet = "";
    public bool interractionStarted = false;

    public override void Interract()
    {
        if (!interractionStarted)
        {
            interractionStarted = true;
            FindObjectOfType<ActionManager>().DisplayAction(gameObject, this, description);
        }
    }

    public void resetInterraction()
    {
        interractionStarted = false;
    }
}