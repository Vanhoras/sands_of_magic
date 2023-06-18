using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField]
    private string[] dialogueLines;

    [SerializeField]
    private string[] questions;

    public void StartDialogue()
    {
        DialogueBoxManager.instance.OpenDialogue(dialogueLines, questions);
    }
}
