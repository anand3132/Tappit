using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to Generate camera Shack for our game.
// Created By Anand.A

public class CameraShake : MonoBehaviour {
	public float min=0f;
	public float max=0f;
	float duration = 0f;
	Vector3 initialPosition;
	public GameObject redBG;


	public void StartShake () {
		duration = 0.3f;
		initialPosition = transform.position;
		min=transform.position.x;
		max=transform.position.x+1;
	}

	void Update () {
		if (duration > 0) {
			gameObject.transform.position = new Vector3 (Mathf.PingPong (Time.time * 15f, max - min), transform.position.y, transform.position.z);
			duration-=Time.deltaTime;
			redBG.SetActive (true);
		}

		if (duration < 0) {
			gameObject.transform.position = initialPosition;
			redBG.SetActive (false);
		}
	}

	public bool IsShakeFinished() {
		return (duration < 0.0f);
	}

	public bool IsShaking() {
		return (duration > 0.0f);
	}

	public void RestCameraShake() {
		duration = 0.0f;
	}
}
