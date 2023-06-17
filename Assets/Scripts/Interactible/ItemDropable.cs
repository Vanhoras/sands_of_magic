using UnityEngine.Events;
using UnityEngine;
using System;

public class ItemDropable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;

    [SerializeField]
    private string[] interactibleItems;

    [SerializeField]
    private string interactibleName;

    private bool startTriger;

    private void Start()
    {
        if(InventoryManager.instance.WasInteractibleInteracted(interactibleName))
        {
            startTriger = true;
            Trigger();
        }
    }

    public bool IsInteractibleWithItem(string itemName)
    {
        return Array.Exists(interactibleItems, element => element == itemName);
    }

    public void Trigger()
    {
        if (!startTriger)
        {
            MusicPlayer.instance.PlayOneShot(MusicPlayer.SFXSounds.DOOR);
        }
        unityEvent.Invoke();
        InventoryManager.instance.InteractibleWasInteracted(interactibleName);
        startTriger = false;
    }
}
