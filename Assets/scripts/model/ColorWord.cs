using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Assertions;

public class ColorWord {
	public readonly Color color;
	public readonly string word;

	public static readonly Color[] approvedColors = new Color[] {Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta};
	public static readonly string[] approvedWords = new string[] {"Red", "Yellow", "Green", "Blue", "Cyan", "Pink"};

	public ColorWord(Color color, string word) {
		this.color = color;
		this.word = word;

		Assert.AreEqual(approvedColors.Length, approvedWords.Length);
	}

	/**
	 * Only use this for generating questions, not randomizing the button order
	 */
	public static ColorWord generateRandomColorWord() {
		return new ColorWord(approvedColors[UnityEngine.Random.Range(0, approvedColors.Length)], 
		                     approvedWords[UnityEngine.Random.Range(0, approvedWords.Length)]);
	}
}
