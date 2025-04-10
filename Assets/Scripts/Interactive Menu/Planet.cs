using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Planet : MonoBehaviour
{

    public int planetIndex;
    private Rigidbody rg;
    [FormerlySerializedAs("gameDatas")] public DatasInteractiveMenu gameDatasInteractiveMenu;
    
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.constraints = RigidbodyConstraints.None;
        Debug.Log(gameDatasInteractiveMenu.planetsPositions[planetIndex]);
        transform.position = new Vector3(gameDatasInteractiveMenu.planetsPositions[planetIndex].x, 0, gameDatasInteractiveMenu.planetsPositions[planetIndex].y);
        rg.constraints = RigidbodyConstraints.FreezeAll;
    }
}
