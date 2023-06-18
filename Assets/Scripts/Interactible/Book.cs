using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField]
    private string[] bookPages;

    public void OpenBook()
    {
        DialogueBoxManager.instance.OpenBook();
        DialogueBoxManager.instance.OpenDialogue(bookPages, null);
    }
}
