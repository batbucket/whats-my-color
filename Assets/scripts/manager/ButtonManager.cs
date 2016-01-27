using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

/**
 * This class manages the buttons on the screen
 * Currently there's 6 buttons, but it should be able to get any number of
 * them as long as they're assigned to it as its children
 */
public class ButtonManager : MonoBehaviour {
    ColorButton[] colorButtons; //Array of the ColorButtons that are the manager's children

    // Use this for initialization
    void Awake() {
        this.colorButtons = GetComponentsInChildren<ColorButton>();
        NullCheck();
    }

    void NullCheck() {
        Assert.IsNotNull(this.colorButtons);
        for (int i = 0; i < colorButtons.Length; i++) {
            Assert.IsNotNull(this.colorButtons[i]);
        }
    }

    /**
	 * Shuffles the buttons that are displayed
	 * by exchanging their ColorWords
	 */
    public void ShuffleButtons() {
        for (int i = 0; i < colorButtons.Length; i++) {
            int j = Random.Range(0, i);
            SwapColorWord(j, i);
        }
    }

    public ColorButton[] GetColorButtons() {
        return colorButtons;
    }

    public void RandomizeColorButtons() {
        for (int i = 0; i < colorButtons.Length; i++) {
            if (Random.value >= 0.5) {
                SetButtonMode(Tool.GetRandomEnum<ButtonMode>(), i);
            }
        }
    }

    void SetWordMode() {
        SetAllButtonModes(ButtonMode.WORD);
    }

    void SetColorMode() {
        SetAllButtonModes(ButtonMode.COLOR);
    }

    /**
     * There are two button modes
     * Display Text and Display Color
     */
    void SetButtonMode(ButtonMode mode, int buttonIndex) {
        colorButtons[buttonIndex].SetButtonMode(mode);
    }

    /**
     * Set the modes of all buttons
     */
    void SetAllButtonModes(ButtonMode mode) {
        for (int i = 0; i < colorButtons.Length; i++) {
            SetButtonMode(mode, i);
        }
    }

    /**
	 * Swap the ColorWords of two buttons given their indicies
     * In the array
     */
    void SwapColorWord(int a, int b) {
        if (a < 0 || b < 0
            || a > colorButtons.Length - 1
            || b > colorButtons.Length - 1) {
            throw new UnityException("Error: Tried to index a:" + a
                                     + " b: " + b
                                     + " In an array of size: " + colorButtons.Length);
        }
        colorButtons[a].SwapColorWord(colorButtons[b]);
    }

    // Update is called once per frame
    void Update() {

    }
}
