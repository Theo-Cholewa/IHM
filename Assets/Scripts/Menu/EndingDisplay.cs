using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndingDisplay : MonoBehaviour
{
    public string endingName; // Le nom de la fin (ex: "colere", "bonne_fin")
    public EndingManager endingManager; // À drag & drop dans l'inspecteur
    public TextMeshProUGUI textComponent; // Le texte de la fin
    public Image backgroundImage; // Le fond coloré

    private Dictionary<string, Color> endingColors = new Dictionary<string, Color>()
    {
        { "paresse", new Color(0.5f, 0.5f, 1f) },       // Bleu doux
        { "colere", Color.red },                        // Rouge vif
        { "luxure", new Color(0.9f, 0.3f, 0.6f) },       // Rose intense
        { "envie", new Color(0.3f, 0.8f, 0.3f) },        // Vert jalousie
        { "avarice", new Color(1f, 0.85f, 0.3f) },       // Or
        { "orgueil", new Color(0.6f, 0.2f, 1f) },        // Violet royal
        { "gourmandise", new Color(1f, 0.6f, 0.2f) },    // Orange sucré
        { "bonne_fin", new Color(0.5f, 0.2f, 0.7f) }         // Jaune lumineux
    };

    void Start()
    {
        if (endingManager == null || textComponent == null || backgroundImage == null)
        {
            Debug.LogError("Veuillez lier tous les composants dans l'inspecteur.");
            return;
        }

        string key = endingName.ToLower();
        bool isUnlocked = endingManager.GetAllEndings().ContainsKey(key) && endingManager.GetAllEndings()[key];
        textComponent.text = isUnlocked ? key : "???";

        backgroundImage.color = isUnlocked && endingColors.ContainsKey(key) ? endingColors[key] : Color.gray;
    }

}
