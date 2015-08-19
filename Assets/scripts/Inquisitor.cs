using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inquisitor : MonoBehaviour {

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
	const float TIME_LIMIT = -1;
	
	// Use this for initialization
	void Start() {
		question = GameObject.Find("Text/Question").GetComponent<Text>();
		cw = GameObject.Find("Text/Word").GetComponent<ChromaWord>();
		scoreText = GameObject.Find("Text/Score").GetComponent<Text>();

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

	void inputChroma(Chroma chroma) {
		if (cw.isChroma(chroma)) {
			correctAnswer();
		} else {
			incorrectAnswer();
		}
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
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();
	}
}
