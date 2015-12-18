using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour {

    public const string GAME_OVER_APPEND = " GAME OVER";
    public const string NEW_RECORD_TEXT = "Broke previous record of {0}!";
    public const string FIRST_RECORD_TEST = "On the board.";

    // Use this for initialization
    void Start () {
        Text gameOverTitle = GameObject.Find("GameOver").GetComponent<Text>();
        Text scoreTitle = GameObject.Find("Score").GetComponent<Text>();
        Text lossMessage = GameObject.Find("LossReason").GetComponent<Text>();
        Text score = GameObject.Find("Count").GetComponent<Text>();
        Text newRecordMessage = GameObject.Find("Record").GetComponent<Text>();
        newRecordMessage.text = "";

        gameOverTitle.text = PlayerPrefs.GetString(Game.GAME_MODE_LOCATION).ToUpper() + GAME_OVER_APPEND;
        lossMessage.text = PlayerPrefs.GetString(Game.LOSS_REASON_LOCATION);

        int scoreToSet = -1;
	    switch (PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION)) {
            case EasyGame.ID:
                gameOverTitle.text = "<color=green>" + gameOverTitle.text + "</color>";
                scoreTitle.text = "<color=green>" + scoreTitle.text + "</color>";
                scoreToSet = PlayerPrefs.GetInt(EasyGame.SCORE_LOCATION);
                updateScoreIfGreater(scoreToSet, EasyGame.HISCORE_LOCATION);
                break;
            case NormalGame.ID:
                gameOverTitle.text = "<color=orange>" + gameOverTitle.text + "</color>";
                scoreTitle.text = "<color=orange>" + scoreTitle.text + "</color>";
                scoreToSet = PlayerPrefs.GetInt(NormalGame.SCORE_LOCATION);
                updateScoreIfGreater(scoreToSet, NormalGame.HISCORE_LOCATION);
                break;
            case HardGame.ID:
                gameOverTitle.text = "<color=red>" + gameOverTitle.text + "</color>";
                scoreTitle.text = "<color=red>" + scoreTitle.text + "</color>";
                scoreToSet = PlayerPrefs.GetInt(HardGame.SCORE_LOCATION);
                updateScoreIfGreater(scoreToSet, HardGame.HISCORE_LOCATION);
                break;
            case ImpossibleGame.ID:
                gameOverTitle.text = "<color=blue>" + gameOverTitle.text + "</color>";
                scoreTitle.text = "<color=blue>" + scoreTitle.text + "</color>";
                scoreToSet = PlayerPrefs.GetInt(ImpossibleGame.SCORE_LOCATION);
                updateScoreIfGreater(scoreToSet, ImpossibleGame.HISCORE_LOCATION);
                break;
            default:
                throw new UnityException("Unknown Mode ID: " + PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION));
        }
        score.text = "" + scoreToSet;
	}

    void updateScoreIfGreater(int score, string hiScoreLoc) {
        if (score > PlayerPrefs.GetInt(hiScoreLoc)) {
            Debug.Log("Score was greater. Replacing...");
            if (PlayerPrefs.GetInt(hiScoreLoc) == 0) {
                GameObject.Find("Record").GetComponent<Text>().text = FIRST_RECORD_TEST;
            } else {
                GameObject.Find("Record").GetComponent<Text>().text = string.Format(NEW_RECORD_TEXT, PlayerPrefs.GetInt(hiScoreLoc));
            }
            PlayerPrefs.SetInt(hiScoreLoc, score);
        }
    }

    public void loadMode() {
        SceneManager.LoadScene("Mode");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
