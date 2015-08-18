using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inquisitor : MonoBehaviour {
	
	enum Status {
		Questioning, Correct, Incorrect
	}

	int score;
	float time;
	const float TIME_LIMIT = 10;
	
	// Use this for initialization
	void Start() {
		Text question = GameObject.Find("Text/Question").GetComponent<Text>();
		Text word = GameObject.Find("Text/Word").GetComponent<Text>();

		Button _1 = GameObject.Find("Button/1").GetComponent<Button>();
		Button _2 = GameObject.Find("Button/2").GetComponent<Button>();
		Button _3 = GameObject.Find("Button/3").GetComponent<Button>();
		Button _4 = GameObject.Find("Button/4").GetComponent<Button>();
		Button _5 = GameObject.Find("Button/5").GetComponent<Button>();
		Button _6 = GameObject.Find("Button/6").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
