using UnityEngine.Events;
using UnityEngine;
using System;

public class ItemDropable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<string> unityEvent;

    [SerializeField]
    private string[] dropableItems;

    [SerializeField]
    private string dropableName;

    private bool startTrigger;

    private void Start()
    {
        string itemName = InventoryManager.instance.WasDropableInteracted(dropableName);

        if (itemName != "")
        {
            startTrigger = true;
            Trigger(itemName);
        }
    }

    public bool IsInteractibleWithItem(string itemName)
    {
        bool ineractibleWithItem = Array.Exists(dropableItems, element => element == itemName);

        bool notOccupied = InventoryManager.instance.WasDropableInteracted(dropableName) == "";

        return ineractibleWithItem && notOccupied;
    }

    public void Trigger(string itemName)
    {
        if (!startTrigger)
        {
            MusicPlayer.instance.PlayOneShot(MusicPlayer.SFXSounds.DOOR);
        }
        unityEvent.Invoke(itemName);
        InventoryManager.instance.DropableWasInteracted(dropableName, itemName);
        startTrigger = false;
    }
}
