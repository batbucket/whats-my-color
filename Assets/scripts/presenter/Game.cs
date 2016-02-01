using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * This class represents the most basic game mode:
 *
 * X seconds per question
 * No time decay
 * One type of question: What is the correct color
 * No button shuffling
 *
 * Template methods are used significantly in this class
 * Meant to be changed by its subclasses to increase the difficulty
 */
abstract public class Game : MonoBehaviour {
    protected QuestionManager questionManager;
    protected ScoreManager scoreManager;
    protected ButtonManager buttonManager;
    protected TimerManager timerManager;
    protected EffectsManager effectsManager;

    public const float INITIAL_TIME_PER_QUESTION = 5.0f;
    protected float timePerQuestion;
    protected int score;

    public const string GAME_MODE_LOCATION = "Game_Mode";
    public const string LOSS_REASON_LOCATION = "Loss_Reason";
    public const string TIME_OUT_MESSAGE = "OUT OF TIME...";
    public const string WRONG_ANSWER_MESSAGE = "A NATURAL MISTAKE.";

    /**
     * If the user is holding down a button and time runs out, the game
     * will first check if the currentButton down has the correct answer
     * and count it as correct if so,
     * otherwise, the game ends as normal
     */
    ColorButton currentButtonDown;

    // Use this for initialization
    void Awake() {
        this.questionManager = gameObject.GetComponentInChildren<QuestionManager>();
        this.scoreManager = gameObject.GetComponentInChildren<ScoreManager>();
        this.buttonManager = gameObject.GetComponentInChildren<ButtonManager>();
        this.timerManager = gameObject.GetComponentInChildren<TimerManager>();
        this.effectsManager = gameObject.GetComponentInChildren<EffectsManager>();
        this.timePerQuestion = INITIAL_TIME_PER_QUESTION;
        NullCheck();
        ManagerInitialization();
    }

    void NullCheck() {
        Assert.IsNotNull(this.questionManager);
        Assert.IsNotNull(this.scoreManager);
        Assert.IsNotNull(this.scoreManager);
        Assert.IsNotNull(this.timerManager);
        Assert.IsNotNull(this.effectsManager);
    }

    void ManagerInitialization() {
        SetupQuestionManager();
        SetupScoreManager();
        SetupButtonManager();
        SetUpTimerManager();
    }

    void SetupQuestionManager() {
        NextQuestion();
    }

    void SetupScoreManager() {
        scoreManager.SetValue(0);
        score = 0;
    }

    void SetupButtonManager() {
        ColorButton[] cbs = buttonManager.GetColorButtons();

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
         * and the listeners of the buttons
		 */
        int index = 0;
        foreach (ColorButton cb in buttonManager.GetColorButtons()) {
            cb.SetColorWord(ColorWord.CreateMatchingColorWord(index++));

            /**
             * We set cb2 = cb since foreach loops are weird.
             *
             * Basically, cb changes with each iteration of the loop
             * so cb in the lambdas change on each new iteration of the loop
             * ultimately becoming whatever is the last element of the loop
             * so all 6 buttons would submit the same answer, which we don't want.
             *
             * Setting a cb2 = cb creates a new ColorButton variable on each loop
             * Ensuring that the cb in the lambda expression does not change with
             * each iteration.
             *
             * See: https://goo.gl/4wp7Hb and https://goo.gl/0ez7YV
             */
            ColorButton cb2 = cb;
            cb2.GetButton().onClick.AddListener(() => {
                SubmitAnswer(cb2.GetColorWord());
            });
            cb2.OnMouseDownAction = () => currentButtonDown = cb2;
            cb2.OnMouseUpAction = () => currentButtonDown = null;
        }

        buttonManager.ShuffleButtons(); //Shuffle once
    }

    void SubmitAnswer(ColorWord cwOfButton) {
        if (IsCorrectAnswer(cwOfButton)) {
            OnCorrectAnswer();
        } else {

            //Check cyan and blue mix-up achievements
            if (questionManager.GetColor() == Color.cyan && cwOfButton.color == Color.blue) { Tool.unlockAchievement(Achievements.achievement_its_cyan_not_blue); }
            if (questionManager.GetColor() == Color.blue && cwOfButton.color == Color.cyan) { Tool.unlockAchievement(Achievements.achievement_its_blue_not_cyan); }

            SaveLossReason(WRONG_ANSWER_MESSAGE);
            OnIncorrectAnswer();
        }
    }

    /**
	 * Conditions for a correct answer should go here
	 *
	 * For a basic game, the condition will be true if
	 * the color held by the ColorButton (which should match its text)
	 * matches the color held by the question
	 */
    bool IsCorrectAnswer(ColorWord cwOfButton) {
        return this.questionManager.IsCorrectAnswer(cwOfButton);
    }

    /**
	 * Things that happen when you get the answer right
	 */
    void OnCorrectAnswer() {
        CorrectEffects();
        UpdateScore();
        NextQuestion();
        ResetTime();
    }

    void CorrectEffects() {
        effectsManager.IndicateSuccess();
    }

    /**
	 * Things that happen when you get the answer wrong
	 */
    void OnIncorrectAnswer() {
        SaveScore();
        SaveGameModeName();
        SceneManager.LoadScene("GameEnd");
    }

    /**
	 * This one just randomizes the next question
	 */
    protected virtual void NextQuestion() {
        questionManager.RandomizeColorWord();
    }

    protected virtual void ResetTime() {
        timerManager.SetTime(timePerQuestion);
        Debug.Log("New time: " + timePerQuestion);
    }

    void UpdateScore() {
        scoreManager.SetValue(++score);
    }

    void SetUpTimerManager() {
        timerManager.SetTime(timePerQuestion);
    }

    bool IsOutOfTime() {
        return timerManager.IsFinished();
    }

    abstract protected void SaveScore();

    void SaveGameModeName() {
        string s = this.GetType().Name;
        s = s.Remove(s.Length - 4); //remove "Game" from class name
        PlayerPrefs.SetString(GAME_MODE_LOCATION, s);
    }

    void SaveLossReason(string reason) {
        PlayerPrefs.SetString(LOSS_REASON_LOCATION, reason);
    }

    /**
	 * Check if the user is out of time
	 */
    void Update() {
        if (IsOutOfTime()) {
            if (currentButtonDown != null) {
                SubmitAnswer(currentButtonDown.GetColorWord());
            } else {
                SaveLossReason(TIME_OUT_MESSAGE);
                OnIncorrectAnswer();
            }
        }
    }
}
