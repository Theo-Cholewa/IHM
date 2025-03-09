using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Datas gameData;

    void Update()
    {
        transform.position = player.transform.position + gameData.marginFromPlayer;
    }
}
