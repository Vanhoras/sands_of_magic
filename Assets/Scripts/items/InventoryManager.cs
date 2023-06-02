using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [HideInInspector]
    private readonly List<string> foundItems = new();

    [HideInInspector]
    private readonly List<ItemData> itemsInInventory = new();

    [HideInInspector]
    private bool hasLantern;

    [HideInInspector]
    private readonly List<string> interactiblesInteracted = new();

    public delegate void ItemAddedEventHandler(ItemData itemData);
    public delegate void EmptyEventHandler();

    public event ItemAddedEventHandler ItemAddedToInventory;
    public event EmptyEventHandler LanternActivated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        if (item.itemName == "lantern") {
            ActivateLantern();
        }
        else
        {
            ItemData itemData = ItemData.CreateFromItem(item);
            itemsInInventory.Add(itemData);
            ItemAddedToInventory?.Invoke(itemData);
        }

        foundItems.Add(item.itemName);
    }

    public void RemoveItem(ItemData item)
    {
        if (item == null) return;

        itemsInInventory.Remove(item);
    }

    public bool WasItemAlreadyFound(string itemName) { 
        return foundItems.Contains(itemName);
    }

    private void ActivateLantern()
    {
        this.hasLantern = true;
        LanternActivated?.Invoke();
    }

    public bool IsLanternActive() { return this.hasLantern; }

    public void InteractibleWasInteracted(string interactibleName)
    {
        interactiblesInteracted.Add(interactibleName);
    }

    public bool WasInteractibleInteracted(string interactibleName)
    {
        return interactiblesInteracted.Contains(interactibleName);
    }

    public List<ItemData> GetItemsInInventory()
    {
        return itemsInInventory;
    }
}
