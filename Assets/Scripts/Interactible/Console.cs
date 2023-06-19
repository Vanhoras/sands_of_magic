using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField]
    private ConsolePart[] consoleParts;

    [SerializeField]
    private DoorActivator doorActivator;

    public void AddGemToConsolePart()
    {
        bool allActivated = true;

        foreach (ConsolePart part in consoleParts)
        {
            if (!part.HasCorrectGem()) allActivated = false; 
        }

        if (allActivated)
        {
            doorActivator.ActivateDoor();

            foreach (ConsolePart part in consoleParts)
            {
                part.ActivateLights();
            }
        }
    }
}
