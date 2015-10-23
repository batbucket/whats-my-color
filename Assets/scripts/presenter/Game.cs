using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

/**
 * This class represents the most basic game mode:
 * 
 * X seconds per question
 * No time decay
 * One type of question: What is the correct color
 * No button shuffling
 */
abstract public class Game : MonoBehaviour {
	protected QuestionManager questionManager;
	protected ScoreManager scoreManager;
	protected ButtonManager buttonManager;
	protected TimerManager timerManager;
	protected EffectsManager effectsManager;

	public const float INITIAL_TIME_PER_QUESTION = 5.0f;
	protected float timePerQuestion;
	int score;

	// Use this for initialization
	void Awake () {
		this.questionManager = gameObject.GetComponentInChildren<QuestionManager>();
		this.scoreManager = gameObject.GetComponentInChildren<ScoreManager>();
		this.buttonManager = gameObject.GetComponentInChildren<ButtonManager>();
		this.timerManager = gameObject.GetComponentInChildren<TimerManager>();
		this.effectsManager = gameObject.GetComponentInChildren<EffectsManager>();
		this.timePerQuestion = INITIAL_TIME_PER_QUESTION;
		nullCheck();
		gameInitialization();
	}

	void nullCheck() {
		Assert.IsNotNull(this.questionManager);
		Assert.IsNotNull(this.scoreManager);
		Assert.IsNotNull(this.scoreManager);
		Assert.IsNotNull(this.timerManager);
		Assert.IsNotNull(this.effectsManager);
	}

	void gameInitialization() {
		setupQuestionManager();
		setupScoreManager();
		setupButtonManager();
		setUpTimerManager();
	}
	
	void setupQuestionManager() {
		nextQuestion();
	}

	void setupScoreManager() {
		scoreManager.setValue(0);
		score = 0;
	}

	void setupButtonManager() {
		ColorButton[] cbs = buttonManager.getColorButtons();

		/**
		 * Length of Color Buttons in array should match with the
		 * readonly approved arrays in ColorWord since we're going
		 * to iterate over all 3 to setup their ColorWords
				 */
		Assert.AreEqual(cbs.Length, ColorWord.approvedColors.Length);
		Assert.AreEqual(cbs.Length, ColorWord.approvedWords.Length);
		Assert.AreEqual(ColorWord.approvedColors.Length, ColorWord.approvedWords.Length);

		/**
		 * Set up the ColorWords associated with the buttons
		 */
		for (int i = 0; i < cbs.Length; i++) {
			cbs[i].setColorWord(ColorWord.generateMatchingColorWord(i));
		}

		/**
		 * Set up the listeners of the buttons
		 */
		for (int i = 0; i < cbs.Length; i++) {
			ColorButton cb = cbs[i];
			cb.getButton().onClick.AddListener(() => {
				submitAnswer(cb.getColorWord());
			});
		}

		buttonManager.shuffleButtons(); //Shuffle once

	}

	void submitAnswer(ColorWord cwOfButton) {
		if (isCorrectAnswer(cwOfButton)) {
			correctAnswer();
		} else {
			incorrectAnswer();
		}
	}

	/**
	 * Conditions for a correct answer should go here
	 * 
	 * For a basic game, the condition will be true if
	 * the color held by the ColorButton (which should match its text)
	 * matches the color held by the question
	 */
	bool isCorrectAnswer(ColorWord cwOfButton) {
		return this.questionManager.isCorrectAnswer(cwOfButton);
	}

	/**
	 * Things that happen when you get the answer right
	 */
	void correctAnswer() {
		correctEffects();
		updateScore();
		nextQuestion();
		resetTime();
	}

	void correctEffects() {
		effectsManager.indicateSuccess();
	}

	/**
	 * Things that happen when you get the answer wrong
	 */
	void incorrectAnswer() {
		Debug.Log("You was wrong!");
	} 

	void incorrectEffects() {
		effectsManager.indicateFailure();
	}

	/**
	 * This one just randomizes
	 */
	protected virtual void nextQuestion() {
		questionManager.randomizeColorWord();
	}

	protected virtual void resetTime() {
		timerManager.setTime(timePerQuestion);
		Debug.Log ("New time: " + timePerQuestion);
	}

	void updateScore() {
		scoreManager.setValue(++score);
	}

	void setUpTimerManager() {
		timerManager.setTime(timePerQuestion);
	}

	bool isOutOfTime() {
		return timerManager.isFinished();
	}

	/**
	 * Check if the user is out of time
	 */
	void Update () {
		if (isOutOfTime()) {
			incorrectAnswer();
		}
	}
}
