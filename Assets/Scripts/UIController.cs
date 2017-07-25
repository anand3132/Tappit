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
	public Slider slider;

	public GameObject mainMenu;
	public GameObject scoreBoard;
	public GameObject infoMenue;
	public GameObject gameMenue;

	public GameObject volumeButton;
	public GameObject playPauseButton;
	public bool volumeSwitch =true;
	public CameraShake cameraShake;

	// scoring systems
	public int score;
	int bestScore = 0;

	void Start () {
		resetInGameUI();
	}

	public void resetInGameUI() {
		// PlayerPrefs.SetInt ("bestScore", 0);
		bestScore = PlayerPrefs.GetInt ("bestScore");
		slider = gameMenue.GetComponentInChildren<Slider> ();
		slider.value = 100f;
		score = 0;
	}

	public void switchToMainMenu() {
		gameManager.switchToMenu ();
		gameMenue.SetActive (false);
		scoreBoard.SetActive (false);
		infoMenue.SetActive (false);
		mainMenu.SetActive (true);
		resetInGameUI ();
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
	}

	public void switchToInfoMenu() {
		gameMenue.SetActive (false);
		scoreBoard.SetActive (false);
		infoMenue.SetActive (true);
		mainMenu.SetActive (false);
	}

	public void resetButtonOnClick() {
		Button playPausebuttonImage = playPauseButton.gameObject.GetComponent<Button>();
		switchToIngameMenu ();
		score = 0;
		playPausebuttonImage.image.sprite = Resources.Load<Sprite> ("play");
	}

	public void PlayPauseButtonClicked() {
		gameManager.PausePlayIngame ();	// Toggle pause/play game

		Button playPausebuttonImage = playPauseButton.gameObject.GetComponent<Button>();
		if (gameManager.IsIngamePaused ()) {
			playPausebuttonImage.image.sprite = Resources.Load<Sprite> ("pause");
		} else {
			playPausebuttonImage.image.sprite = Resources.Load<Sprite> ("play");
		}
	}

	public void switchVolume() {
		Button volumeButtonImage = volumeButton.gameObject.GetComponent<Button>();
		if (volumeSwitch) {
			volumeButtonImage.image.sprite = Resources.Load<Sprite>("volume-in-active");
			audioController.playOrstopSound ();
			volumeSwitch = false;
		} else {
			volumeButtonImage.image.sprite = Resources.Load<Sprite>("volume-active");
			audioController.playOrstopSound ();
			volumeSwitch = true;

		}
	}

	void FixedUpdate(){
		if (gameManager.GetGameState() == GameManager.GAMESTATE.kIngame) {
			if (slider.value > 1.0f) {
				if (!gameManager.IsIngamePaused ()) {
					slider.value -= 50.0f * Time.deltaTime;
				}
			} else {
				// TODO : Try to use WIN & LOSE state so that we can move this code inside GameManager.
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
		Application.OpenURL ("market://details?id=air.org.axisentertainment.BabyHazelKitchenTime");
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
