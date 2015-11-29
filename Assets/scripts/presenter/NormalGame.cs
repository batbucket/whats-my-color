using UnityEngine;
using System.Collections;

/**
 * Medium games have:
 * A time decay
 */
public class NormalGame : Game {
	public const int ID = 1;

	public const float DECAY_RATE = .05f;
	public const float TIME_FLOOR = 2.0f; //The minimum amount of time per question
	
	protected override void resetTime() {
		timePerQuestion = Mathf.Max(timePerQuestion * (1.0f - DECAY_RATE), TIME_FLOOR);
		Debug.Log ("New time: " + timePerQuestion);
		base.resetTime();
	}
}
