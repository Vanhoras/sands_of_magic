using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Item : Interactible
{

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public string itemName;

    private InventoryMannager inventoryMannager;

    private void Start()
    {
        inventoryMannager = FindObjectOfType<InventoryMannager>();
    }

    public void AddToInventory()
    {
        inventoryMannager.AddItem(this);
        Destroy(gameObject);
    }
}
