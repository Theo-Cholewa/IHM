using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public DataPlanet3 dataPlanet3;
    public void PlayGame()
    {
        dataPlanet3.Reset();
        SceneManager.LoadScene("Planete0");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
