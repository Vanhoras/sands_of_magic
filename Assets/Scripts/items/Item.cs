using UnityEngine;

public class Item : Interactible
{
    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public bool deleteOnRemove = true;

    [SerializeField]
    public bool inConsole = false;

    public delegate void ItemCollectedEventHandler();
    public event ItemCollectedEventHandler ItemCollected;

    private void Start()
    {
        if (InventoryManager.instance.WasItemAlreadyFound(itemName) && !inConsole)
        {
            Remove();
        }
    }

    public void AddToInventory()
    {
        MusicPlayer.instance.PlayOneShot(MusicPlayer.SFXSounds.PICKUP);
        InventoryManager.instance.AddItem(this);
        ItemCollected?.Invoke();
        Remove();
    }

    private void Remove()
    {
        if (deleteOnRemove)
        {
            Destroy(gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

}
