using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChromaButton : MonoBehaviour {

	Button button;
	public Chroma chroma;
	public Word word;
	
	const int MAGENTA_FONT_SIZE = 72;
	const int DEFAULT_FONT_SIZE = 80;

	// Use this for initialization
	void Awake () {
		this.button = gameObject.GetComponent<Button>();
	}

	public Button getButton() {
		return button;
	}

	public Chroma getChroma() {
		return chroma;
	}

	public Word getWord() {
		return word;
	}

	public void changeChroma(Chroma chroma) {
		this.chroma = chroma;
	}

	public void changeWord(Word word) {
		this.word = word;
	}
	
	// Update is called once per frame
	void Update () {
		if (chroma.Equals(Chroma.MAGENTA)) {
			button.GetComponentInChildren<Text>().fontSize = MAGENTA_FONT_SIZE;
		} else {
			button.GetComponentInChildren<Text>().fontSize = DEFAULT_FONT_SIZE;
		}
		button.GetComponentInChildren<Text>().text = chroma.ToString();
	}
}
