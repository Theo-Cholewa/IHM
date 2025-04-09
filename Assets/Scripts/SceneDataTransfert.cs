
using UnityEngine;
public class SceneDataTransfer : MonoBehaviour
{
    public static SceneDataTransfer Instance;
    private int fromPlanet = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public int FromPlanet
    {
        get { return fromPlanet; } 
        set { fromPlanet = value; }
    }
}