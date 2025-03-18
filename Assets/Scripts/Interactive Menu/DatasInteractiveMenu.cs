using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class DatasInteractiveMenu : ScriptableObject
{
    public Vector2 areaSize;
    public Vector3 marginFromPlayer;
    public Vector2[] planetsPositions;
    public List<String> elementsToDetect;
}