using UnityEngine;

public class Item : Interactible
{

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public string itemName;

    private void Start()
    {
        if (InventoryMannager.instance.WasItemAlreadyFound(itemName))
        {
            Destroy(gameObject);
        }
    }

    public void AddToInventory()
    {
        InventoryMannager.instance.AddItem(this);
        Destroy(gameObject);
    }
}
