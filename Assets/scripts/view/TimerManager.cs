﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class TimerManager : MonoBehaviour {
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

	public void setTime(float both) {
		setCurrentValue(both);
		setMaxValue(both);
	}
	
	public bool isFinished() {
		return currentValue == 0;
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
		Debug.Log(isFinished());
		if (isInitiated()) {
			decrementTime();
			changeColor();
			setBarX();
		} else {
			timeBar.color = DISABLED_COLOR;
		}
	}
}