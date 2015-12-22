using UnityEngine;
using System.Collections;
using System;

/**
 * Easy games have:
 * A time limit
 * 1 Question
 * No decay rate
 */
public class EasyGame : Game {
	public const int ID = 1;
	public const string SCORE_LOCATION = "Easy_Score";
    public const string HISCORE_LOCATION = "Easy_Hiscore";

    protected override void saveScore() {
		PlayerPrefs.SetInt(SCORE_LOCATION, score);
	}

    /**
     * Modes are unlocked based on the hiscore in the previous
     * Mode. Easy is automatically unlocked, but this method is
     * Here for consistency.
     */
    public static bool hasMetRequirement() {
        return true;
    }
}
