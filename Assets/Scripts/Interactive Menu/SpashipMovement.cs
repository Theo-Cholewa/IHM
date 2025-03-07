using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpashipControl : MonoBehaviour
{
    public Datas gameData;
    public Rigidbody rg;
    public float forwardSpeed;
    public float steerSpeed;
    public float friction;
    
    private float inputY;

    private float inputX;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Mathf.Abs(rg.position.x) > gameData.AreaSize.x && rg.velocity.x*rg.position.x >= 0)
        {
            rg.position = new Vector3(-Math.Sign(rg.position.x)*gameData.AreaSize.x, rg.position.y, rg.position.z);
        }
        if (Mathf.Abs(rg.position.z) > gameData.AreaSize.y && rg.velocity.z*rg.position.y >= 0)
        {
            rg.position = new Vector3(rg.position.x, rg.position.y, -Math.Sign(rg.position.z)*gameData.AreaSize.y);
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
        rg.AddForce(-friction * velocity.x*(1-Mathf.Abs(inputY*0.5f)), 0, -friction * velocity.z*(1-Mathf.Abs(inputY*0.5f)), ForceMode.Acceleration);
    }
}
