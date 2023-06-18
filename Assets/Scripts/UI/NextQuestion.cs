using TMPro;
using UnityEngine;

public class NextQuestion : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questionField;

    [SerializeField]
    private GameObject NextImage;

    private bool show;

    private void Update()
    {
        if (!show) return;


    }

    public void ShowQuestion(string questionText)
    {
        this.show = true;

        this.questionField.gameObject.SetActive(true);
        this.questionField.text = questionText;

        this.NextImage.SetActive(true);
    }

    public void StopShowingQuestion()
    {
        this.show = false;

        this.questionField.gameObject.SetActive(false);
        this.questionField.text = "";

        this.NextImage.SetActive(false);
    }
}
