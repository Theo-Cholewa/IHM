using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class Data : ScriptableObject
{
    public Vector3 marginFromPlayer;
    public List<String> elementsToDetect;

    public string storyEnd;

    public void SetStoryEnd(string newStoryEnd)
    {
        if(this.storyEnd == null || this.storyEnd == "")
            this.storyEnd = newStoryEnd;
        Debug.Log("Story end set to: " + this.storyEnd);
    }
}