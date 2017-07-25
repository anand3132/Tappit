using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioSource bgMSource;
	public AudioSource effectSource;
	public static AudioController instance = null;
	public float highPitchRange = 1.05f;
	public float lowPitchRange =.95f;
	public AudioClip bGMusic;

	void Awake(){
		if (instance == null)
			instance = this;
		else if(instance!=this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
		PlayBGM (bGMusic);
	}

	public void playOrstopSound(){
			bgMSource.mute = !bgMSource.mute;
			effectSource.mute = !effectSource.mute;
	}

	public void PlayBGM(AudioClip _clip){
		bgMSource.loop = true;
		bgMSource.clip = _clip;
		bgMSource.Play ();
	}

	public void PlaySFX(params AudioClip [] _clips){
		int randomIndex = Random.Range (0, _clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		effectSource.pitch = randomPitch;
		effectSource.clip = _clips [randomIndex];
		effectSource.Play ();
	}
}
