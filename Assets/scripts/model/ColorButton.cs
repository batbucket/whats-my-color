using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class ColorButton : MonoBehaviour {

	Button button;
	ColorWord colorWord;
	ButtonType buttonType;
	public static readonly Color WORD_MODE_COLOR = Color.white;
	bool colorWordSet;
	
	const int DEFAULT_FONT_SIZE = 80;

	// Use this for initialization
	void Start () {
		this.button = gameObject.GetComponent<Button>();
		this.buttonType = ButtonType.WORD;
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
		} else {
			throw new UnityException("Cannot set a ColorButton's ColorWord twice! Use swap() to switch!");
		}
	}
	
	public void setButtonType(ButtonType newButtonType) {
		this.buttonType = newButtonType;
	}

	private ColorWord getColorWord() {
		return colorWord;
	}

	private void setTextAlpha(int alpha) {
		Color color = button.GetComponentInChildren<Text>().color;
		color.a = alpha;
		button.GetComponentInChildren<Text>().color = color;
	}

	private void setTextInvisible() {
		setTextAlpha(0);
	}

	private void setTextVisible() {
		setTextAlpha(1);
	}

	private void updateButtonText() {
		if (buttonType == ButtonType.WORD) {
			button.GetComponentInChildren<Text>().text = colorWord.word;
			setTextVisible();
		} else if (buttonType == ButtonType.COLOR) {
			setTextInvisible();
		} else {
			throw new UnityException("Unknown Button Type: " + buttonType);
		}
	}

	private void updateButtonDisplay() {;
		if (buttonType == ButtonType.WORD) {
			setButtonColor(WORD_MODE_COLOR);
		} else if (buttonType == ButtonType.COLOR){
			setButtonColor(colorWord.color);
		} else {
			throw new UnityException("Unknown Button Type: " + buttonType);
		}
	}

	private void setButtonColor(Color color) {
		ColorBlock cb = button.GetComponent<Button>().colors;
		cb.normalColor = color;
		cb.highlightedColor = color;
		button.GetComponent<Button>().colors = cb;
	}

	public void swapColorWord(ColorButton other) {
		ColorWord temp = other.getColorWord();
		other.setColorWord(this.getColorWord());
		this.setColorWord(temp);
	}

	// Update is called once per frame
	void Update () {
		updateButtonText();
		updateButtonDisplay();
	}
}
