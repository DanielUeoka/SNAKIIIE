using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixers : MonoBehaviour
{

    public AudioMixer MixadorAudio;
    public Slider sliderMenuPrincipal;
    public Slider sliderMenuPause;
    private bool isUpdating = false;

    void start()
    {
        float volumeinicial = PlayerPrefs.GetFloat("VolumeMusica", 0f);
        MixadorAudio.SetFloat("Musica", volumeinicial);

        sliderMenuPrincipal.value = volumeinicial;
        sliderMenuPause.value = volumeinicial;

        sliderMenuPrincipal.onValueChanged.AddListener(OnSliderMenuPrincipalChanged);
        sliderMenuPause.onValueChanged.AddListener(OnSliderMenuPauseChanged);
    }

    void OnSliderMenuPrincipalChanged(float valor)
    {
        Debug.Log("Slider do menu principal mudou para: " + valor);
        if (isUpdating) return;
        isUpdating = true;
        sliderMenuPause.value = valor;
        SetMusicaVol(valor);
        isUpdating = false;
    }

    void OnSliderMenuPauseChanged(float valor)
    {
        if (isUpdating) return;
        isUpdating = true;
        sliderMenuPrincipal.value = valor;
        SetMusicaVol(valor);
        isUpdating = false;
    }

    public void SetMusicaVol(float vol) //Função que ajusta o nivel de volume da musica do jogo
    {
        isUpdating = true;
        MixadorAudio.SetFloat("Musica", vol);
        PlayerPrefs.SetFloat("VolumeMusica", vol);
    }

    public void SetSFXVol(float vol) //Função qua ajusta o nivel de volume dos sound effects do jogo
    {
        MixadorAudio.SetFloat("SFX", vol);
        PlayerPrefs.SetFloat("VolumeSFX", vol);
    }


}
