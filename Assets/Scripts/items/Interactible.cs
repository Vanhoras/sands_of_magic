using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{

    [SerializeField]
    private UnityEvent unityEvent;

    public void Trigger()
    {
        unityEvent.Invoke();
    }
}
