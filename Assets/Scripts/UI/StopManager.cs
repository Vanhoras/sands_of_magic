using UnityEngine;

public class StopManager : MonoBehaviour
{
    public static StopManager instance { get; private set; }

    [HideInInspector]
    public bool stopped;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
