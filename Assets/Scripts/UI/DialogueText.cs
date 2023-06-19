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

    [SerializeField]
    private LastQuestion lastQuestionField1;

    [SerializeField]
    private LastQuestion lastQuestionField2;

    private string[] textLines;
    private string[] questions;

    private bool lastQuestions;

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

    public void StartDialogue(string[] textLines, string[] questions, Color color, bool lastQuestions)
    {
        this.lastQuestions = lastQuestions;
        //textField.color = color;
        this.textLines = textLines;
        this.questions = questions;
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
        } else if (lastQuestions && questions.Length > currentLine + 1)
        {
            lastQuestionField1.SetLastQuestion(questions[currentLine]);
            lastQuestionField2.SetLastQuestion(questions[currentLine + 1]);
        }
    }

    public void FinishLineOrStartNextLine()
    {
        if (lastQuestions && (!writing && currentLine >= textLines.Length - 1)) return;

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
        return !lastQuestions && (!writing && currentLine >= textLines.Length -1);
    }

    public void EndDialogue()
    {
        StopWriting();

        textField.text = "";
    }
}
