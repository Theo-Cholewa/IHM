using UnityEngine.Events;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent<CarIdentity, Checkpoint> onCheckpointEnter;

    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.gameObject.GetComponent<CarIdentity>() is not null)
        {
            // fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(collider.gameObject.GetComponent<CarIdentity>(), this);
        }

        // Or can be done by getting a component that means this is a car, like CarController
        //CarController car = collider.gameObject.GetComponent<CarController>();
        //if (car != null)
        //{
        //    onCheckpointEnter.Invoke(collider.gameObject, this);
        //}
    }
}