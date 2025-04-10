using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princessMovement : MonoBehaviour
{
    public float speed;
    public float steerSpeed;
    private Vector2 input;


    protected bool canWalk = true;

    void Update()
    {
        if (canWalk)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Rotation (gauche/droite)
            float rotation = input.x * steerSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);

            Vector3 oldPos = transform.position;

            // Translation (avant/arrière)
            Vector3 moveDirection = transform.forward * (input.y * speed * Time.deltaTime);
            transform.Translate(moveDirection, Space.World);
        }
    }

    public void SetCanWalk(bool canWalk)
    {
        this.canWalk = canWalk;
    }
}