using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class ModeController : MonoBehaviour {

	public const string MODE_ID_LOCATION = "Mode_ID";

	public const string NO_MODE_TEXT = "NO SELECTION";
    public const string HAS_MODE_TEXT = "CONFIRM SELECTION";

	public const string EASY_MODE_TEXT = "<color=green>EASY: PICK THE COLOR, NOT THE WORD.</color>";
	public const string NORMAL_MODE_TEXT = "<color=orange>NORMAL: TIME PER QUESTION DECAYS.</color>";
	public const string HARD_MODE_TEXT = "<color=red>HARD: BUTTONS ARE RANDOMIZED.</color>";
	public const string IMPOSSIBLE_MODE_TEXT = "<color=blue>IMPOSSIBLE: ???</color>";

    public const string NORMAL_LOCKED_TEXT = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=orange>NORMAL MODE</color>.</color></i>";
    public const string HARD_LOCKED_TEXT = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=red>HARD MODE</color>.</color></i>";
    public const string IMPOSSIBLE_LOCKED_TEXT = "<i><color=grey>SCORE {0}+ TO UNLOCK <color=blue>IMPOSSIBLE MODE</color>.</color></i>";

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        conditionalConfirmActivatedState();
        conditionalConfirmText();
        conditionalModes();
		displayDifficultyText();
		displayHiScores();
	}

	public void loadGame() {
		SceneManager.LoadScene("GenericGame");
	}

    public void loadStart() {
        SceneManager.LoadScene("Start");
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

    /**
     * Extra text is added if there's a locked next mode
     */
	void displayDifficultyText() {
        string displayText = "";

		switch(getModeID()) {
		case EasyGame.ID:
            displayText = EASY_MODE_TEXT;
            if (!NormalGame.hasMetRequirement()) {
                displayText += "\n" + string.Format(NORMAL_LOCKED_TEXT, NormalGame.SCORE_REQUIREMENT);
            }
            break;
		case NormalGame.ID:
            displayText = NORMAL_MODE_TEXT;
            if (!HardGame.hasMetRequirement()) {
                displayText += "\n" + string.Format(HARD_LOCKED_TEXT, HardGame.SCORE_REQUIREMENT);
            }
            break;
		case HardGame.ID:
            displayText = HARD_MODE_TEXT;
            if (!ImpossibleGame.hasMetRequirement()) {
                displayText += "\n" + string.Format(IMPOSSIBLE_LOCKED_TEXT, ImpossibleGame.SCORE_REQUIREMENT);
            }
            break;
		case ImpossibleGame.ID:
            displayText = IMPOSSIBLE_MODE_TEXT;
			break;
		default:
            displayText = "";
            break;
		}

        setDifficultyText(displayText);
	}

	void displayHiScores() {
		GameObject.Find("EasyButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION);
		GameObject.Find("NormalButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION);
        GameObject.Find("HardButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION);
        GameObject.Find("ImpossibleButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(ImpossibleGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(ImpossibleGame.HISCORE_LOCATION);
    }

    /**
     * Button is disabled if no mode has ever been selected, say, on the first program run
     */
    void conditionalConfirmActivatedState() {
        GameObject.Find("ConfirmSelectionButton").GetComponent<Button>().interactable = getModeID() != 0;
    }

    void conditionalConfirmText() {
        Button b = GameObject.Find("ConfirmSelectionButton").GetComponent<Button>();
        Text t = b.GetComponentInChildren<Text>();
        if (b.IsInteractable()) {
            t.text = HAS_MODE_TEXT;
        } else {
            t.text = NO_MODE_TEXT;
        }
    }

    /**
     * Modes are enabled as the user hits the high score requirements
     * Otherwise they are not interactable
     */
    void conditionalModes() {
        Button easyButton = GameObject.Find("EasyButton").GetComponent<Button>();
        Button normalButton = GameObject.Find("NormalButton").GetComponent<Button>();
        Button hardButton = GameObject.Find("HardButton").GetComponent<Button>();
        Button impossibleButton = GameObject.Find("ImpossibleButton").GetComponent<Button>();

        easyButton.interactable = EasyGame.hasMetRequirement();
        normalButton.interactable = NormalGame.hasMetRequirement();
        hardButton.interactable = HardGame.hasMetRequirement();
        impossibleButton.interactable = ImpossibleGame.hasMetRequirement();
    }

    /**
     * Debug use only
     * Allows testing of the high score requirements
     */
    public void deletePlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
