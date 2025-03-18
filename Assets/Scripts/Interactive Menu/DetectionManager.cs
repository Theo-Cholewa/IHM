using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DetectionManager2 : MonoBehaviour
{
    public static DetectionManager2 Instance;

    private GameObject player;
    [FormerlySerializedAs("gameDatas")] public DatasInteractiveMenu gameDatasInteractiveMenu;

    private List<GameObject> objectToDetect;

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        objectToDetect = new List<GameObject>();
        gameDatasInteractiveMenu.elementsToDetect.ForEach(tag => objectToDetect.AddRange(GameObject.FindGameObjectsWithTag(tag)));
    }
    
    void Update()
    {
        foreach (GameObject targetObject in objectToDetect)
        {
            if (targetObject == null || player == null)
                continue;

            // Calcul de la distance entre la princesse et l'objet
            float distance = Vector3.Distance(targetObject.transform.position, player.transform.position);

            Interraction2 interractionScript = targetObject.GetComponent<Interraction2>();
            if (interractionScript != null)
            {

                if (distance < interractionScript.getDetectionRadius())
                {
                    interractionScript.Interract();
                }
            }
        }
    }
}
