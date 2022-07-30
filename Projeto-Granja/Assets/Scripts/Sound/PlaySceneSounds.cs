using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneSounds : MonoBehaviour
{
    public string[] musicName;
    public bool stopOtherMusics;
    public bool stopEffects;
    // Start is called before the first frame update
    void Start()
    {
        if (stopOtherMusics)
            AudioManager.instance.StopAllMusicSounds();
        if(stopEffects)
            AudioManager.instance.StopAllEffectsSounds();
        foreach (string item in musicName)
        {
            AudioManager.instance.Play(item);
        }
    }
}
