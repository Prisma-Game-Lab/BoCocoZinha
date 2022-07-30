using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	//public AudioMixerGroup mixerGroup;

	public Sound[] soundMusics;
	public Sound[] soundEffects;
	public static AudioManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		foreach (Sound s in soundMusics)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
		foreach (Sound s in soundEffects)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(soundMusics, item => item.name == sound);
        if (s != null)
        {
            s.source.Play();
            return;
        }

		s = Array.Find(soundEffects, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StopSpecificSound(string name)
	{
		Sound s = Array.Find(soundMusics, sound => sound.name == name);
		if (s != null)
		{
			s.source.Stop();
			return;
		}
		s = Array.Find(soundEffects, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop();
	}

    public void StopAllMusicSounds()
    {
        foreach (Sound sound in soundMusics)
        {
            sound.source.Stop();
        }
    }

	public void StopAllEffectsSounds()
	{
		foreach (Sound sound in soundEffects)
		{
			sound.source.Stop();
		}
	}

	public void UpdateSoundVolumes()
	{
		foreach (Sound s in soundMusics)
			s.source.volume = PlayerPrefs.GetFloat("BackgroundPrefs") * PlayerPrefs.GetFloat("MasterSoundPrefs") * 0.0001f;
		foreach (Sound s in soundEffects)
			s.source.volume = PlayerPrefs.GetFloat("SoundEffectsPrefs") * PlayerPrefs.GetFloat("MasterSoundPrefs") * 0.0001f;
	}

}
