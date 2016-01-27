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

    float currentValue; //Current value of the timer
    const float INITIAL_CURRENT = 10;

    float maxValue; //Value the timer will count down from
    const float INITIAL_MAX = 10;

    bool initiated; //Determines if the timer is active or not

    // Use this for initialization
    void Awake() {
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
    public void SetTime(float both) {
        SetCurrentValue(both);
        SetMaxValue(both);
    }

    public bool IsFinished() {
        return currentValue <= 0;
    }

    public void Enable() {
        this.initiated = true;
    }

    public void Disable() {
        this.initiated = false;
    }

    /**
	 * Returns whether or not the timerbar is enabled
	 */
    public bool IsInitiated() {
        return initiated;
    }

    public float GetCurrentTime() {
        return currentValue;
    }

    void SetCurrentValue(float value) {
        currentValue = value;
    }

    void SetMaxValue(float value) {
        maxValue = value;
    }

    /**
     * Scales the X of the time bar image
     */
    void SetBarX() {
        Vector3 scale = timeBarTransform.localScale;
        scale.x = (currentValue / maxValue);
        timeBarTransform.localScale = scale;
    }

    void ChangeColor() {
        float value = 1 - (currentValue / maxValue);
        Color color = Color.black;
        if (value < TRANSITION_POINT) {
            color = Color.Lerp(firstColor, secondColor, (1 - (currentValue / maxValue)) / TRANSITION_POINT); //Go from first and second color before .5 on the timer
        } else {
            color = Color.Lerp(secondColor, thirdColor, (1 - (currentValue / maxValue) - TRANSITION_POINT) / TRANSITION_POINT); //Go from second to third after .5 on the timer
        }
        timeBar.color = color;
    }

    void DecrementTime() {
        if (currentValue > 0) {
            currentValue -= Time.deltaTime;
        } else {
            timeBar.color = DEAD_COLOR;
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsInitiated()) {
            DecrementTime();
            ChangeColor();
            SetBarX();
        } else {
            timeBar.color = DISABLED_COLOR;
        }
    }
}
