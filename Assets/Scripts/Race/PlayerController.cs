using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    
    private float inputX;
    private float inputY;
    public UnityEvent<Vector2> onInput;

    
    
    // Flag to indicate whether the animation/delay is finished.
    void Start()
    {
    }

    void Update()
    {
        // If the animation/delay is not finished, skip movement.

        // Process input and movement only after the animation is complete.
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        onInput.Invoke(new Vector2(inputX, inputY).normalized);

    }
}