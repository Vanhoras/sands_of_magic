using UnityEngine;

public class ItemData
{
    public Sprite sprite;

    public string itemName;

    public static ItemData CreateFromItem(Item item)
    {
        ItemData data = new ItemData();
        data.sprite = item.sprite;
        data.itemName = item.itemName;
        return data;
    }
}
