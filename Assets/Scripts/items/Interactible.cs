using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{

    [SerializeField]
    private UnityEvent unityEvent;

    [SerializeField]
    private float distance;

    public void Trigger()
    {
        unityEvent.Invoke();
    }

    public float GetDistance() { return distance; }


    
}
