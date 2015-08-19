using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChromaButton : MonoBehaviour {

	Button button;
	public Chroma chroma;
	Color color;

	// Use this for initialization
	void Awake () {
		this.button = gameObject.GetComponent<Button>();
	}

	public Button getButton() {
		return button;
	}

	public Chroma getChroma() {
		return chroma;
	}

	public void changeChroma(Chroma chroma) {
		this.chroma = chroma;
	}
	
	// Update is called once per frame
	void Update () {
		button.GetComponentInChildren<Text>().text = chroma.ToString();
	}
}
