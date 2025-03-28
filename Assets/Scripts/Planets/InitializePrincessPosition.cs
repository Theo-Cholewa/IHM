using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacePlayerOnSphere : MonoBehaviour
{
    private Transform sphere;
    private Transform player;
    [FormerlySerializedAs("gameDatas")] public PlanetData gameData;
    
    private void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;


        GameObject sphereObject = GameObject.FindGameObjectWithTag("Planet");
        sphere = sphereObject.transform;
        
        PlacePlayerAtTop();    }
    
    private float GetSphereRadius()
    {
        float baseRadius = 0.5f;
        
        SphereCollider sphereCollider = sphere.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            baseRadius = sphereCollider.radius;
        }
        
        return baseRadius * sphere.transform.lossyScale.x;// * gameData.planetScale;
    }
    
    private float GetPlayerHeight()
    {
        float height = 2.0f;
        
        CapsuleCollider capsule = player.GetComponent<CapsuleCollider>();
        if (capsule != null)
        {
            height = capsule.height * player.lossyScale.y;
            return height;
        }
        
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            height = controller.height * player.lossyScale.y;
            return height;
        }
        
        Renderer renderer = player.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            height = renderer.bounds.size.y;
            return height;
        }
        
        return height;
    }
    
    private void PlacePlayerAtTop()
    {
        float radius = GetSphereRadius();
        float playerHeight = GetPlayerHeight();
        float heightOffset = playerHeight / 2.0f;
        
        Vector3 upDirection = Vector3.up;
        Vector3 topPosition = sphere.position + upDirection * radius;
        Vector3 finalPosition = topPosition + upDirection * heightOffset;
        
        player.position = finalPosition;
        player.up = upDirection;
        player.forward = Vector3.forward;
    }
}