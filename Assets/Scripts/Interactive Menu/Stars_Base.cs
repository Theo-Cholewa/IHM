using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Stars : MonoBehaviour
{
    [FormerlySerializedAs("gameData")] public DatasInteractiveMenu gameDataInteractiveMenu;
    public int starCount = 5000;
    private Mesh mesh;
    private Vector3[] stars;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer.material == null)
        {
            meshRenderer.material = new Material(Shader.Find("Particles/Unlit"));
        }

        mesh = new Mesh();
        stars = new Vector3[starCount*5];

        for (int i = 0; i < starCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-gameDataInteractiveMenu.areaSize.x, gameDataInteractiveMenu.areaSize.x),
                Random.Range(-100f, 0),
                Random.Range(-gameDataInteractiveMenu.areaSize.y, gameDataInteractiveMenu.areaSize.y)
            );
            stars[i] = pos;
            stars[i + starCount] = pos + new Vector3(gameDataInteractiveMenu.areaSize.x*2, 0, 0);
            stars[i + starCount*2] = pos + new Vector3(0, 0, gameDataInteractiveMenu.areaSize.y*2);
            stars[i + starCount*3] = pos + new Vector3(-gameDataInteractiveMenu.areaSize.x*2, 0, 0);
            stars[i + starCount*4] = pos + new Vector3(0, 0,-gameDataInteractiveMenu.areaSize.y*2);
        }

        mesh.vertices = stars;
        mesh.SetIndices(System.Linq.Enumerable.Range(0, starCount*5).ToArray(), MeshTopology.Points, 0);
        meshFilter.mesh = mesh;
    }
}