﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Assertions;

public class ColorWord {
	public readonly Color color;
	public readonly string word;

	/**
	 * These arrays are meant to be paired.
	 * Example:
	 * Index 0 of both should be Color.red and "Red"
	 */
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

	/**
	 * Creates a ColorWord whose Color matches its Word
	 * Use this for button initialization in the Game presenter
	 * 
	 * @param i the index of the approved arrays you want to generate the matching ColorWord for
	 */
	public static ColorWord generateMatchingColorWord(int i) {
		if (i > approvedColors.Length - 1 || i > approvedWords.Length - 1 || i < 0) {
			throw new UnityException("The index used: " + i + " exceeds the bounds of [0, " + (approvedColors.Length - 1) + "]");
		}
		return new ColorWord(approvedColors[i], approvedWords[i]);
	}
}
