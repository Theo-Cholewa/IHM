using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/DataPlanet3", order = 1)]
public class DataPlanet3 : PlanetData
{
    public List<string> pickUpItems = new List<string>(); // Liste des éléments à ramasser
    public List<string> stepDialogues = new List<string>(); // Liste des étapes des dialogues

    public void AddPickUpItem(string newItem)
    {
        pickUpItems.Add(newItem); // Ajouter le nouvel élément
    }

}
