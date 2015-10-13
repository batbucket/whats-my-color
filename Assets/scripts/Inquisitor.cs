using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inquisitor : MonoBehaviour {

	//Text question;
	ChromaWord cw;

	ChromaButton b1;
	ChromaButton b2;
	ChromaButton b3;
	ChromaButton b4;
	ChromaButton b5;
	ChromaButton b6;
	ChromaButton[] chromaButtons;

	Scorer scorer;
	Timer timer;

	int score;
	float timeLimit;

	public const float INITIAL_TIME_LIMIT = 5.0f;
	public const float TIME_DECAY = 0.05f;
	
	// Use this for initialization
	void Start() {

		//question = GameObject.Find("Text/Question").GetComponent<Text>();
		cw = GameObject.Find("Text/Word").GetComponent<ChromaWord>();
		scorer = GameObject.Find("Text/Score").GetComponent<Scorer>();
		timer = GameObject.Find("Background/Timebar").GetComponent<Timer>();
		resetTime();

		b1 = GameObject.Find ("Buttons/1").GetComponent<ChromaButton>();
		b2 = GameObject.Find ("Buttons/2").GetComponent<ChromaButton>();
		b3 = GameObject.Find ("Buttons/3").GetComponent<ChromaButton>();
		b4 = GameObject.Find ("Buttons/4").GetComponent<ChromaButton>();
		b5 = GameObject.Find ("Buttons/5").GetComponent<ChromaButton>();
		b6 = GameObject.Find ("Buttons/6").GetComponent<ChromaButton>();

		chromaButtons = new ChromaButton[GameObject.FindGameObjectsWithTag("Button").Length];
		chromaButtons[0] = b1;
		chromaButtons[1] = b2;
		chromaButtons[2] = b3;
		chromaButtons[3] = b4;
		chromaButtons[4] = b5;
		chromaButtons[5] = b6;

		for (int i = 0; i < chromaButtons.Length; i++) {
			chromaButtonAssigner(chromaButtons[i]);
		}
	}

	void resetTime() {
		timeLimit = INITIAL_TIME_LIMIT;
		timer.setTime(INITIAL_TIME_LIMIT);
	}

	void chromaButtonAssigner(ChromaButton cb) {
		cb.getButton().onClick.AddListener(() => {
			inputChroma(cb.getChroma());
		});
	}

	void inputAnswer(ChromaButton cb) {
		inputChroma(cb.getChroma());
	}

	void inputChroma(Chroma chroma) {
		if (inputChromaSuccessCondition(chroma)) {
			correctAnswer();
		} else {
			incorrectAnswer();
		}
	}

	bool inputChromaSuccessCondition(Chroma chroma) {
		return cw.isChroma(chroma);
	}

	void inputWord(Word word) {
		if (inputWordSuccessCondition(word)) {
			correctAnswer();
		} else {
			incorrectAnswer();
		}
	}

	bool inputWordSuccessCondition(Word word) {
		return cw.isWord(word);
	}

	void incorrectAnswer() {
		score = 0;
		resetTime();
		cw.randomizeChromaWord();
		shuffleButton ();
		Debug.Log ("You was wrong!");

	}

	void reduceTime() {
		timeLimit *= (1 - TIME_DECAY);
		timer.setTime(timeLimit);
	}

	void correctAnswer() {
		score++;
		cw.randomizeChromaWord();
		reduceTime();
		//shuffleButton ();
		Debug.Log ("You was right!");

	}

	void shuffleButton(){
		foreach (ChromaButton cb in chromaButtons) {
			cb.changeChroma(cb.randomChroma());
		}
		for (int i = 0; i < chromaButtons.Length; i++) {
			chromaButtonAssigner(chromaButtons[i]);
		}

	}

	void updateScore() {
		scorer.setValue(score);
	}

	// Update is called once per frame
	void Update () {
		updateScore();

		if (timer.isFinished()) {
			incorrectAnswer();
		}

	}
}
