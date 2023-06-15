using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    [SerializeField]
    private AudioClip[] musicClips;

    private AudioSource musicSource;
    public enum MusicState
    {
        Ashara,
        Shaara
    }
    [SerializeField] 
    private MusicState currentMusicState;

    /*
    public enum UISounds
    {
        Open,
        Hover,
        Select,
        Back,
        Close
    }*/

    [SerializeField] 
    private AudioClip[] sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        Play(currentMusicState);
    }

    public void Play(MusicState st)
    {
        if (musicClips.Length <= 0) { return; }
        switch (st)
        {
            case MusicState.Ashara:
                currentMusicState = MusicState.Ashara;
                musicSource.clip = musicClips[0];
                musicSource.Play();
                break;
            case MusicState.Shaara:
                currentMusicState = MusicState.Shaara;
                musicSource.clip = musicClips[1];
                musicSource.Play();
                break;
            default:
                break;
        }
    }

    public void Stop()
    {
        StartCoroutine(FadeOut(1f));
    }

    public void Destroy()
    {
        StartCoroutine(FadeOut(1f, true));
    }

    /*
    public void PlayOneShot(UISounds sound)
    {
        switch (sound)
        {
            case UISounds.Open:
                musicSource.PlayOneShot(uiSounds[0]);
                break;
            case UISounds.Hover:
                musicSource.PlayOneShot(uiSounds[1]);
                break;
            case UISounds.Select:
                musicSource.PlayOneShot(uiSounds[2]);
                break;
            case UISounds.Back:
                musicSource.PlayOneShot(uiSounds[3]);
                break;
            case UISounds.Close:
                musicSource.PlayOneShot(uiSounds[4]);
                break;
            default:
                break;
        }

    }
    */

    private IEnumerator FadeOut(float duration, bool destroy = false)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }

        musicSource.Stop();
        musicSource.clip = null;
        musicSource.volume = startVolume;

        if (destroy)
        {
            Destroy(this.gameObject);
        }

        yield break;
    }
}
