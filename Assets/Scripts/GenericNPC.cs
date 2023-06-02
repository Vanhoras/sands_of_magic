using System.Collections;
using UnityEngine;

public class GenericNPC : MonoBehaviour
{
    [SerializeField]
    private string[] dialogLines;

    [SerializeField]
    private float quipDuration = 3f;

    private int currentLine = 0;

    private TMPro.TextMeshProUGUI m_TextMeshPro;

    private Coroutine quipCoroutine;

    private void Awake()
    {
        m_TextMeshPro = GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        m_TextMeshPro.transform.parent.gameObject.SetActive(true);
    }

    public void OutputDialogLine()
    {
        if (dialogLines.Length == 0) return;

        if (quipCoroutine != null) {
            StopCoroutine(quipCoroutine);
            quipCoroutine = null;
        }

        quipCoroutine = StartCoroutine(Quip());
    }

    
    private IEnumerator Quip()
    {
        m_TextMeshPro.text = dialogLines[currentLine];
        currentLine = currentLine + 1 == dialogLines.Length ? 0 : currentLine + 1;
        yield return new WaitForSeconds(quipDuration);

        m_TextMeshPro.text = "";
        quipCoroutine = null;
        yield break;
    }
}
