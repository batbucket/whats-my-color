using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class QuestionManager : MonoBehaviour {
	Text question;

	Text coloredWord;
	ColorWord colorWord;
	QuestionType questionType;

	public const string RIGHT_COLOR_TEXT = "What's the COLOR?";
	public const string WRONG_COLOR_TEXT = "What's NOT the COLOR?";
	public const string RIGHT_WORD_TEXT = "What's the WORD?";
	public const string WRONG_WORD_TEXT = "What's NOT the WORD?";

	void Start() {
		this.question = gameObject.GetComponent<Text>();
		this.coloredWord = gameObject.GetComponentsInChildren<Text>()[1]; //Index 0 has the parent's script. Weird.

		this.questionType = QuestionType.RIGHT_WORD; //Default type of question
		randomizeColorWord();
		randomizeQuestionType();

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

	void updateColoredWordDisplay() {
		setColoredWordColor(colorWord.color);
	}

	void Update() {
		updateQuestionText();
		updateColoredWordDisplay();
	}
}
