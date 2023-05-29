using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private NavMeshAgent agent;

    private Vector2 followSpot;
    private bool moving;

    private Vector3 lastPosition;
    private float checkLastPositionEvery = 0.1f;
    private float timeTillCheckPosition;

    private Interactible interactible;

    private PlayerParent playerParent;
    private GameObject dummyTopScale;
    private GameObject dummyBottomScale;


    private void Start()
    {
        playerParent = transform.parent.GetComponent<PlayerParent>();
        dummyTopScale = playerParent.dummyTopScale;
        dummyBottomScale = playerParent.dummyBottomScale;


        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnMouseClick;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        lastPosition = transform.position;

        AdjustPerspective();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!moving) return;

        AdjustPerspective();
        ChangeDirection();

        if (AgentHasStopped())
        {
            moving = false;
            if (interactible != null) {
                interactible.Trigger();
                interactible = null;
            }
        }
    }

    private void OnMouseClick(InputAction.CallbackContext input)
    {
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();
        MoveStart(inputVector);

        FindInteractible(inputVector);
    }

    private void MoveStart(Vector2 inputVector)
    {
        moving = true;

        followSpot = Camera.main.ScreenToWorldPoint(inputVector);

        Vector3 destination = new Vector3(followSpot.x, followSpot.y, transform.position.z);
        agent.SetDestination(destination);
    }

    private void FindInteractible(Vector2 inputVector)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputVector);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject != null && hitObject.GetComponent<Interactible>() != null)
                {
                    Debug.Log("interactible found");
                    interactible = hitObject.GetComponent<Interactible>();
                }
            }
        }
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

    private void ChangeDirection()
    {

        Vector3 directionVector = transform.position - lastPosition;

        if (directionVector.x == 0) return;

        bool left = directionVector.x < 0;

        transform.rotation = left ? transform.rotation = Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private bool AgentHasStopped()
    {
        timeTillCheckPosition += Time.deltaTime;

        if (timeTillCheckPosition < checkLastPositionEvery) return false;

        timeTillCheckPosition = 0;

        bool hasStopped = transform.position == lastPosition;

        lastPosition = transform.position;

        return hasStopped;
    }

    private void OnDestroy()
    {
        inputActions.Player.Click.performed -= OnMouseClick;
    }
}
