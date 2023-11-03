using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance { get; private set; }

    [SerializeField]
    private Texture2D cursorDefaultTexture;

    [SerializeField]
    private Texture2D cursorPointerTexture;


    private Vector2 cursorHotSpotDefault;
    private Vector2 cursorHotSpotPointer;

    private PlayerInputActions inputActions;
    private bool hovering;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    

    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        cursorHotSpotDefault = Vector2.zero;
        cursorHotSpotPointer = new Vector2(cursorPointerTexture.width / 2, cursorPointerTexture.height / 2);

        SetCursorToDefault();
    }

    private void Update()
    {
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(inputVector);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        bool foundInteractible = false;

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

            if (hitObject.GetComponent<Interactible>() != null
                || hitObject.GetComponent<DoorWay>() != null)
            {
                foundInteractible = true;
                break;
            }
        }

        if (foundInteractible && !hovering)
        {
            hovering = true;
            SetCursorToPointer();
        }
        else if (!foundInteractible && hovering)
        {
            hovering = false;
            SetCursorToDefault();
        }
    }

    public void SetCursorToDefault()
    {
        Cursor.SetCursor(cursorDefaultTexture, cursorHotSpotDefault, CursorMode.Auto);
    }

    public void SetCursorToPointer()
    {
        Cursor.SetCursor(cursorPointerTexture, cursorHotSpotPointer, CursorMode.Auto);
    }
}
