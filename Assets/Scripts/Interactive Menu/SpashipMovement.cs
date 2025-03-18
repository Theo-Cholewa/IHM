using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpashipControl : MonoBehaviour
{
    [FormerlySerializedAs("gameData")] public DatasInteractiveMenu gameDataInteractiveMenu;
    public Rigidbody rg;
    public float forwardSpeed;
    public float steerSpeed;
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
        foreach (Transform child in  spaceship)
        {
            trail.Add(child.GetComponent<TrailRenderer>());
            Debug.Log(child);
        }
    }

    void Update()
    {
        if (Mathf.Abs(rg.position.x) > gameDataInteractiveMenu.areaSize.x && rg.velocity.x*rg.position.x >= 0)
        {
            rg.position = new Vector3(-Math.Sign(rg.position.x)*gameDataInteractiveMenu.areaSize.x, rg.position.y, rg.position.z);
        }
        if (Mathf.Abs(rg.position.z) > gameDataInteractiveMenu.areaSize.y && rg.velocity.z*rg.position.y >= 0)
        {
            rg.position = new Vector3(rg.position.x, rg.position.y, -Math.Sign(rg.position.z)*gameDataInteractiveMenu.areaSize.y);
        }
        
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");

        float rotation = inputX * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);

        Vector3 worldEulerAngles = transform.eulerAngles;
        rg.AddForce(
            inputY * forwardSpeed * Mathf.Sin(worldEulerAngles.y * Mathf.PI / 180),
            0,
            inputY * forwardSpeed * Mathf.Cos(worldEulerAngles.y * Mathf.PI / 180),
            ForceMode.Acceleration
        );

        Vector3 velocity = rg.velocity;
        float angle = Vector3.SignedAngle(Vector3.right, spaceship.forward, Vector3.up);
        //TODO refactor trail emitting
        trail.ForEach((emitter) => emitter.emitting = (angle*velocity.x >= 0) && ((angle+180)*velocity.z >=0));
        rg.AddForce(-friction * velocity.x*(1-Mathf.Abs(inputY*0.5f)), 0, -friction * velocity.z*(1-Mathf.Abs(inputY*0.5f)), ForceMode.Acceleration);
    }
}
