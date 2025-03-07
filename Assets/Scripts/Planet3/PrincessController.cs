using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class princessController : MonoBehaviour
{
    public UnityEvent<Vector2> unityEvent;
    private float inputX;
    private float inputY;
    void Update() // Get keyboard inputs
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        unityEvent.Invoke(new Vector2(inputX, inputY).normalized);
    }
}