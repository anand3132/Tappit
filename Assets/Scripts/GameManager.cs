using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to controll the flow of our game.
// Created By Anand.A

public class GameManager : MonoBehaviour {
	// Use this for initialization
	public enum GAMESTATE {
		kMenu,
		kIngame,
		kGameOver,
		kMaxState
	}

	// controller references.
	public UIController uiController;
	public PatternGenerator patternGenerator;
	public CameraShake cameraShake;

	// Audio Clip references.
	public AudioClip clickSound;
	public AudioClip gameOverSound;

	private bool paused = false;
	GAMESTATE gameState = GAMESTATE.kMenu;
	GameObject levelObject = null;

	public GAMESTATE GetGameState() {
		return gameState;
	}

	void Update () {
		switch (gameState) {
		case GAMESTATE.kMenu: {
				// Not added any logic so far.
			}
			break;
		case GAMESTATE.kIngame: {
				updateIngame ();
			}
			break;
		case GAMESTATE.kGameOver: {
				// Not added any logic so far.
			}
			break;
		}
	}

	public void SwitchToIngame() {
		gameState = GAMESTATE.kIngame;
		levelObject = patternGenerator.LoadLevel (Random.Range(1, 5));
	}

	public void SwitchToMenu() {
		if (levelObject != null) {
			GameObject.DestroyImmediate (levelObject);
		}
		paused = false;
		gameState = GAMESTATE.kMenu;
	}

	public void SwitchToGameOver() {
		gameState = GameManager.GAMESTATE.kGameOver;
		AudioController.instance.PlaySFX (gameOverSound);
		uiController.SwitchToScoreBoard ();
		ResetGame ();
	}
		
	public void ResetGame(){
		GameObject.DestroyImmediate (levelObject);
		uiController.slider.value = 100;
		paused = false;
	}

	public void PausePlayIngame() {
		paused = !paused;
		//Debug.Log ("Pause " + paused);
		PauseOrPlayGameAnimation(levelObject, paused);
	}

	public bool IsIngamePaused() {
		return paused;
	}

	private void PauseOrPlayGameAnimation(GameObject animationTree, bool pause){
		foreach (Animation animation in animationTree.GetComponents<Animation>()) {
			foreach(AnimationState animState in animation ) {
				animState.speed = (pause) ? 0.0f : 1.0f;
			}
		}

		for (int c = 0; c < animationTree.transform.childCount; c++) {
			PauseOrPlayGameAnimation (animationTree.transform.GetChild(c).gameObject, pause);
		}
	}
	public void updateIngame() {

		
		if (Input.GetMouseButtonDown (0)) {
					if (IsIngamePaused () ) {
						return;
					}
			// check if player clicked on the correct symbol or not.
			Vector3 touchPosition = Input.mousePosition;
			Vector3 pos = Camera.main.ScreenToWorldPoint (touchPosition);
			RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);
			if (hit != null && hit.collider != null) {
				if (patternGenerator.fillImage.color == hit.collider.GetComponent<SpriteRenderer> ().material.color) {
					AudioController.instance.PlaySFX(clickSound);
					uiController.AddScore ();
					ResetGame ();
					SwitchToIngame ();
				} else {
					// shake the camera for wrong click
					if (!cameraShake.IsShaking () && !cameraShake.IsShakeFinished ()) {
						cameraShake.StartShake ();
						PausePlayIngame ();

					}
				}
			}
		}

		// if there is a camera shake then check for camera shake animation finished.
		// TODO : Need to introduce two more states WIN & LOSE to clean this up.
		if (cameraShake.IsShakeFinished ()) {
			cameraShake.RestCameraShake ();
			SwitchToGameOver ();
		}
	}
}//GameManager
