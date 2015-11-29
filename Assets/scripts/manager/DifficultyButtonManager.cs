using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyButtonManager : MonoBehaviour {
	Button[] buttons;
	public const string DIFFICULTY_ID_STORAGE_NAME = "Difficulty_ID";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeScene() {
		Application.LoadLevel("GenericGame");
	}

	public void easyButtonClicked() {
		PlayerPrefs.SetInt(DIFFICULTY_ID_STORAGE_NAME, EasyGame.ID);
	}

	public void normalButtonClicked() {
		PlayerPrefs.SetInt(DIFFICULTY_ID_STORAGE_NAME, NormalGame.ID);
	}

	public void hardButtonClicked() {
		PlayerPrefs.SetInt(DIFFICULTY_ID_STORAGE_NAME, HardGame.ID);
	}

	public void impossibleButtonClicked() {
		PlayerPrefs.SetInt(DIFFICULTY_ID_STORAGE_NAME, ImpossibleGame.ID);
	}
}
