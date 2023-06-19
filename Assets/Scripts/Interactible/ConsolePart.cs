using UnityEngine;

public class ConsolePart : MonoBehaviour
{
    [SerializeField]
    private GameObject light1;

    [SerializeField]
    private GameObject light2;

    [SerializeField]
    private string correctGem;

    [SerializeField]
    private Item gem1;

    [SerializeField]
    private Item gem2;

    [SerializeField]
    private Item gem3;


    private string currentGem = "";

    private void Awake()
    {
        gem1.ItemCollected += RemoveGem;
        gem2.ItemCollected += RemoveGem;
        gem3.ItemCollected += RemoveGem;
    }

    private void OnDestroy()
    {
        gem1.ItemCollected -= RemoveGem;
        gem2.ItemCollected -= RemoveGem;
        gem3.ItemCollected -= RemoveGem;
    }

    public bool HasCorrectGem()
    {
        return (correctGem == "" && (currentGem == null || currentGem == "")) || (currentGem == correctGem);
    }

    public void SetGem(string currentGem)
    {
        this.currentGem = currentGem;

        if (gem1.itemName == currentGem)
        {
            gem1.gameObject.SetActive(true);
        } else if (gem2.itemName == currentGem)
        {
            gem2.gameObject.SetActive(true);
        }
        else if (gem3.itemName == currentGem)
        {
            gem3.gameObject.SetActive(true);
        }

        if (HasCorrectGem())
        {
            ActivateLights();
        }
    }

    public void ActivateLights()
    {
        light1.SetActive(true);
        light2.SetActive(true);
    }

    private void RemoveGem()
    {
        gem1.gameObject.SetActive(false);
        gem2.gameObject.SetActive(false);
        gem3.gameObject.SetActive(false);
        light1.SetActive(false);
        light2.SetActive(false);
        currentGem = "";
        InventoryManager.instance.DropableNoLongerInteracted(transform.name);
    }
}
