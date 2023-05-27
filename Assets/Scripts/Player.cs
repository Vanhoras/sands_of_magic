using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject dummyTopScale;

    [SerializeField]
    private GameObject dummyBottomScale;

    private PlayerInputActions inputActions;
    private NavMeshAgent agent;

    private Vector2 followSpot;

    // Start is called before the first frame update
    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += Move;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        followSpot = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 destination = new Vector3(followSpot.x, followSpot.y, transform.position.z);
        agent.SetDestination(destination);

        AdjustPerspective();
    }

    private void Move(InputAction.CallbackContext input)
    {
        followSpot = Camera.main.ScreenToWorldPoint(inputActions.Player.Position.ReadValue<Vector2>());
    }

    private void AdjustPerspective()
    {
        Vector3 bottomScale = dummyBottomScale.transform.localScale;
        Vector3 topScale = dummyTopScale.transform.localScale;
        float postionTop = dummyTopScale.transform.position.y;
        float postionBottom = dummyBottomScale.transform.position.y;
        float percentY = (transform.position.y - postionBottom) / (postionTop - postionBottom);
        transform.localScale = Vector3.Lerp(topScale, bottomScale, 1 - percentY);
    }
}
