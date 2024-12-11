using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    public AudioMixer aM;
    public Slider mvs;
    public Slider muvs;
    public Slider sfvs;

    private void Start()
    {
        float value;
        aM.GetFloat("MasterVolume", out value);
        mvs.value = Mathf.Pow(10, value / 20);
        aM.GetFloat("MusicVolume", out value);
        muvs.value = Mathf.Pow(10, value / 20);
        aM.GetFloat("SFXVolume", out value);
        sfvs.value = Mathf.Pow(10, value / 20);
    }

    public void changeMaster(float value)
    {
        aM.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
    public void changeMusic(float value)
    {
        aM.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
    public void changeSFX(float value)
    {
        aM.SetFloat("SFXVolume", Mathf.Log10(value)*20);
    }
}
