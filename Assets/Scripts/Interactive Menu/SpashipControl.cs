using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class SpashipControl : princessMovement


{
    [FormerlySerializedAs("gameData")] public DatasInteractiveMenu gameDataInteractiveMenu;


    public Rigidbody rg;

    public float friction;


    private float inputY;


    private float inputX;


    private List<TrailRenderer> trail;


    private Transform spaceship;


    void Start()


    {
        rg = GetComponent<Rigidbody>();


        spaceship = GameObject.FindWithTag("Player").transform;


        trail = new List<TrailRenderer>();


        foreach (Transform child in spaceship)


        {
            trail.Add(child.GetComponent<TrailRenderer>());


            Debug.Log(child);
        }
    }


    void FixedUpdate(){
    if(canWalk)
    {
        if (Mathf.Abs(rg.position.x) > gameDataInteractiveMenu.areaSize.x)


        {
            trail.ForEach((emitter) => emitter.emitting = false);


            rg.position = new Vector3(-Math.Sign(rg.position.x) * gameDataInteractiveMenu.areaSize.x, rg.position.y,
                rg.position.z);
        }


        if (Mathf.Abs(rg.position.z) > gameDataInteractiveMenu.areaSize.y)


        {
            trail.ForEach((emitter) => emitter.emitting = false);


            rg.position = new Vector3(rg.position.x, rg.position.y,
                -Math.Sign(rg.position.z) * gameDataInteractiveMenu.areaSize.y);
        }


        inputY = Input.GetAxis("Vertical");


        inputX = Input.GetAxis("Horizontal");


        float rotation = inputX * steerSpeed * Time.fixedDeltaTime;

        transform.Rotate(0, rotation, 0, Space.World);


        Vector3 worldEulerAngles = transform.eulerAngles;

        Vector3 force = new Vector3(inputY * speed * Mathf.Sin(worldEulerAngles.y * Mathf.PI / 180),
            0,
            inputY * speed * Mathf.Cos(worldEulerAngles.y * Mathf.PI / 180));
        rg.AddForce(force,
            ForceMode.Acceleration
        );


        Vector3 velocity = rg.velocity;


        float forwardVelocity = Vector3.Dot(spaceship.forward, velocity);


        trail.ForEach((emitter) => emitter.emitting = forwardVelocity > 0);

        Vector3 frictionForce = new Vector3(
            -friction * velocity.x * (1 - Mathf.Abs(inputY * 0.5f)),
            0,
            -friction * velocity.z * (1 - Mathf.Abs(inputY * 0.5f))
        );
        rg.AddForce(frictionForce, ForceMode.Acceleration); 
    }
    else
    {
        rg.velocity = Vector3.zero;
    }
    }
}