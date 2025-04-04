
using UnityEngine;
public class SceneDataTransfer : MonoBehaviour
{
    public static SceneDataTransfer Instance;
    public int fromPlanet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}