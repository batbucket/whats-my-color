using UnityEngine;
using System.Collections;

public class SampleGame : EasyGame {

	public void goToModeScreen() {

	}

	public void goToCreditsScreen() {

	}

	// Update is called once per frame
	void Update () {
		if ((Random.Range(0, (int) timerManager.getCurrentTime() * 60) == 0 || timerManager.getCurrentTime() == 0) && timerManager.getCurrentTime() <= INITIAL_TIME_PER_QUESTION - 1.0f) {
			base.nextQuestion();
			base.resetTime();
		}
	}
}
