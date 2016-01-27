using UnityEngine;
using System.Collections;
using System;

/**
 * Hard games have:
 * Time decay
 * Button shuffle
 */
public class HardGame : Game {
    public const int ID = 3;
    public const string SCORE_LOCATION = "Hard_Score";
    public const string HISCORE_LOCATION = "Hard_Hiscore";
    public const string MODE_DESCRIPTION = "<color=red>HARD: BUTTONS ARE RANDOMIZED.</color>";
    public const string UNLOCK_DESCRIPTION = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=red>HARD MODE</color>.</color></i>";

    public const float DECAY_RATE = .02f;
    public const float TIME_FLOOR = 0.1f; //The minimum amount of time per question

    public const int SCORE_REQUIREMENT = 15;

    protected override void ResetTime() {
        timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
        Debug.Log("New time: " + timePerQuestion);
        base.ResetTime();
    }

    /**
     * Buttons are shuffled before the next question is posed
     */
    protected override void NextQuestion() {
        buttonManager.ShuffleButtons();
        base.NextQuestion();
    }

    protected override void SaveScore() {
        PlayerPrefs.SetInt(SCORE_LOCATION, score);
    }

    public static bool HasMetRequirement() {
        return PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION) >= SCORE_REQUIREMENT;
    }
}
