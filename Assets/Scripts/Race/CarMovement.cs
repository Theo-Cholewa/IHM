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

    void Update()
    {
        // If the animation/delay is not finished, skip movement.

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
        rg.AddForce(-friction * velocity.x*(1-Mathf.Abs(input.y*0.5f)), 0, -friction * velocity.z*(1-Mathf.Abs(input.y*0.5f)), ForceMode.Acceleration);
    }
    

    public void SetInputs(Vector2 input) {
        this.input = input;
    }

    public float GetSpeed()
    {
        return rg.velocity.magnitude;
    }
    
}