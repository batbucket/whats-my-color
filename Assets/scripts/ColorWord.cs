using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ColorWord {
	Color color;
	string word;

	public ColorWord(Color color, string word) {
		this.color = color;
		this.word = word;
	}

	public Color getColor() {
		return color;
	}

	public string getWord() {
		return word;
	}
}
