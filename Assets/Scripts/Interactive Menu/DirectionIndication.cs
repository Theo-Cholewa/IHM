using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INDICATOR
{
    
}
public class DirectionIndication : MonoBehaviour
{
    public Transform indicator;
    
    public GameObject objectToReplaceWith; // L'objet que vous voulez utiliser comme remplacement
    public GameObject sphereToReplace;  

    private Transform _spaceShip;
    
    void Start()
    {
        _spaceShip = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (sphereToReplace != null && objectToReplaceWith != null)
        {
            
            GameObject newObject = Instantiate(objectToReplaceWith, sphereToReplace.transform.parent);
            
            Destroy(newObject.transform.GetComponent<Planet>());
            
            newObject.transform.localPosition = Vector3.zero;

            newObject.transform.localPosition = sphereToReplace.transform.localPosition;
            newObject.transform.localRotation = sphereToReplace.transform.localRotation;
            newObject.transform.localScale = sphereToReplace.transform.localScale;
            
            Destroy(sphereToReplace);
        }
    }

    void Update()
    
    {
        Vector3 directionToTarget = objectToReplaceWith.transform.position - _spaceShip.position;
        float angle = Vector3.SignedAngle(Vector3.right, directionToTarget, Vector3.up);
        indicator.rotation = Quaternion.Euler(0, angle, 0);
    }
}
