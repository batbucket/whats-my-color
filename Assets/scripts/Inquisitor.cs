﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inquisitor : MonoBehaviour {

	enum Question {
		Color, Word
	}

	enum Answer {
		Color, Word
	}

	Text question;
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
	Transform timeBar;
	public const float TIME_LIMIT = 5.0f;
	
	// Use this for initialization
	void Start() {
		question = GameObject.Find("Text/Question").GetComponent<Text>();
		cw = GameObject.Find("Text/Word").GetComponent<ChromaWord>();
		scoreText = GameObject.Find("Text/Score").GetComponent<Text>();
		timeBar = GameObject.Find("Background/Timebar").GetComponent<Transform>();

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
		Debug.Log ("You was wrong!");
	}

	void correctAnswer() {
		score++;
		cw.randomizeChromaWord();
		Debug.Log ("You was right!");
	}

	float timeRemainingAsPercentage() {
		return (TIME_LIMIT - time) / TIME_LIMIT;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();

		if (TIME_LIMIT > 0 && time < TIME_LIMIT) {
		time += Time.deltaTime;
			Debug.Log ((TIME_LIMIT - time) / TIME_LIMIT);
			timeBar.transform.localScale = new Vector3(transform.localScale.x * timeRemainingAsPercentage(), transform.localScale.y, transform.localScale.z);
		}
	}
}
