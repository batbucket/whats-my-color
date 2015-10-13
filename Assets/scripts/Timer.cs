using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	Image timeBar;
	Transform timeBarTransform;

	public Color firstColor;
	public Color secondColor;
	public Color thirdColor;

	const float TRANSITION_POINT = .5f;

	Color DISABLED_COLOR = Color.gray;
	Color DEAD_COLOR = Color.black;
	
	float currentValue;
	const float INITIAL_CURRENT = 10;

	float maxValue;
	const float INITIAL_MAX = 10;

	bool initiated;
	bool finished;
	
	// Use this for initialization
	void Awake () {
		timeBar = gameObject.GetComponent<Image>();
		timeBarTransform = gameObject.GetComponent<Transform>();
		
		currentValue = INITIAL_CURRENT;
		maxValue = INITIAL_MAX;
		initiated = true;
		finished = false;
	}
	
	void setCurrentValue(float value) {
		currentValue = value;
	}
	
	void setMaxValue(float value) {
		maxValue = value;
	}
	
	public void setTime(float both) {
		setCurrentValue(both);
		setMaxValue(both);
	}

	void reset() {
		currentValue = maxValue;
		finished = false;
	}
	
	public bool isFinished() {
		return finished;
	}

	public void enable() {
		this.initiated = true;
	}

	public void disable() {
		this.initiated = false;
	}

	public bool isInitiated() {
		return initiated;
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
			finished = false;
		} else {
			timeBar.color = DEAD_COLOR;
			finished = true;
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
