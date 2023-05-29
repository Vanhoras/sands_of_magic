using UnityEngine;

public class InventoryMannager : MonoBehaviour
{
    public static InventoryMannager SharedInstance { get; private set; }

    private Player _player;
    public Player PlayerInstance
    {
        get { return _player; }
        set { 
            _player = value;
            if (HasLantern) _player.ActivateLantern();
        }
    }

    private bool _hasLantern;
    public bool HasLantern
    {
        get { return _hasLantern; }
        set { _hasLantern = value; }
    }

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        if (item.itemName == "lantern") ActivateLantern();
    }

    private void ActivateLantern()
    {
        HasLantern = true;
        _player.ActivateLantern();
    }
}
