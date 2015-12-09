using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

/**
 * This class manages the TimerBar on the top of the screen
 */
public class TimerManager : MonoBehaviour {
	Image timeBar;
	Transform timeBarTransform;
	
	public Color firstColor; //The 'healthy' color
	public Color secondColor; //The 'injured' color
	public Color thirdColor; //The 'dying' color
	
	const float TRANSITION_POINT = .5f; //The point at which the colors will transition to the next one
	
	Color DISABLED_COLOR = Color.gray;
	Color DEAD_COLOR = Color.black;
	
	float currentValue;
	const float INITIAL_CURRENT = 10;
	
	float maxValue;
	const float INITIAL_MAX = 10;
	
	bool initiated;
	
	// Use this for initialization
	void Awake () {
		timeBar = gameObject.GetComponent<Image>();
		timeBarTransform = gameObject.GetComponent<Transform>();

		currentValue = INITIAL_CURRENT;
		maxValue = INITIAL_MAX;
		initiated = true;

		Assert.IsNotNull(timeBar);
		Assert.IsNotNull(timeBarTransform);
	}

	/**
	 * Set the time, which starts counting down immediately
	 */
	public void setTime(float both) {
		setCurrentValue(both);
		setMaxValue(both);
	}
	
	public bool isFinished() {
		return currentValue <= 0;
	}
	
	public void enable() {
		this.initiated = true;
	}
	
	public void disable() {
		this.initiated = false;
	}

	/**
	 * Returns whether or not the timerbar is enabled
	 */
	public bool isInitiated() {
		return initiated;
	}

	public float getCurrentTime() {
		return currentValue;
	}
	
	void setCurrentValue(float value) {
		currentValue = value;
	}
	
	void setMaxValue(float value) {
		maxValue = value;
	}
	
	void setBarX() {
		Vector3 scale = timeBarTransform.localScale;
		scale.x = (currentValue / maxValue);
		timeBarTransform.localScale = scale;
	}
	
	void changeColor() {
		float value = 1 - (currentValue / maxValue);
		Color color = Color.black;
		if (value < TRANSITION_POINT) {
			color = Color.Lerp (firstColor, secondColor, (1 - (currentValue / maxValue)) / TRANSITION_POINT);
		} else {
			color = Color.Lerp (secondColor, thirdColor, (1 - (currentValue / maxValue) - TRANSITION_POINT) / TRANSITION_POINT);
		}
		timeBar.color = color;
	}
	
	void decrementTime() {
		if (currentValue > 0) {
			currentValue -= Time.deltaTime;
		} else {
			timeBar.color = DEAD_COLOR;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isInitiated()) {
			decrementTime();
			changeColor();
			setBarX();
		} else {
			timeBar.color = DISABLED_COLOR;
		}
	}
}
