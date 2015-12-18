using UnityEngine;
using System.Collections;
using System;

/**
 * Medium games have:
 * A time decay
 */
public class NormalGame : Game {
	public const int ID = 2;
	public const string SCORE_LOCATION = "Normal_Score";
    public const string HISCORE_LOCATION = "Normal_Hiscore";

	public const float DECAY_RATE = .05f;
	public const float TIME_FLOOR = 1.0f; //The minimum amount of time per question

    public const int SCORE_REQUIREMENT = 10;
	
	protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}

	protected override void saveScore() {
		PlayerPrefs.SetInt(SCORE_LOCATION, score);
	}

    public static bool hasMetRequirement() {
        return PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION) >= SCORE_REQUIREMENT;
    }
}
