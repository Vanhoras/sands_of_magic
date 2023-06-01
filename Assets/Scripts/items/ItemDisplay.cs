using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    private ItemData item;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        image.sprite = item.sprite;
    }
}
