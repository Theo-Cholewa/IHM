using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EndingManager : MonoBehaviour
{
    private EndingsData endingsData;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Resources/end.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            endingsData = JsonUtility.FromJson<EndingsData>(json);
        }
        else
        {
            Debug.LogWarning("Fichier de fins non trouvé, création d'une nouvelle structure.");
            endingsData = new EndingsData();
            SaveData();
        }

        // Vérification et affichage des fins débloquées au démarrage
        if (endingsData != null)
        {
            foreach (var field in typeof(EndingsData).GetFields())
            {
                bool value = (bool)field.GetValue(endingsData);
                if (value) // Affiche uniquement les fins débloquées
                {
                    Debug.Log("Fin débloquée : " + field.Name);
                }
            }
        }
        else
        {
            Debug.LogError("endingsData est NULL au démarrage !");
        }

        // Vérification de SceneDataTransfer
        if (SceneDataTransfer.Instance == null)
        {
            Debug.LogError("SceneDataTransfer instance not found in the scene!");
        }
        else if (!string.IsNullOrEmpty(SceneDataTransfer.Instance.storyEnd))
        {
            SetEnding(SceneDataTransfer.Instance.storyEnd, true);
        }
    }

    void Update()
    {
        // Quitter l'application si la touche Échap est pressée
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    public void SetEnding(string name, bool value)
    {
        if (endingsData == null) return;

        var field = typeof(EndingsData).GetField(name);
        if (field != null)
        {
            field.SetValue(endingsData, value);
            SaveData();
        }
        else
        {
            Debug.LogWarning("Nom de fin invalide : " + name);
        }
    }

    public bool GetEnding(string name)
    {
        var field = typeof(EndingsData).GetField(name);
        if (field != null)
        {
            return (bool)field.GetValue(endingsData);
        }

        Debug.LogWarning("Nom de fin invalide : " + name);
        return false;
    }

    public Dictionary<string, bool> GetAllEndings()
    {
        Dictionary<string, bool> dict = new Dictionary<string, bool>();

        if (endingsData == null)
        {
            //Debug.LogError("endingsData est NULL !");
            return dict; // Retourne un dictionnaire vide pour éviter l'erreur
        }

        foreach (var field in typeof(EndingsData).GetFields())
        {
            dict[field.Name] = (bool)field.GetValue(endingsData);
        }

        return dict;
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(endingsData, true);
        File.WriteAllText(filePath, json);
    }

    // Méthode publique pour quitter l'application
    public void QuitApplication()
    {
        Debug.Log("Application fermée !");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Arrête le jeu dans l'éditeur
        #else
            Application.Quit();  // Ferme l'application en mode build
        #endif
    }
}