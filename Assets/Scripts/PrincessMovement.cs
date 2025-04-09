using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princessMovement : MonoBehaviour
{
    public float speed;
    public float steerSpeed;
    private Vector2 input;
    public List<TrailRenderer> trail;


    private bool canWalk = true;

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

            // Calcul correct de la vélocité (position actuelle - ancienne position)
            Vector3 velocity = transform.position - oldPos;
            float forwardVelocity = Vector3.Dot(transform.forward, velocity);
            trail.ForEach((emitter) => emitter.emitting = forwardVelocity > 0);
        }
    }

    public void SetCanWalk(bool canWalk)
    {
        this.canWalk = canWalk;
    }
}