
using UnityEngine;
public class SceneDataTransfer : MonoBehaviour
{
    public static SceneDataTransfer Instance;
    private int fromPlanet = 0;

    public int numberReturnsPlanet0 = 3;

    public bool planet1Finished = false;
    public bool planet2Finished = false;
    public bool planet3Finished = false;

    public string storyEnd;

    public void SetStoryEnd(string newStoryEnd)
    {
        if(this.storyEnd == null || this.storyEnd == "")
            this.storyEnd = newStoryEnd;
        Debug.Log("Story end set to: " + this.storyEnd);
    }

    public void SetPlanetFinished(bool finished, int planetNumber)
    {
        switch (planetNumber)
        {
            case 1:
                this.planet1Finished = finished;
                break;
            case 2:
                this.planet2Finished = finished;
                break;
            case 3:
                this.planet3Finished = finished;
                break;
            default:
                Debug.LogError("Invalid planet number: " + planetNumber);
                break;
        }
    }

    public bool gameFinished()
    {
        return planet1Finished && planet2Finished && planet3Finished;
    }

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
        set { fromPlanet = value; 
         Debug.Log("FromPlanet set to: " + value);}
    }

    public int NumberReturnsPlanet0
    {
        get { return numberReturnsPlanet0; } 
        set { numberReturnsPlanet0 = value; 
         Debug.Log("NumberReturnsPlanet0 set to: " + value);}
    }
}