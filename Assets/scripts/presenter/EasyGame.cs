using UnityEngine;
using System.Collections;

/**
 * Easy games have:
 * A time limit
 * 1 Question
 * No decay rate
 */
public class EasyGame : Game {
	public const int ID = 0;
	public const string EASY_SCORE_LOCATION = "Easy_Score";

	protected override void saveScore() {
		PlayerPrefs.SetInt(EASY_SCORE_LOCATION, score);
	}
}
