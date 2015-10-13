using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scorer : MonoBehaviour {
	int value;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text>().text = "null";
	}

	public void setValue(int newValue) {
		this.value = newValue;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text = "" + value;
	}
}
