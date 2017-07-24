using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is used to Generate random level for our game.
// Created By Anand.A

public class PatternGenerator : MonoBehaviour {
	public Image fillImage;
	public GameManager gameManager;
	public Component[] childAnimations;
	public GameObject gameMenue;

	public GameObject LoadLevel(int level){
		Object tempObj;
		switch(level)
		{
		case 1:
			tempObj = Resources.Load("Prefab/Level_1");
			break;
		case 2:
			tempObj = Resources.Load("Prefab/Level_2");
			break;
		case 3:
			tempObj = Resources.Load("Prefab/Level_3");
			break;
		default:
			tempObj = Resources.Load("Prefab/Level_3");
			break;
		}

		GameObject currentLevel = null;
		if (tempObj == null) {
			return null;
		}

		currentLevel = (GameObject)GameObject.Instantiate (tempObj, transform);

		// preset colors
		Color[] presetColors = {
			Color.black
			, Color.blue
			, Color.cyan
			, Color.gray
			, Color.green
			, Color.magenta
			, Color.red
			, Color.white
			, Color.yellow
		};

		// random color generation from the preset list
		int nObjectsToColor = GetComponentsInChildren<SpriteRenderer> ().Length;
		int presetColorCounter = presetColors.Length;
		List<int> colorIndices = new List<int> ();

		int overflowCounter = 0;
		while (presetColorCounter > 0 && overflowCounter < 5000) {
			int randIndex = Random.Range (0, presetColors.Length);
			// check for duplicates
			bool found = false;
			for (int x = 0; x < colorIndices.Count; x++) {
				if (randIndex == colorIndices [x]) {
					found = true;
					break;
				}
			}
			if (!found) {
				colorIndices.Add (randIndex);
//				Debug.Log ("index (" + (colorIndices.Count-1) +") " + randIndex);
				presetColorCounter--;
			}
			overflowCounter++;
		}

		if (overflowCounter > 100) {
			Debug.LogError ("OVER FLOW");
		}

		// start coloring
		int whichColorForFill = Random.Range(0, nObjectsToColor);
		int itr = 0;
		foreach ( SpriteRenderer spr in GetComponentsInChildren<SpriteRenderer>()) {
			if (itr < colorIndices.Count) {
				spr.material.color = presetColors[colorIndices [itr]];
			} else {
				Color rndClr = Random.ColorHSV (0.3f, 1f, 0.3f, 1f, 0.3f, 1f);
				spr.material.color = rndClr;
			}

			if (whichColorForFill == itr) {
				fillImage.color = spr.material.color;
			}
			itr++;
		}

//		if (_obj != null) {
//			 currentLevel = (GameObject)GameObject.Instantiate (_obj, transform);
//		
//			foreach ( SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
//				Color rnd = Random.ColorHSV (0f, 1f, 0f, 1f, 0f, 1f);
//				sr.material.color = rnd;
//				Fill.color=rnd;
//			}
//		}

		return currentLevel;
	}
}
