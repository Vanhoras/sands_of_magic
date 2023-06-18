using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{

    private AudioSource musicSource;
    public static DialoguePlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartDialogue()
    {
        musicSource.Play();
    }

    public void StopDialogue()
    {
        musicSource.Stop();
    }
}
