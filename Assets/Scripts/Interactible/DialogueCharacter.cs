using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField]
    private string[] dialogueLines;

    [SerializeField]
    private string[] questions;

    [SerializeField]
    private Color color = Color.white;

    [SerializeField]
    private bool lastQuestions = false;

    public void StartDialogue()
    {
        DialogueBoxManager.instance.OpenDialogue(dialogueLines, questions, color, lastQuestions);
    }
}
