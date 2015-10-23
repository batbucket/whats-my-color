using UnityEngine;
using System.Collections;

/**
 * Hard games have:
 * Time decay
 * Button shuffle
 */
public class HardGame : Game {
	public const float DECAY_RATE = .07f;
	public const float TIME_FLOOR = 2.0f; //The minimum amount of time per question
	
	protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}

	protected override void nextQuestion() {
		buttonManager.shuffleButtons();
		base.nextQuestion();
	}
}
