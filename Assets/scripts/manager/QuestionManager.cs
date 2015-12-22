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
	 * The type of Question that is asked of the user
	 * Currently we have:
	 * RIGHT_COLOR: User must answer the Color of the ColorWord in some format
	 * WRONG_COLOR: Any answer that isn't the right color will work
	 * RIGHT_WORD: User must answer the Word of the ColorWord in some format
	 * WRONG_WORD: Any answer that isn't the right word will work
	 */
	QuestionMode questionMode;

	/**
	 * How the ColoredWord will be displayed
	 * TEXT: Show the ColorWord as normal (A colored text)
	 * SHAPE: Show the ColorWord as some shape that is colored the same as the ColorWord
	 */
	ColoredWordMode coloredWordMode;

	public const string RIGHT_COLOR_TEXT = "What's the COLOR?";
	public const string WRONG_COLOR_TEXT = "What's NOT the COLOR?";
	public const string RIGHT_WORD_TEXT = "What's the WORD?";
	public const string WRONG_WORD_TEXT = "What's NOT the WORD?";

	public const string COLOR_MODE_TEXT = "■■■"; //Deprecated

    /**
     * These fields are used for the text bounce effect
     */
    public const float MAX_SIZE_INCREASE = .1f;
    public const float INCREASE_RATE = .5f;

    void Start() {
		this.question = gameObject.GetComponent<Text>();
		this.coloredWord = gameObject.GetComponentsInChildren<Text>()[1]; //Index 0 has the parent's script. Weird.

		this.questionMode = QuestionMode.RIGHT_COLOR; //Default type of question
		this.coloredWordMode = ColoredWordMode.TEXT; //Default type of display
		this.colorWord = ColorWord.generateRandomColorWord();

		Assert.IsNotNull(this.question);
		Assert.IsNotNull(this.colorWord);
	}

	public void randomizeColorWord() {
		colorWord = ColorWord.generateRandomColorWord();
	}

	public void randomizeQuestionMode() {
		if (coloredWordMode == ColoredWordMode.SHAPE) {
			if (Tool.randomBoolean()) {
				questionMode = QuestionMode.RIGHT_COLOR;
			} else {
				questionMode = QuestionMode.WRONG_COLOR;
			}
		} else {
		questionMode = Tool.GetRandomEnum<QuestionMode>();
		}
	}

	public void randomizeColorWordMode() {
		coloredWordMode = Tool.GetRandomEnum<ColoredWordMode>();
	}
	
	public bool isCorrectAnswer(ColorWord cw) {
		if (questionMode == QuestionMode.RIGHT_COLOR) {
			return cw.color.Equals(this.getColor());
		} else if (questionMode == QuestionMode.WRONG_COLOR) {
			return !cw.color.Equals(this.getColor());
		} else if (questionMode == QuestionMode.RIGHT_WORD) {
			return cw.word.Equals(this.getWord());
		} else if (questionMode == QuestionMode.WRONG_WORD) {
			return !cw.word.Equals(this.getWord());
		} else {
			throw new UnityException("Unknown enum type: " + this.questionMode);
		}
	}

	public ColoredWordMode getColoredWordMode() {
		return coloredWordMode;
	}

	public QuestionMode getQuestionMode() {
		return questionMode;
	}
	
	ColorWord getColorWord() {
		return colorWord;
	}
	
	string getWord() {
		return colorWord.word;
	}
	
	Color getColor() {
		return colorWord.color;
	}

	void setQuestionMode(QuestionMode questionMode) {
		this.questionMode = questionMode;
	}

	void setColoredWordMode(ColoredWordMode coloredWordMode) {
		this.coloredWordMode = coloredWordMode;
	}

	void updateQuestionText() {
		if (questionMode == QuestionMode.RIGHT_COLOR) {
			question.text = RIGHT_COLOR_TEXT;
		} else if (questionMode == QuestionMode.WRONG_COLOR) {
			question.text = WRONG_COLOR_TEXT;
		} else if (questionMode == QuestionMode.RIGHT_WORD) {
			question.text = RIGHT_WORD_TEXT;
		} else if (questionMode == QuestionMode.WRONG_WORD) {
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
		if (coloredWordMode == ColoredWordMode.TEXT) {
			setColoredWordText(colorWord.word);
		} else if (coloredWordMode == ColoredWordMode.SHAPE) {
			setColoredWordText(COLOR_MODE_TEXT);
		} else {
			throw new UnityException("Unknown enum: " + coloredWordMode);
		}
		setColoredWordColor(colorWord.color);
	}

	void Update() {
		updateQuestionText();
		updateColoredWordDisplay();
		Tool.bouncyEffect(GameObject.Find("ColoredWord"), MAX_SIZE_INCREASE, INCREASE_RATE);
	}
}
