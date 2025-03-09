using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public int planetIndex;
    private Rigidbody rg;
    public Datas gameDatas;
    
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.constraints = RigidbodyConstraints.None;
        Debug.Log(gameDatas.planetsPositions[planetIndex]);
        transform.position = new Vector3(gameDatas.planetsPositions[planetIndex].x, 0, gameDatas.planetsPositions[planetIndex].y);
        rg.constraints = RigidbodyConstraints.FreezeAll;
    }

    
    void Update()
    {
        
    }
}
