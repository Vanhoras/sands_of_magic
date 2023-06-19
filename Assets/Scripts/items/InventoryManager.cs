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
    private readonly List<InteractedDropables> dropablesInteracted = new();

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

    public void DropableWasInteracted(string dropableName, string itemName)
    {
        dropablesInteracted.Add(new InteractedDropables(dropableName, itemName));
    }

    public void DropableNoLongerInteracted(string dropableName)
    {
        foreach (var dropable in dropablesInteracted)
        {
            if (dropable.dropableName == dropableName)
            {
                dropablesInteracted.Remove(dropable);
                break;
            }
        }
    }

    public string WasDropableInteracted(string dropableName)
    {
        string itemName = "";
        foreach (var dropable in dropablesInteracted)
        {
            if (dropable.dropableName == dropableName)
            {
                itemName = dropable.itemName; break;
            }
        }

        return itemName;
    }

    public List<ItemData> GetItemsInInventory()
    {
        return itemsInInventory;
    }
}

public struct InteractedDropables
{
    public string dropableName;
    public string itemName;

    public InteractedDropables (string dropableName, string itemName)
    {
        this.dropableName = dropableName;
        this.itemName = itemName;
    }
}
