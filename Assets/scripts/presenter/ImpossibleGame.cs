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
	public const int ID = 4;
	public const string SCORE_LOCATION = "Impossible_Score";
    public const string HISCORE_LOCATION = "Impossible_Hiscore";

    public const float DECAY_RATE = .02f;
	public const float TIME_FLOOR = 0.1f; //The minimum amount of time per question

    public const int SCORE_REQUIREMENT = 20;

    protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}
	
    /**
     * Button modes are also randomized
     */
	protected override void nextQuestion() {
		base.nextQuestion();
		buttonManager.shuffleButtons();
		buttonManager.randomizeButtonModes();
		questionManager.randomizeQuestionMode();
	}

	protected override void saveScore() {
		PlayerPrefs.SetInt(SCORE_LOCATION, score);
	}

    public static bool hasMetRequirement() {
        return PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION) >= SCORE_REQUIREMENT;
    }
}
