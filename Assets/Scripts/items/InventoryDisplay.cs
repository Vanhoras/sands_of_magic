using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private ItemDisplay[] itemDisplays;

    void Start()
    {
        List<ItemData> items = InventoryManager.instance.GetItemsInInventory();
        
        for (int i = 0; i < items.Count && i < itemDisplays.Length; i++)
        {
            this.itemDisplays[i].gameObject.SetActive(true);
            this.itemDisplays[i].SetItem(items[i]);
        }

        InventoryManager.instance.ItemAddedToInventory += OnItemAdded;
    }

    private void OnDestroy()
    {
        InventoryManager.instance.ItemAddedToInventory -= OnItemAdded;
    }

    private void OnItemAdded(ItemData item)
    {
        for (int i = 0; i < itemDisplays.Length; i++)
        {
            if (!this.itemDisplays[i].isActiveAndEnabled)
            {
                this.itemDisplays[i].gameObject.SetActive(true);
                this.itemDisplays[i].SetItem(item);
                break;
            }
        }
    }

}
