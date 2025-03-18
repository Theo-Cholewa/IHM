using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    [FormerlySerializedAs("gameData")] public DatasInteractiveMenu gameDataInteractiveMenu;

    void Update()
    {
        transform.position = player.transform.position + gameDataInteractiveMenu.marginFromPlayer;
    }
}
