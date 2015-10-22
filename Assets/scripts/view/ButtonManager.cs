using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

public class ButtonManager : MonoBehaviour {
	ColorButton[] colorButtons;

	// Use this for initialization
	void Start () {
		colorButtons = GetComponentsInChildren<ColorButton>();
		nullCheck();
	}

	void nullCheck() {
		for (int i = 0; i < colorButtons.Length; i++) {
			Assert.IsNotNull(this.colorButtons[i]);
		}
	}

	void swap(int a, int b) {
		if (a < 0 || b < 0 
		    || a > colorButtons.Length - 1 
		    || b > colorButtons.Length - 1) {
			throw new UnityException("Error: Tried to index a:" + a 
			                         + " b: " + b 
			                         + " In an array of size: " + colorButtons.Length);
		}
		colorButtons[a].swapColorWord(colorButtons[b]);
	}

	public void randomize() {
		for (int i = 0; i < colorButtons.Length; i++) {
			int j = Random.Range(0, i);
			swap(j, i);	
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
