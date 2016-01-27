using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class ModeController : MonoBehaviour {

    public const string MODE_ID_LOCATION = "Mode_ID";

    public const string NO_MODE_TEXT = "NO SELECTION";
    public const string HAS_MODE_TEXT = "CONFIRM SELECTION";

    // Use this for initialization
    void Start() {
        Tool.unlockAchievement(Achievements.achievement_participation_medal); //'Unlock' Easy mode
    }

    // Update is called once per frame
    void Update() {
        ConditionalConfirmActivatedState();
        ConditionalConfirmText();
        ConditionalModes();
        DisplayDifficultyText();
        DisplayHiScores();
    }

    public void LoadGame() {
        SceneManager.LoadScene("GenericGame");
    }

    public void LoadStart() {
        SceneManager.LoadScene("Start");
    }

    public void EasyButtonClicked() {
        SetModeID(EasyGame.ID);
    }

    public void NormalButtonClicked() {
        SetModeID(NormalGame.ID);
    }

    public void HardButtonClicked() {
        SetModeID(HardGame.ID);
    }

    public void ImpossibleButtonClicked() {
        SetModeID(ImpossibleGame.ID);
    }

    void SetModeID(int ID) {
        PlayerPrefs.SetInt(MODE_ID_LOCATION, ID);
    }

    void SetDifficultyText(string s) {
        GameObject.Find("DifficultyDescription").GetComponent<Text>().text = s;
    }

    public static int GetModeID() {
        return PlayerPrefs.GetInt(MODE_ID_LOCATION);
    }

    /**
     * Extra text is added if there's a locked next mode
     */
    void DisplayDifficultyText() {
        string displayText = "";

        switch (GetModeID()) {
            case EasyGame.ID:
                displayText = EasyGame.MODE_DESCRIPTION;
                if (!NormalGame.HasMetRequirement()) {
                    displayText += "\n" + string.Format(NormalGame.UNLOCK_DESCRIPTION, NormalGame.SCORE_REQUIREMENT);
                }
                break;
            case NormalGame.ID:
                displayText = NormalGame.MODE_DESCRIPTION;
                if (!HardGame.HasMetRequirement()) {
                    displayText += "\n" + string.Format(HardGame.UNLOCK_DESCRIPTION, HardGame.SCORE_REQUIREMENT);
                }
                break;
            case HardGame.ID:
                displayText = HardGame.MODE_DESCRIPTION;
                if (!ImpossibleGame.HasMetRequirement()) {
                    displayText += "\n" + string.Format(ImpossibleGame.UNLOCK_DESCRIPTION, ImpossibleGame.SCORE_REQUIREMENT);
                }
                break;
            case ImpossibleGame.ID:
                displayText = ImpossibleGame.MODE_DESCRIPTION;
                break;
            default:
                displayText = "";
                break;
        }

        SetDifficultyText(displayText);
    }

    void DisplayHiScores() {
        GameObject.Find("EasyButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION);
        GameObject.Find("NormalButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION);
        GameObject.Find("HardButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION);
        GameObject.Find("ImpossibleButton").GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(ImpossibleGame.HISCORE_LOCATION) == 0 ? "" : "" + PlayerPrefs.GetInt(ImpossibleGame.HISCORE_LOCATION);
    }

    /**
     * Button is disabled if no mode has ever been selected, say, on the first program run
     */
    void ConditionalConfirmActivatedState() {
        GameObject.Find("ConfirmSelectionButton").GetComponent<Button>().interactable = GetModeID() != 0;
    }

    void ConditionalConfirmText() {
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
    void ConditionalModes() {
        Button easyButton = GameObject.Find("EasyButton").GetComponent<Button>();
        Button normalButton = GameObject.Find("NormalButton").GetComponent<Button>();
        Button hardButton = GameObject.Find("HardButton").GetComponent<Button>();
        Button impossibleButton = GameObject.Find("ImpossibleButton").GetComponent<Button>();

        easyButton.interactable = EasyGame.HasMetRequirement();
        normalButton.interactable = NormalGame.HasMetRequirement();
        hardButton.interactable = HardGame.HasMetRequirement();
        impossibleButton.interactable = ImpossibleGame.HasMetRequirement();
    }

    /**
     * Debug use only
     * Allows testing of the high score requirements
     */
    public void DeletePlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
