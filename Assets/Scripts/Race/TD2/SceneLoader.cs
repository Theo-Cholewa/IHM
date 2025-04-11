using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public DataPlanet3 dataPlanet3;
    public void PlayGame()
    {
        dataPlanet3.Reset();
        SceneDataTransfer.Instance.Reset();
        SceneManager.LoadScene("Planete0");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
