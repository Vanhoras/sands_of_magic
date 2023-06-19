using TMPro;
using UnityEngine;

public class LastQuestion : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private GameObject button;

    public void SetLastQuestion(string question)
    {
        button.SetActive(true);
        textField.text = question;
    }
}
