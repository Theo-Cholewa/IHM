﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform player;
    public Vector3 marginFromPlayer;

    void Update()
    {
        transform.position = player.transform.position + marginFromPlayer;
    }
}