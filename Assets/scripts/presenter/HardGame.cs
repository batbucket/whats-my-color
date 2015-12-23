using UnityEngine;
using System.Collections;

/**
 * Hard games have:
 * Time decay
 * Button shuffle
 */
public class HardGame : Game {
	public const int ID = 3;
	public const string SCORE_LOCATION = "Hard_Score";
    public const string HISCORE_LOCATION = "Hard_Hiscore";

    public const float DECAY_RATE = .02f;
	public const float TIME_FLOOR = 0.1f; //The minimum amount of time per question

    public const int SCORE_REQUIREMENT = 15;

    protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}

    /**
     * Buttons are shuffled before the next question is posed
     */
	protected override void nextQuestion() {
		buttonManager.shuffleButtons();
		base.nextQuestion();
	}

	protected override void saveScore() {
		PlayerPrefs.SetInt(SCORE_LOCATION, score);
	}

    public static bool hasMetRequirement() {
        return PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION) >= SCORE_REQUIREMENT;
    }
}
