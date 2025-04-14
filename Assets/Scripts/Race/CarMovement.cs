using System.Collections;
using UnityEngine;

public class CarMovement: MonoBehaviour
{
    public Rigidbody rg;
    public float forwardSpeed;
    public float steerSpeed;
    public float friction;
    public Vector2 input;
    
    // Flag to indicate whether the animation/delay is finished.
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        // Set the starting position.
        //rg.transform.position = new Vector3(-0.44f, 0, -3.49f);
    }

    void FixedUpdate()
    {
        float rotation = input.x * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);

        Vector3 worldEulerAngles = transform.eulerAngles;
        rg.AddForce(
            input.y * forwardSpeed * Mathf.Sin(worldEulerAngles.y * Mathf.PI / 180),
            0,
            input.y * forwardSpeed * Mathf.Cos(worldEulerAngles.y * Mathf.PI / 180),
            ForceMode.Acceleration
        );

        Vector3 velocity = rg.velocity;
        
        float maxSpeed = 20f;
        if (rg.velocity.magnitude > maxSpeed)
        {
            rg.velocity = rg.velocity.normalized * maxSpeed;
        }
        
        rg.AddForce(-friction * velocity.x*(1-Mathf.Abs(input.y*0.5f)), 0, -friction * velocity.z*(1-Mathf.Abs(input.y*0.5f)), ForceMode.Acceleration);
        
        Vector3 localVel = transform.InverseTransformDirection(rg.velocity);
        localVel.x *= 0.7f; // Plus proche de 0 = plus de grip
        rg.velocity = transform.TransformDirection(localVel);
    }
    

    public void SetInputs(Vector2 input) {
        this.input = input;
    }

    public float GetSpeed()
    {
        return rg.velocity.magnitude;
    }
    
}