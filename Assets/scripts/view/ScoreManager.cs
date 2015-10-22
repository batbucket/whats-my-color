using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

public class ScoreManager : MonoBehaviour {
	int value;
	
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
