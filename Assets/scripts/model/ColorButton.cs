using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class ColorButton : MonoBehaviour {

	Button button;
	ColorWord colorWord; //Color and Word should match
	ButtonMode buttonMode;
	public static readonly Color WORD_MODE_COLOR = Color.white;
	bool colorWordSet;
	
	const int DEFAULT_FONT_SIZE = 80;

	// Use this for initialization
	void Awake () {
		this.button = gameObject.GetComponent<Button>();
		this.buttonMode = ButtonMode.WORD;
		this.colorWord = new ColorWord(Color.grey, "UNDEFINED"); //Change this with setting the colorWord

		Assert.IsNotNull(this.button);
	}

	public Button getButton() {
		return button;
	}

	public Color getColor() {
		return colorWord.color;
	}

	public string getWord() {
		return colorWord.word;
	}

	public void setColorWord(ColorWord other) {
		if (!colorWordSet) {
			this.colorWord = other;
			colorWordSet = true;
		} else {
			throw new UnityException("Cannot set a ColorButton's ColorWord twice! Use swap() to switch!");
		}
	}

	/**
	 * Works like setColorWord(), but doesn't have the warning
	 * if you use this multiple times on one button
	 * Used in swap()
	 */ 
	void forceSetColorWord(ColorWord other) {
		this.colorWord = other;
	}
	
	public void setButtonMode(ButtonMode newButtonMode) {
		this.buttonMode = newButtonMode;
	}

	public ColorWord getColorWord() {
		return colorWord;
	}

	public void swapColorWord(ColorButton other) {
		ColorWord temp = other.getColorWord();
		other.forceSetColorWord(this.getColorWord());
		this.forceSetColorWord(temp);
	}

	void setTextAlpha(int alpha) {
		Color color = button.GetComponentInChildren<Text>().color;
		color.a = alpha;
		button.GetComponentInChildren<Text>().color = color;
	}

	void setTextInvisible() {
		setTextAlpha(0);
	}

	void setTextVisible() {
		setTextAlpha(1);
	}

	void updateButtonText() {
		if (buttonMode == ButtonMode.WORD) {
			button.GetComponentInChildren<Text>().text = colorWord.word;
			setTextVisible();
		} else if (buttonMode == ButtonMode.COLOR) {
			setTextInvisible();
		} else {
			throw new UnityException("Unknown Button Type: " + buttonMode);
		}
	}

	void updateButtonDisplay() {;
		if (buttonMode == ButtonMode.WORD) {
			setButtonColor(WORD_MODE_COLOR);
		} else if (buttonMode == ButtonMode.COLOR){
			setButtonColor(colorWord.color);
		} else {
			throw new UnityException("Unknown Button Type: " + buttonMode);
		}
	}

	void setButtonColor(Color color) {
		ColorBlock cb = button.GetComponent<Button>().colors;
		cb.normalColor = color;
		cb.highlightedColor = color;
		button.GetComponent<Button>().colors = cb;
	}

	// Update is called once per frame
	void Update () {
		updateButtonText();
		updateButtonDisplay();
	}
}
