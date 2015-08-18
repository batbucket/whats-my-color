using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorButton : MonoBehaviour {

	Button button;
	Chroma chroma;
	Word word;

	// Use this for initialization
	void Start () {
	
	}

	//Overloaded
	public void Start(Chroma chroma, Word word) {
		this.chroma = chroma;
		this.word = word;
		this.button = gameObject.GetComponent<Button>();
	}

	public Button getButton() {
		return button;
	}

	public Chroma getChroma() {
		return chroma;
	}

	public void setChroma(Chroma chroma) {
		this.chroma = chroma;
	}

	public Word getWord() {
		return word;
	}

	public void setWord(Word word) {
		this.word = word;
	}
	
	// Update is called once per frame
	void Update () {
		Color color = Helper.translate(chroma);
		button.image.color = color;
	}
}
