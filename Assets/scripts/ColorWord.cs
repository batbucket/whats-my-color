using UnityEngine;
using System.Collections;

public class ColorWord : MonoBehaviour {

	public enum Color {
		RED, ORANGE, GREEN, BLUE, PURPLE, YELLOW
	}
	
	public enum Word {
		RED, ORANGE, GREEN, BLUE, PURPLE, YELLOW
	}

	Color color;
	Word word;

	// Use this for initialization
	void Start () {
	
	}

	//Overloaded Start
	void Start(Color color, Word word) {
		this.color = color;
		this.word = word;
		Start ();
	}

	public Color getColor() {
		return color;
	}

	public Word getWord() {
		return word;
	}

	public bool isColor(Color color) {
		return this.color.Equals(color);
	}

	public bool isWord(Word word) {
		return this.word.Equals(word);
	}

	public bool isColorAndWord(Color color, Word word) {
		return isColor(color) && isWord(word);
	}

	public override bool Equals(object obj) {

		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}
		
		ColorWord cw = (ColorWord) obj;

		return this.color.Equals(cw.getColor()) && this.word.Equals(cw.getWord());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
