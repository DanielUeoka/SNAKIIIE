using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXVolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public string mixerParameter = "SFX"; // Esse é o nome do parâmetro no AudioMixer

    void OnEnable()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumeSFX", 0f);

        volumeSlider.value = savedVolume;
        audioMixer.SetFloat(mixerParameter, savedVolume);

        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    void HandleVolumeChange(float value)
    {
        audioMixer.SetFloat(mixerParameter, value);
        PlayerPrefs.SetFloat("VolumeSFX", value);
    }
}