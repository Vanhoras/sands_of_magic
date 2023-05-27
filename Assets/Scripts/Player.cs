using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float perspectiveScale;

    [SerializeField]
    private float scaleRatio;

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
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 destination = new Vector3(followSpot.x, followSpot.y, transform.position.z);
        Debug.Log("destination: " + destination);
        agent.SetDestination(destination);

       // AdjustPerspective();
    }

    private void Move(InputAction.CallbackContext input)
    {
        Debug.Log("Move");
        followSpot = Camera.main.ScreenToWorldPoint(inputActions.Player.Position.ReadValue<Vector2>());
        Debug.Log("followSpot: " + followSpot);
    }

    private void AdjustPerspective()
    {
        Vector3 scale = transform.localScale;
        scale.x = perspectiveScale * (scaleRatio - transform.position.y);
        scale.y = perspectiveScale * (scaleRatio - transform.position.y);
        transform.localScale = scale;
    }
}
