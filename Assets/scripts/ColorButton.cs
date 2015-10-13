using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorButton : MonoBehaviour {

	Button button;
	ColorWord colorWord;

	const int MAGENTA_FONT_SIZE = 72;
	const int DEFAULT_FONT_SIZE = 80;

	// Use this for initialization
	void Awake () {
		this.button = gameObject.GetComponent<Button>();
	}

	public Button getButton() {
		return button;
	}

	public Color getColor() {
		return colorWord.getColor();
	}

	public string getWord() {
		return colorWord.getWord();
	}
	
	// Update is called once per frame
	void Update () {
		button.GetComponentInChildren<Text>().text = colorWord.getWord();
	}
}
