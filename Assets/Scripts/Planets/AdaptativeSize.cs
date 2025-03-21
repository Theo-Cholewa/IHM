using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AdaptativeSize : MonoBehaviour
{
    
    
    [FormerlySerializedAs("gameDatas")] public DataPlanet1 gameData;

    void Start()
    {
        transform.localScale = new Vector3(gameData.planetScale,gameData.planetScale,gameData.planetScale);
    }
}
