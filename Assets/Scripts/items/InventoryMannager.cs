using System.Collections.Generic;
using UnityEngine;

public class InventoryMannager : MonoBehaviour
{
    public static InventoryMannager instance { get; private set; }

    [HideInInspector]
    private readonly List<string> foundItems = new();

    [HideInInspector]
    private bool hasLantern;

    private Player _player;

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

        if (item.itemName == "lantern") ActivateLantern();

        foundItems.Add(item.itemName);
    }

    public bool WasItemAlreadyFound(string itemName) { 
        return foundItems.Contains(itemName);
    }

    private void ActivateLantern()
    {
        this.hasLantern = true;
        _player.ActivateLantern();
    }

    public void SetPlayerInstance(Player player)
    {
        _player = player;
        if (hasLantern) _player.ActivateLantern();
    }
}
