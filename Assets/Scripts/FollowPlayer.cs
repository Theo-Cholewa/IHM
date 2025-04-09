
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    [FormerlySerializedAs("gameData")] public Data gameData;
    private Vector3 _relativePosition;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            player = playerObject.transform;
            _relativePosition = player.InverseTransformPoint(transform.position);
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Player' n'a été trouvé !");
        }
    }

    void LateUpdate()
    {
        Quaternion playerRotation = player.rotation;
        
        Vector3 offsetPosition = player.position + playerRotation * gameData.marginFromPlayer;
        transform.position = offsetPosition;
        transform.LookAt(player);
        
        Vector3 playerUp = player.up;
        Vector3 forwardDir = player.position - transform.position;
        Vector3 rightDir = Vector3.Cross(playerUp, forwardDir).normalized;
        Vector3 upDir = Vector3.Cross(forwardDir, rightDir).normalized;
        
        transform.rotation = Quaternion.LookRotation(forwardDir, upDir);
    }
}