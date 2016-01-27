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
    public const string MODE_DESCRIPTION = "<color=green>EASY: PICK THE COLOR, NOT THE WORD.</color>";
    public const string UNLOCK_DESCRIPTION = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=green>EASY MODE</color>.</color></i>"; //You should never see this

    public const int SCORE_REQUIREMENT = 0; //Automatically unlocked, but here for consistency

    protected override void SaveScore() {
        PlayerPrefs.SetInt(SCORE_LOCATION, score);
    }

    /**
     * Modes are unlocked based on the hiscore in the previous
     * Mode. Easy is automatically unlocked, but this method is
     * Here for consistency.
     */
    public static bool HasMetRequirement() {
        return true;
    }
}
