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
				updateMenuLogic ();
				//Debug.Log("MenueState");
				uiController.onTouch();
			}
			break;
		case GAMESTATE.kIngame: {
				//Debug.Log("IngameState");
				uiController.onTouch ();
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

	private void updateMenuLogic() {
		//uiController.switchToMainMenu ();
		// we can add any logic for menu here. 
	}

	private void updateIngameLogic() {
		//uiController.switchToIngameMenu ();
	}

	public void ResetGame(){
		GameObject.DestroyImmediate (levelObject);
		uiController.slider.value = 100;
		uiController.score = 0;
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
}//GameManager
