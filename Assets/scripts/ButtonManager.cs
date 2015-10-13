using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	ColorButton b1;
	ColorButton b2;
	ColorButton b3;
	ColorButton b4;
	ColorButton b5;
	ColorButton b6;
	ColorButton[] colorButtons;

	// Use this for initialization
	void Start () {

		/**
		 * Buttons are arranged as follows:
		 * 				b1 b4
		 * 				b2 b5
		 * 				b3 b6
		 */
		b1 = GameObject.Find ("Buttons/1").GetComponent<ColorButton>();
		b2 = GameObject.Find ("Buttons/2").GetComponent<ColorButton>();
		b3 = GameObject.Find ("Buttons/3").GetComponent<ColorButton>();
		b4 = GameObject.Find ("Buttons/4").GetComponent<ColorButton>();
		b5 = GameObject.Find ("Buttons/5").GetComponent<ColorButton>();
		b6 = GameObject.Find ("Buttons/6").GetComponent<ColorButton>();

		colorButtons = new ColorButton[] {b1, b2, b3, b4, b5, b6};
	}

	void swap(int a, int b) {
		ColorButton temp = colorButtons[b];
		colorButtons[b] = colorButtons[a];
		colorButtons[a] = temp;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
