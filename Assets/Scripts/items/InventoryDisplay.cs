using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    public GameObject cape;

    [SerializeField]
    private ItemDisplay[] itemDisplays;

    private PlayerInputActions inputActions;

    void Awake()
    {
        List<ItemData> items = InventoryManager.instance.GetItemsInInventory();
        
        for (int i = 0; i < items.Count && i < itemDisplays.Length; i++)
        {
            this.itemDisplays[i].gameObject.SetActive(true);
            this.itemDisplays[i].SetItem(items[i]);
        }

        InventoryManager.instance.ItemAddedToInventory += OnItemAdded;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.OpenInventory.performed += OnOpenInventory;
    }

    private void OnDestroy()
    {
        InventoryManager.instance.ItemAddedToInventory -= OnItemAdded;
        inputActions.Player.OpenInventory.performed -= OnOpenInventory;
    }

    private void OnItemAdded(ItemData item)
    {
        OpenCape();

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

    private void OnOpenInventory(InputAction.CallbackContext input)
    {
        ToggleCape();
    }

    public void ToggleCape()
    {
        cape.SetActive(!cape.activeSelf);
    }

    public void OpenCape()
    {
        cape.SetActive(true);
    }

    public void CloseCape()
    {
        cape.SetActive(false);
    }

}
