using UnityEngine;
using UnityEngine.Serialization;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab; // Assigne un prefab avec un MeshRenderer
    public int starCount = 50000; // Nombre d'étoiles
    [FormerlySerializedAs("gameData")] public DatasInteractiveMenu gameDataInteractiveMenu;
    public Vector2 rangeY;

    void Start()
    {
        GenerateStars();
    }

    void GenerateStars()
    {
        Vector3[] positions = new Vector3[starCount*5];

        for (int i = 0; i < starCount; i++)
        {

            Vector3 pos = new Vector3(
                Random.Range(-gameDataInteractiveMenu.areaSize.x, gameDataInteractiveMenu.areaSize.x),
                Random.Range(rangeY.x, rangeY.y),
                Random.Range(-gameDataInteractiveMenu.areaSize.y, gameDataInteractiveMenu.areaSize.y)
            );

            positions[0] = pos;
            positions[1] = pos + new Vector3(gameDataInteractiveMenu.areaSize.x*2, 0, 0);
            positions[2] = pos + new Vector3(0, 0, gameDataInteractiveMenu.areaSize.y*2);
            positions[3] = pos + new Vector3(-gameDataInteractiveMenu.areaSize.x*2, 0, 0);
            positions[4] = pos + new Vector3(0, 0,-gameDataInteractiveMenu.areaSize.y*2);

            Vector3 size = Vector3.one * Random.Range(0.1f, 2f) * (Mathf.Abs(gameDataInteractiveMenu.marginFromPlayer.y-pos.y)/Mathf.Abs(rangeY.x-gameDataInteractiveMenu.marginFromPlayer.y));
            
            for (int k = 0; k < 5; k++)
            {
                GameObject star = Instantiate(starPrefab, positions[k], Quaternion.identity);
                star.transform.localScale = Vector3.one;
                //star.transform.localScale = size; 
                star.isStatic = true; 
            }
        }

        StaticBatchingUtility.Combine(gameObject);
    }
}