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
	public const int ID = 3;
	public const string IMPOSSIBLE_SCORE_LOCATION = "Impossible_Score";

	public const float DECAY_RATE = .05f;
	public const float TIME_FLOOR = 1.0f; //The minimum amount of time per question
	
	protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}
	
	protected override void nextQuestion() {
		questionManager.randomizeColorWord();
		buttonManager.shuffleButtons();
		buttonManager.randomizeButtonModes();
		questionManager.randomizeQuestionMode();
	}

	protected override void saveScore() {
		PlayerPrefs.SetInt(IMPOSSIBLE_SCORE_LOCATION, score);
	}
}
