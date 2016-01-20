using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

/**
 * This class manages the score display
 */
public class ScoreManager : MonoBehaviour {
	int value; //Number that will be displayed by the score text

	// Use this for initialization
	void Start () {
		Assert.IsNotNull(gameObject.GetComponent<Text>().text);

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
