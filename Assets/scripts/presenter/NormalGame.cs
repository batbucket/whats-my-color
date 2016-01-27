﻿using UnityEngine;
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
    public const string MODE_DESCRIPTION = "<color=orange>NORMAL: TIME PER QUESTION DECAYS.</color>";
    public const string UNLOCK_DESCRIPTION = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=orange>NORMAL MODE</color>.</color></i>";

    public const float DECAY_RATE = .02f;
    public const float TIME_FLOOR = 0.1f; //The minimum amount of time per question

    public const int SCORE_REQUIREMENT = 10; //Hiscore in Easy Mode needed to unlock this mode

    /**
     * Added time decay before each time reset
     */
    protected override void ResetTime() {
        timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
        Debug.Log("New time: " + timePerQuestion);
        base.ResetTime();
    }

    protected override void SaveScore() {
        PlayerPrefs.SetInt(SCORE_LOCATION, score);
    }

    public static bool HasMetRequirement() {
        return PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION) >= SCORE_REQUIREMENT;
    }
}
