using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoAddOptions : MonoBehaviour
{
    public Slider SFXSliderOBJ,
        MusicSliderOBJ;

    public void Start()
    {
        SetSFXSlider();
        SetMusicSlider();
    }

    public void SetSFXSlider()
    {
        //sliderOBJ = this.gameObject.GetComponent<Slider>();
        SFXSliderOBJ.value = GameManagement.instance.sfxVolume;
    }

    public void SetMusicSlider()
    {
        //MusicSliderOBJ = this.gameObject.GetComponentInChildren<Slider>();
        MusicSliderOBJ.value = GameManagement.instance.musicVolume;
    }
    
    public void SFXVolumeChange(float SFXAudio) //changes SFX audio volume
    {
        GameManagement.instance.sfxVolume = SFXAudio;
    }

    public void MusicAudioChange(float MusicAudio) //changes Music audio volume
    {
        GameManagement.instance.musicVolume = MusicAudio;
    }
}
