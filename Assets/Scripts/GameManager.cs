using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Datas gameData;

    void Start()
    {
        Debug.Log("Player Score: " + gameData.AreaSize);
    }
}