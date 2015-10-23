using UnityEngine;
using System.Collections;

/**
 * Impossible games have:
 * Time decay
 * Button shuffle
 * 4 Question Modes
 * 2 Button modes
 */
public class ImpossibleGame : Game {
	public const float DECAY_RATE = .09f;
	public const float TIME_FLOOR = 2.0f; //The minimum amount of time per question
	
	protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}
	
	protected override void nextQuestion() {
		questionManager.randomizeColorWord();
		buttonManager.shuffleButtons();
		buttonManager.randomizeButtonMode();
		questionManager.randomizeColorWordMode();
		questionManager.randomizeQuestionMode(); //Must be called AFTER randomizing the ColorWordMode
	}
}
