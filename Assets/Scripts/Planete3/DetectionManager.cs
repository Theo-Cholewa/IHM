using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    [System.Serializable]
    public class DetectedObject
    {
        public GameObject targetObject; // L'objet à détecter
        public float detectionRadius;  // Rayon de détection
    }

    public List<DetectedObject> objectsToDetect; // Liste des objets et leur rayon de détection
    public GameObject princess; // L'objet "princess"

    void Update()
    {
        foreach (DetectedObject detected in objectsToDetect)
        {
            if (detected.targetObject == null || princess == null)
                continue;

            // Calcul de la distance entre la princesse et l'objet
            float distance = Vector3.Distance(detected.targetObject.transform.position, princess.transform.position);

            // Vérification si la distance est inférieure au rayon de détection
            if (distance < detected.detectionRadius)
            {
                // Récupérer le script Interraction sur l'objet détecté
                Interraction interractionScript = detected.targetObject.GetComponent<Interraction>();

                if (interractionScript != null)
                {
                    // Activer la méthode Interraction
                    interractionScript.Interract();
                }
            }
        }
    }
}
