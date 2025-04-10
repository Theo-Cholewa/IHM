using UnityEngine;
using UnityEngine.Serialization;

namespace INDICATOR
{
    public class InitialPositionSpaceShip: MonoBehaviour
    {
        private Rigidbody rg;
        [FormerlySerializedAs("gameDatas")] public DatasInteractiveMenu gameDatasInteractiveMenu;

        void Start()
        {
            rg = GetComponent<Rigidbody>();
            if (SceneDataTransfer.Instance != null)
            {
                Vector2 pos = gameDatasInteractiveMenu.planetsPositions[SceneDataTransfer.Instance.FromPlanet] +
                              new Vector2(10, 0);
                rg.position = new Vector3(pos.x, 0, pos.y);
            }
            else
            {
                rg.position = Vector3.zero;
            }
        }
    }
}