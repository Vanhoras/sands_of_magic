using NavMeshPlus.Components;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;

    [SerializeField]
    private GameObject gem;

    [SerializeField]
    private GameObject lights;

    [SerializeField]
    public NavMeshSurface surface;

    [SerializeField]
    private DoorWay doorWay;

    public void ActivateDoor()
    {
        wall.SetActive(false);
        doorWay.gameObject.SetActive(true);
        gem.SetActive(true);
        if (lights != null)
        {
            lights.SetActive(true);
        }
        surface.BuildNavMesh();
    }
}
