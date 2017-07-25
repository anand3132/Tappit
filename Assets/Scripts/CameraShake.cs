using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	public float min=0f;
	public float max=0f;
	float duration = 0f;
	Vector3 initialPosition;

	// Use this for initialization
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
		}

		if (duration < 0) {
			gameObject.transform.position = initialPosition;
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
