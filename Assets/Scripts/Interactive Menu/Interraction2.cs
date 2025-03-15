using UnityEngine;

public abstract class Interraction2 : MonoBehaviour
{
    
    public float detectionRadius;
    
    public abstract void Interract();

    public float getDetectionRadius()
    {
        return detectionRadius;
    }
}
