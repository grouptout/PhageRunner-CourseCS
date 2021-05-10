
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private Slider[] volumeSliders;
    [SerializeField] private bool inGame;

    private readonly List<string> _names = new List<string> {"AllVolume", "MusicVolume", "MenuEffectsVolume", "GameMusicVolume", "GameEffectsVolume"};

     #endregion

    #region MonoBehavoir Callbacks

    private void Start()
    {
        for(var i = 0; i < volumeSliders.Length; i++)
        {
            if (PlayerPrefs.HasKey($"Volumes/{_names[i]}"))
            {
                audioMixerGroup.audioMixer.SetFloat(_names[i], Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat($"Volumes/{_names[i]}")));
                volumeSliders[i].value = PlayerPrefs.GetFloat($"Volumes/{_names[i]}");
            }
            else
            {
                audioMixerGroup.audioMixer.GetFloat(_names[i], out var tmp);
                volumeSliders[i].value = Remap(tmp, -80f, 0f, 0f, 1f);
            }
        }
        
        if (!inGame)
        {
            gameObject.SetActive(false);
        }
    }
    

    #endregion

    #region Private Methods

    private static float Remap(float value,float min1,float max1,float min2,float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
    }

    #endregion
    
    #region Public Methods

    public void ChangeAllVolume(float vol)
    {
        audioMixerGroup.audioMixer.SetFloat("AllVolume", Mathf.Lerp(-80, 0, vol));
        PlayerPrefs.SetFloat("Volumes/AllVolume", vol);
    }
    
    public void ChangeMainMenuMusicVolume(float vol)
    {
        audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, vol));
        PlayerPrefs.SetFloat("Volumes/MusicVolume", vol);
    }
    
    public void ChangeMenuEffectsVolume(float vol)
    {
        audioMixerGroup.audioMixer.SetFloat("MenuEffectsVolume", Mathf.Lerp(-80, 0, vol));
        PlayerPrefs.SetFloat("Volumes/MenuEffectsVolume", vol);
    }
    public void ChangeGameMusicVolume(float vol)
    {
        audioMixerGroup.audioMixer.SetFloat("GameMusicVolume", Mathf.Lerp(-80, 0, vol));
        PlayerPrefs.SetFloat("Volumes/GameMusicVolume", vol);
    }
    public void ChangeGameEffectsVolume(float vol)
    {
        audioMixerGroup.audioMixer.SetFloat("GameEffectsVolume", Mathf.Lerp(-80, 0, vol));
        PlayerPrefs.SetFloat("Volumes/GameEffectsVolume", vol);
    }
    
    #endregion
}
