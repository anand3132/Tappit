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
	GAMESTATE gameState = GAMESTATE.kMenu;
	GameObject levelObject = null;
	private bool paused = false;

	public GAMESTATE GetGameState() {
		return gameState;
	}

	void Update () {
		switch (gameState) {
		case GAMESTATE.kMenu: {
				//Debug.Log("MenueState");
				//onTouch();
			}
			break;
		case GAMESTATE.kIngame: {
				//Debug.Log("IngameState");
				onTouch ();
//				updateIngameLogic ();
			}
			break;
		case GAMESTATE.kGameOver: {
				uiController.switchToScoreBoard ();
			}
			break;
		}
	}

	public void switchToIngame() {
		gameState = GAMESTATE.kIngame;
		levelObject = patternGenerator.LoadLevel (Random.Range(1, 4));
	}

	public void switchToMenu() {
		if (levelObject != null) {
			GameObject.DestroyImmediate (levelObject);
		}
		paused = false;
		gameState = GAMESTATE.kMenu;
	}

	public void switchToGameOver() {
		gameState = GameManager.GAMESTATE.kGameOver;
		ResetGame ();
	}
		
	public void ResetGame(){
		GameObject.DestroyImmediate (levelObject);
		uiController.slider.value = 100;
		paused = false;
	}

	public void PausePlayIngame() {
		paused = !paused;
		Debug.Log ("Pause " + paused);
		PauseOrPlayGameAnimation(levelObject, paused);
	}

	private bool IsIngamePaused() {
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
	public void onTouch(){
		if (Input.GetMouseButtonDown (0)) {
			//	Vector3 touchPosition = Input.GetTouch (0).position;
			Vector3 touchPosition = Input.mousePosition;
			Vector3 pos = Camera.main.ScreenToWorldPoint (touchPosition);
			RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);
			if (hit != null && hit.collider != null) {
				if (patternGenerator.fillImage.color == hit.collider.GetComponent<SpriteRenderer> ().material.color) {
//					Debug.Log ("Collided");
					uiController.addScore ();
					ResetGame ();
					switchToIngame ();
				} else {
					switchToGameOver ();
				}
			}
			Debug.Log ("Score" + uiController.score);
		}
	}
}//GameManager
