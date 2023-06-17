using NavMeshPlus.Components;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;

    [SerializeField]
    private GameObject Gem;

    [SerializeField]
    private GameObject Lights;

    [SerializeField]
    public NavMeshSurface surface;

    public void ActivateDoorFirstTime()
    {
        ActivateDoor();
    }

    public void ActivateDoor()
    {
        wall.SetActive(false);
        Gem.SetActive(true);
        Lights.SetActive(true);
        surface.BuildNavMesh();
    }
}
