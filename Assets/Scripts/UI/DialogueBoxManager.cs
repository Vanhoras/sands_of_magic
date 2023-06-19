using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBoxManager : MonoBehaviour
{
    public static DialogueBoxManager instance { get; private set; }

    [SerializeField]
    private DialogueText dialogueBox;

    [SerializeField]
    private GameObject bookPage;

    private PlayerInputActions inputActions;

    private bool open;

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

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnMouseClick;
    }

    private void Open()
    {
        open = true;
        StopManager.instance.stopped = true;
    }

    private void Close()
    {
        open = false;
        StopManager.instance.stopped = false;
    }

    public void OpenBook()
    {
        Open();
        this.bookPage.SetActive(true);
    }

    public void OpenDialogue(string[] dialogue, string[] questions, Color color, bool lastQuestions)
    {
        Open();
        this.dialogueBox.gameObject.SetActive(true);
        this.dialogueBox.StartDialogue(dialogue, questions, color, lastQuestions);
        
    }

    public void CloseDialogue()
    {
        this.bookPage.SetActive(false);
        this.dialogueBox.gameObject.SetActive(false);
        this.dialogueBox.EndDialogue();
        Close();
    }

    private void OnMouseClick(InputAction.CallbackContext input)
    {
        if (!open) return;

        if (this.dialogueBox.IsFinished())
        {
            CloseDialogue();
        }
        else
        {
           this.dialogueBox.FinishLineOrStartNextLine();
        }
    }
}
