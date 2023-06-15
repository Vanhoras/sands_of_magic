using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{

    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        Slider slider = gameObject.GetComponent<Slider>();

        float currentVolume;
        audioMixer.GetFloat("MusicVolume", out currentVolume);

        slider.value = Mathf.Pow(10, currentVolume / 20);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
