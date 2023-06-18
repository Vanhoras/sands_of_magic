using UnityEngine;
using UnityEngine.InputSystem;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private ItemData item;
    private PlayerInputActions inputActions;

    private Vector3 startPosition;
    private bool moving;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnMouseClick;
        inputActions.Player.Click.canceled += OnMouseLetGo;
    }

    private void Update()
    {
        if (moving)
        {
            Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();

            Vector2 moveToPosition = Camera.main.ScreenToWorldPoint(inputVector);

            transform.position = new Vector3(moveToPosition.x, moveToPosition.y, 0);
        }
    }

    private void OnDestroy()
    {
        inputActions.Player.Click.performed -= OnMouseClick;
        inputActions.Player.Click.canceled -= OnMouseLetGo;
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        spriteRenderer.sprite = item.sprite;
    }

    public void RemoveItem()
    {
        this.item = null;
        spriteRenderer.sprite = null;
        gameObject.SetActive(false);
    }

    private void OnMouseClick(InputAction.CallbackContext input)
    {
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(inputVector);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }

            if (hit.collider.tag == this.tag)
            {
                moving = true;
                startPosition = transform.localPosition;
                break;
            }
        }
    }

    private void OnMouseLetGo(InputAction.CallbackContext input)
    {
        if (!moving) return;

        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(inputVector);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject == null)
            {
                continue;
            }

            ItemDropable itemDropable = hitObject.GetComponent<ItemDropable>();

            if (itemDropable != null)
            {
                if (itemDropable.IsInteractibleWithItem(item.itemName))
                {
                    itemDropable.Trigger(item.itemName);
                    transform.localPosition = startPosition;
                    moving = false;
                    InventoryManager.instance.RemoveItem(item);
                    RemoveItem();
                }
                break;
            }
        }


        if (moving) {
            transform.localPosition = startPosition;
        }
        moving = false;
    }
}
