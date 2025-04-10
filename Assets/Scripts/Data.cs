using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class Data : ScriptableObject
{
    public Vector3 marginFromPlayer;
    public List<String> elementsToDetect;

    public string storyEnd;

    public bool planet1Finished = false;
    public bool planet2Finished = false;
    public bool planet3Finished = false;

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
}