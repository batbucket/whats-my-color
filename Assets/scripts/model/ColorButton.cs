using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System;

public class ColorButton : MonoBehaviour {

    Button button;
    ColorWord colorWord; //Color and Word should match
    ButtonMode buttonMode;
    public static readonly Color WORD_MODE_COLOR = Color.white;
    bool colorWordSet;

    public Action OnMouseDownAction { get; set; }
    public Action OnMouseUpAction { get; set; }

    const int DEFAULT_FONT_SIZE = 80;

    // Use this for initialization
    void Awake() {
        this.button = gameObject.GetComponent<Button>();
        this.buttonMode = ButtonMode.WORD;
        this.colorWord = new ColorWord(Color.grey, "UNDEFINED"); //Change this with setting the colorWord
        Assert.IsNotNull(this.button);
        OnMouseDownAction = () => { };
        OnMouseUpAction = () => { };
    }

    public void OnMouseDown() {
        OnMouseDownAction.Invoke();
    }

    public void OnMouseUp() {
        OnMouseUpAction.Invoke();
    }

    public Button GetButton() {
        return button;
    }

    public Color GetColor() {
        return colorWord.color;
    }

    public string GetWord() {
        return colorWord.word;
    }

    public void SetColorWord(ColorWord other) {
        if (!colorWordSet) {
            this.colorWord = other;
            colorWordSet = true;
        } else {
            throw new UnityException("Cannot set a ColorButton's ColorWord twice! Use swap() to switch!");
        }
    }

    /**
	 * Works like setColorWord(), but doesn't have the warning
	 * if you use this multiple times on one button
	 * Used in swap()
	 */
    void ForceSetColorWord(ColorWord other) {
        this.colorWord = other;
    }

    public void SetButtonMode(ButtonMode newButtonMode) {
        this.buttonMode = newButtonMode;
    }

    public ColorWord GetColorWord() {
        return colorWord;
    }

    public void SwapColorWord(ColorButton other) {
        ColorWord temp = other.GetColorWord();
        other.ForceSetColorWord(this.GetColorWord());
        this.ForceSetColorWord(temp);
    }

    void SetTextAlpha(int alpha) {
        Color color = button.GetComponentInChildren<Text>().color;
        color.a = alpha;
        button.GetComponentInChildren<Text>().color = color;
    }

    void SetTextInvisible() {
        SetTextAlpha(0);
    }

    void SetTextVisible() {
        SetTextAlpha(1);
    }

    void UpdateButtonText() {
        if (buttonMode == ButtonMode.WORD) {
            button.GetComponentInChildren<Text>().text = colorWord.word;
            SetTextVisible();
        } else if (buttonMode == ButtonMode.COLOR) {
            SetTextInvisible();
        } else {
            throw new UnityException("Unknown Button Type: " + buttonMode);
        }
    }

    void UpdateButtonDisplay() {
        if (buttonMode == ButtonMode.WORD) {
            SetButtonColor(WORD_MODE_COLOR);
        } else if (buttonMode == ButtonMode.COLOR) {
            SetButtonColor(colorWord.color);
        } else {
            throw new UnityException("Unknown Button Type: " + buttonMode);
        }
    }

    void SetButtonColor(Color color) {
        ColorBlock cb = button.GetComponent<Button>().colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        button.GetComponent<Button>().colors = cb;
    }

    // Update is called once per frame
    void Update() {
        UpdateButtonText();
        UpdateButtonDisplay();
    }
}
