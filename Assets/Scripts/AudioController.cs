using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioClip BGMusic;
	public AudioSource audioSource;
	public AudioClip gameOverSound;
	public AudioClip clickSound;

	void Start(){
		audioSource.clip = BGMusic;
	}
	public void BGaudio()
	{
	}
}
