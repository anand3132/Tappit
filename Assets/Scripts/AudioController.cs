using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

// This class is used to controll the Audio of our game..
// Created By Anand.A

public class AudioController : MonoBehaviour {
	public AudioSource bgMSource;
	public AudioSource effectSource;
	public static AudioController instance = null;
	public float highPitchRange = 1.05f;
	public float lowPitchRange = 0.95f;
	public AudioClip bGMusic;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		
		DontDestroyOnLoad (gameObject);
		PlayBGM (bGMusic);
	}

	public void PlayOrstopSound() {
		bgMSource.mute = !bgMSource.mute;
		effectSource.mute = !effectSource.mute;
	}

	public void PlayBGM(AudioClip bGMClip) {
		bgMSource.loop = true;
		bgMSource.clip = bGMClip;
		bgMSource.Play ();
	}

	public void PlaySFX(params AudioClip [] sFXClip) {
		int randomIndex = Random.Range (0, sFXClip.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		effectSource.pitch = randomPitch;
		effectSource.clip = sFXClip [randomIndex];
		effectSource.Play ();
	}
}
