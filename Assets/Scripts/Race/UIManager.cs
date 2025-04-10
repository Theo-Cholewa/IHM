using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public TextMeshProUGUI result;
    
    void Start()
    {
        text.SetText("");
        result.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Change Escape to any key you want
        {
            QuitGame();
        }
    }

    public void UpdateLapText(string message)
    {
        text.SetText(message);
    }

    public void Result(string message)
    {
        result.SetText(message);
    }
    
    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // This helps for debugging in the editor
        Application.Quit();
    }
}
