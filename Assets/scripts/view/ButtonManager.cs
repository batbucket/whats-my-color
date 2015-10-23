﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

/**
 * This class manages the buttons on the screen
 * Currently there's 6 buttons, but it should be able to get any number of
 * them as long as they're assigned to it as its children
 */
public class ButtonManager : MonoBehaviour {
	ColorButton[] colorButtons; //Array of the ColorButtons that are the manager's children

	// Use this for initialization
	void Awake () {
		this.colorButtons = GetComponentsInChildren<ColorButton>();
		nullCheck();
	}

	void nullCheck() {
		Assert.IsNotNull(this.colorButtons);
		for (int i = 0; i < colorButtons.Length; i++) {
			Assert.IsNotNull(this.colorButtons[i]);
		}
	}

	/**
	 * Shuffles the buttons that are displayed
	 * by exchanging their ColorWords
	 */
	public void shuffleButtons() {
		for (int i = 0; i < colorButtons.Length; i++) {
			int j = Random.Range(0, i);
			swapColorWord(j, i);	
		}
	}

	public ColorButton[] getColorButtons() {
		return colorButtons;
	}

	public void randomizeButtonMode() {
		setButtonMode(Tool.GetRandomEnum<ButtonMode>());
	}

	void setWordMode() {
		setButtonMode(ButtonMode.WORD);
	}

	void setColorMode() {
		setButtonMode(ButtonMode.COLOR);
	}

	void setButtonMode(ButtonMode mode) {
		for (int i = 0; i < colorButtons.Length; i++) {
			colorButtons[i].setButtonMode(mode);
		}
	}

	/**
	 * Swap the ColorWords of two buttons given their indicies
     */
	void swapColorWord(int a, int b) {
		if (a < 0 || b < 0 
		    || a > colorButtons.Length - 1 
		    || b > colorButtons.Length - 1) {
			throw new UnityException("Error: Tried to index a:" + a 
			                         + " b: " + b 
			                         + " In an array of size: " + colorButtons.Length);
		}
		colorButtons[a].swapColorWord(colorButtons[b]);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
