using UnityEngine;

public class Item : Interactible
{
    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public string itemName;

    private void Start()
    {
        if (InventoryManager.instance.WasItemAlreadyFound(itemName))
        {
            Destroy(gameObject);
        }
    }

    public void AddToInventory()
    {
        MusicPlayer.instance.PlayOneShot(MusicPlayer.SFXSounds.PICKUP);
        InventoryManager.instance.AddItem(this);
        Destroy(gameObject);
    }

}
