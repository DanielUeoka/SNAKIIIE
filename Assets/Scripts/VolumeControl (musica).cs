using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public string mixerParameter = "Musica";

    void OnEnable()
    {
        // Carrega volume salvo
        float savedVolume = PlayerPrefs.GetFloat("VolumeMusica", 0f);

        // Aplica no slider e no mixer
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat(mixerParameter, savedVolume);

        // Remove listeners duplicados e adiciona novo
        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    void HandleVolumeChange(float value)
    {
        audioMixer.SetFloat(mixerParameter, value);
        PlayerPrefs.SetFloat("VolumeMusica", value);
    }
}