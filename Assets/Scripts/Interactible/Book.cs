using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField]
    private string[] bookPages;

    public void OpenBook()
    {
        DialogueBoxManager.instance.OpenBook();
        DialogueBoxManager.instance.OpenDialogue(bookPages, null, Color.white, false);
    }
}
