using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ModeController : MonoBehaviour {

	public const string MODE_ID_LOCATION = "Mode_ID";

	public const string NO_MODE_TEXT = "SELECT A DIFFICULTY.";
	public const string EASY_MODE_TEXT = "<color=green>EASY: PICK THE COLOR, NOT THE WORD.</color>";
	public const string NORMAL_MODE_TEXT = "<color=orange>NORMAL: ...WITH TIME DECAY!</color>";
	public const string HARD_MODE_TEXT = "<color=red>HARD: ...AND SHUFFLED BUTTONS!</color>";
	public const string IMPOSSIBLE_MODE_TEXT = "<color=blue>IMPOSSIBLE: ???</color>";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		displayDifficultyText();
		displayHiScores();
	}

	public void confirmSelectionButtonClicked() {
		SceneManager.LoadScene("GenericGame");
	}

	public void easyButtonClicked() {
		setModeID(EasyGame.ID);
	}

	public void normalButtonClicked() {
		setModeID(NormalGame.ID);
	}

	public void hardButtonClicked() {
		setModeID(HardGame.ID);
	}

	public void impossibleButtonClicked() {
		setModeID(ImpossibleGame.ID);	
	}

	void setModeID(int ID) {
		PlayerPrefs.SetInt(MODE_ID_LOCATION, ID);
	}

	void setDifficultyText(string s) {
		GameObject.Find("DifficultyDescription").GetComponent<Text>().text = s;
	}

	public static int getModeID() {
		return PlayerPrefs.GetInt(MODE_ID_LOCATION);
	}

	void displayDifficultyText() {
		switch(getModeID()) {
		case EasyGame.ID:
			setDifficultyText(EASY_MODE_TEXT);
			break;
		case NormalGame.ID:
			setDifficultyText(NORMAL_MODE_TEXT);
			break;
		case HardGame.ID:
			setDifficultyText(HARD_MODE_TEXT);
			break;
		case ImpossibleGame.ID:
			setDifficultyText(IMPOSSIBLE_MODE_TEXT);
			break;
		default:
			setDifficultyText(NO_MODE_TEXT);
			break;
		}
	}

	void displayHiScores() {
		GameObject.Find("EasyButton").GetComponentInChildren<Text>().text = "" + PlayerPrefs.GetInt(EasyGame.EASY_SCORE_LOCATION);
		GameObject.Find("NormalButton").GetComponentInChildren<Text>().text = "" + PlayerPrefs.GetInt(NormalGame.NORMAL_SCORE_LOCATION);
		GameObject.Find("HardButton").GetComponentInChildren<Text>().text = "" + PlayerPrefs.GetInt(HardGame.HARD_SCORE_LOCATION);
		GameObject.Find("ImpossibleButton").GetComponentInChildren<Text>().text = "" + PlayerPrefs.GetInt(ImpossibleGame.IMPOSSIBLE_SCORE_LOCATION);
	}
}
