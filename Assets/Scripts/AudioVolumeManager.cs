using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeManager : MonoBehaviour
{
    public AudioMixer aM;
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
