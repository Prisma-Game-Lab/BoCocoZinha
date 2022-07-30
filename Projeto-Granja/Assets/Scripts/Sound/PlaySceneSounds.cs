using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneSounds : MonoBehaviour
{
    public string[] musicName;
    public bool stopOtherMusics;
    // Start is called before the first frame update
    void Start()
    {
        if (stopOtherMusics)
            AudioManager.instance.StopAllMusicSounds();
        foreach (string item in musicName)
        {
            AudioManager.instance.Play(item);
        }
        
    }
}
