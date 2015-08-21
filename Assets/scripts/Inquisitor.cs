using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inquisitor : MonoBehaviour {

	public enum Question {
		Color, Word
	}

	public enum Answer {
		Color, Word
	}

	//Text question;
	ChromaWord cw;
	Text scoreText;

	ChromaButton b1;
	ChromaButton b2;
	ChromaButton b3;
	ChromaButton b4;
	ChromaButton b5;
	ChromaButton b6;
	ChromaButton[] chromaButtons;
	
	int score;
	float time;
	float timeLimit;
	GameObject timeBar;
	float timeDecayAccumulated;
	const float TIME_LIMIT = 3.0f;
	public const float TIME_DECAY = 0.95f;
	
	// Use this for initialization
	void Start() {

		//question = GameObject.Find("Text/Question").GetComponent<Text>();
		cw = GameObject.Find("Text/Word").GetComponent<ChromaWord>();
		scoreText = GameObject.Find("Text/Score").GetComponent<Text>();
		timeBar = GameObject.Find("Background/Timebar");

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
		resetTimeLimit();
		resetTime();
		cw.randomizeChromaWord();
		shuffleButton ();
		Debug.Log ("You was wrong!");

	}

	void resetTimeLimit() {
		timeLimit = TIME_LIMIT;
	}

	void resetTime() {
		time = 0.0f;
	}

	void correctAnswer() {
		score++;
		cw.randomizeChromaWord();
		resetTime();
		timeLimit *= TIME_DECAY;
		shuffleButton ();
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
			        
	

	float timeRemainingAsPercentage() {
		return (timeLimit - time) / timeLimit;
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();

		if (time < 0) {
			timeBar.GetComponent<Image>().color = Color.gray;
		} else {
			timeBar.GetComponent<Image>().color = Color.black;
		}

		if (timeLimit > 0 && time < timeLimit) {
		time += Time.deltaTime;
			Debug.Log ((timeLimit - time) / timeLimit);
			timeBar.GetComponent<Transform>().transform.localScale = new Vector3(transform.localScale.x * timeRemainingAsPercentage(), transform.localScale.y, transform.localScale.z);
		} else if (false || TIME_LIMIT <= 0) { //lol bypass unreachable code error
			timeBar.SetActive(false);
		} else {
			incorrectAnswer();
		}
	}
}
