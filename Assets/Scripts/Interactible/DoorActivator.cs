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

    public void ActivateDoor()
    {
        wall.SetActive(false);
        Gem.SetActive(true);
        if (Lights != null)
        {
            Lights.SetActive(true);
        }
        surface.BuildNavMesh();
    }
}
