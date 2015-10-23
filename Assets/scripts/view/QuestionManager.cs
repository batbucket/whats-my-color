using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

/**
 * This class manages the Question and the ColorWord one is asked to identify in some way
 */
public class QuestionManager : MonoBehaviour {
	Text question; //Displays the objective of the question to the user (what they have to answer)
	Text coloredWord; //The Color Word that is actually displayed to the user
	ColorWord colorWord; //The internal ColorWord that affects the coloredWord display

	/**
	 * These fields are used for the text bounce effect
	 */
	public const float MAX_SIZE_INCREASE = .2f; //The largest possible amount the text can increase by before it starts getting smaller
	public const float INCREASE_RATE = .5f; //Larger numbers means larger rate of shifting
	bool increasing;

	/**
	 * The type of Question that is asked of the user
	 * Currently we have:
	 * RIGHT_COLOR: User must answer the Color of the ColorWord in some format
	 * WRONG_COLOR
	 * RIGHT_WORD: User must answer the Word of the ColorWord in some format
	 * WRONG_WORD
	 */
	QuestionType questionType;

	/**
	 * How the ColoredWord will be displayed
	 * TEXT: Show the ColorWord as normal (A colored text)
	 * SHAPE: Show the ColorWord as some shape that is colored the same as the ColorWord
	 */
	ColoredWordType coloredWordType;

	public const string RIGHT_COLOR_TEXT = "What's the COLOR?";
	public const string WRONG_COLOR_TEXT = "What's NOT the COLOR?";
	public const string RIGHT_WORD_TEXT = "What's the WORD?";
	public const string WRONG_WORD_TEXT = "What's NOT the WORD?";

	public const string COLOR_MODE_TEXT = "■■■";

	void Start() {
		this.question = gameObject.GetComponent<Text>();
		this.coloredWord = gameObject.GetComponentsInChildren<Text>()[1]; //Index 0 has the parent's script. Weird.

		this.questionType = QuestionType.RIGHT_COLOR; //Default type of question
		this.coloredWordType = ColoredWordType.TEXT; //Default type of display
		this.colorWord = ColorWord.generateRandomColorWord();

		Assert.IsNotNull(this.question);
		Assert.IsNotNull(this.colorWord);
	}

	public ColorWord getColorWord() {
		return colorWord;
	}

	public string getWord() {
		return colorWord.word;
	}

	public Color getColor() {
		return colorWord.color;
	}

	public void randomizeColorWord() {
		colorWord = ColorWord.generateRandomColorWord();
	}

	public void randomizeQuestionType() {
		questionType = Tool.GetRandomEnum<QuestionType>();
	}

	public void setQuestionType(QuestionType questionType) {
		this.questionType = questionType;
	}

	public void setColoredWordType(ColoredWordType coloredWordType) {
		this.coloredWordType = coloredWordType;
	}

	void updateQuestionText() {
		if (questionType == QuestionType.RIGHT_COLOR) {
			question.text = RIGHT_COLOR_TEXT;
		} else if (questionType == QuestionType.WRONG_COLOR) {
			question.text = WRONG_COLOR_TEXT;
		} else if (questionType == QuestionType.RIGHT_WORD) {
			question.text = RIGHT_WORD_TEXT;
		} else if (questionType == QuestionType.WRONG_WORD) {
			question.text = WRONG_WORD_TEXT;
		} else {
			throw new UnityException("Unknown enum type: " + this);
		}
	}

	void setColoredWordColor(Color color) {
		coloredWord.color = color;
	}

	void setColoredWordText(string text) {
		coloredWord.text = text;
	}

	void updateColoredWordDisplay() {
		if (coloredWordType == ColoredWordType.TEXT) {
			setColoredWordText(colorWord.word);
		} else if (coloredWordType == ColoredWordType.SHAPE) {
			setColoredWordText(COLOR_MODE_TEXT);
		} else {
			throw new UnityException("Unknown enum: " + coloredWordType);
		}
		setColoredWordColor(colorWord.color);
	}

	void textBounce() {
		Vector4 cwScale = coloredWord.GetComponent<Transform>().localScale;
		if (cwScale.x > 1 + MAX_SIZE_INCREASE) {
			increasing = false;
		} else if (cwScale.x < 1) {
			increasing = true;
		}

		float timeValue = Time.deltaTime * INCREASE_RATE;

		if (increasing) {
			cwScale.x += timeValue;
			cwScale.y += timeValue;
		} else {
			cwScale.x -= timeValue;
			cwScale.y -= timeValue;
		}

		coloredWord.GetComponent<Transform>().localScale = cwScale;
	}

	void Update() {
		updateQuestionText();
		updateColoredWordDisplay();
		textBounce();
	}
}
