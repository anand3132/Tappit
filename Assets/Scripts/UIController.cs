using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// This class is used to manage the ui behaviors of our game.
// Created By Anand.A

public class UIController : MonoBehaviour {
	// gui components
	public GameManager gameManager;
	public PatternGenerator pGenerator;
	public AudioController audioController;

	public Text scoreText;
	public Text ingameScoreText;

	public Text bestScoreText;

	public GameObject mainMenu;
	public GameObject scoreBoard;
	public GameObject infoMenue;
	public GameObject gameMenue;

	public GameObject volumeButton;
	public bool volumeSwitch=true;
	public CameraShake cameraShake;

	public Slider slider;

	// scoring systems
	public int score;
	int bestScore =0;

	void Start () {
		//PlayerPrefs.SetInt ("bestScore", 0);
		score = 0;
		bestScore = PlayerPrefs.GetInt ("bestScore");
		slider= gameMenue.GetComponentInChildren<Slider> ();
		slider.value = 100f;
	}

	public void switchToMainMenu() {
		gameManager.switchToMenu ();
		gameMenue.SetActive (false);
		scoreBoard.SetActive (false);
		infoMenue.SetActive (false);
		mainMenu.SetActive (true);
	}

	public void switchToIngameMenu() {
		gameManager.switchToIngame ();
		gameMenue.SetActive (true);
		scoreBoard.SetActive (false);
		infoMenue.SetActive (false);
		mainMenu.SetActive (false);
	} 

	public void switchToScoreBoard() {
		gameMenue.SetActive (false);
		scoreBoard.SetActive (true);
		infoMenue.SetActive (false);
		mainMenu.SetActive (false);
		bestScoreText.text = bestScore.ToString();
		scoreText.text = score.ToString();
		// gameManager.ResetGame ();
		gameManager.redBG.SetActive (false);

	}

	public void switchToInfoMenu() {
		gameMenue.SetActive (false);
		scoreBoard.SetActive (false);
		infoMenue.SetActive (true);
		mainMenu.SetActive (false);
	}
	public void resetButtonOnClick()
	{
		switchToIngameMenu ();
		score = 0;
	}
	public void switchVolume(){
		if (volumeSwitch) {
			Button buttonImage =volumeButton.gameObject.GetComponent<Button>();
			buttonImage.image.sprite = Resources.Load<Sprite>("volume-in-active");
			volumeSwitch = false;
			audioController.playOrstopSound ();
		} else {
			Button buttonImage =volumeButton.gameObject.GetComponent<Button>();
			buttonImage.image.sprite = Resources.Load<Sprite>("volume-active");		
			volumeSwitch = true;
			audioController.playOrstopSound ();

		}
	}

	void FixedUpdate(){
		if (gameManager.GetGameState() == GameManager.GAMESTATE.kIngame) {
			if (slider.value > 1.0f) {
				if (!gameManager.IsIngamePaused ()) {
					slider.value -= 50.0f * Time.deltaTime;
				}
			} else {
				// start the camera shake here.
				if (!cameraShake.IsShaking () && !cameraShake.IsShakeFinished ()) {
					cameraShake.StartShake ();
				} else {
					if (cameraShake.IsShakeFinished ()) {
						cameraShake.RestCameraShake ();
						gameManager.switchToGameOver ();
					}
				}
			}	
		}
		ingameScoreText.text = score.ToString ();
	}

	public void playStore() {
		Application.OpenURL ("market://details?id=com.example.android");
	}

	public void addScore() {
		score++;
		Debug.Log ("Score added" + score);
		if (score>bestScore ) {
			bestScore = score;
			PlayerPrefs.SetInt ("bestScore", score);
		}
	}

	public int getHighScore() {
		return bestScore;
	}

}
