using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private NextQuestion next;

    private string[] textLines;
    private string[] questions;

    private bool writing;

    private int currentLine;
    private int currentLetter;

    private float nextUpdate;

    // Update is called once per frame
    private void Update()
    {
        if (!writing) { return; }

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + speed;
            textField.text += textLines[currentLine][currentLetter];

            currentLetter++;

            if (currentLetter >= textLines[currentLine].Length)
            {
                currentLetter = 0;
                StopWriting();
            }
        }
    }

    public void StartDialogue(string[] textLines, string[] questions)
    {
        this.textLines = textLines;
        currentLine = 0;
        StartWriting();
    }

    private void StartWriting()
    {
        textField.text = "";
        currentLetter = 0;
        writing = true;
        DialoguePlayer.instance.StartDialogue();
        next.StopShowingQuestion();
    }

    private void StopWriting()
    {
        writing = false;
        DialoguePlayer.instance.StopDialogue();

        if (currentLine <  textLines.Length -1)
        {
            next.ShowQuestion(questions != null && questions.Length > currentLine ? questions[currentLine] : "");
        }
    }

    public void FinishLineOrStartNextLine()
    {
        if (writing)
        {
            textField.text = textLines[currentLine];
            StopWriting();
        } else
        {
            currentLine++;
            StartWriting();
        }
    }

    public bool IsFinished()
    {
        return !writing && currentLine >= textLines.Length -1;
    }

    public void EndDialogue()
    {
        StopWriting();

        textField.text = "";
    }
}
