using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour {

    public const string GAME_OVER_APPEND = " GAME OVER"; //___ GAME OVER
    public const string NEW_RECORD_TEXT = "Broke previous record of {0}!";
    public const string FIRST_RECORD_TEST = "On the board."; //Message when hiscoring more than 0 in a mode

    public const string WAITING_TEXT = "CAN RETURN IN {0:F1}"; //Text on disabled return button
    public const string DONE_TEXT = "RETURN TO MODE SELECT";
    public const float RETURN_DELAY = .5f; //Time until can return to mode select
    public float currentDelay; //Current delay on time until can return

    Text gameOverTitle;
    Text scoreTitle;
    Text lossMessage;
    Text score;
    Text newRecordMessage;
    Button returnButton;

    // Use this for initialization
    void Start () {
        currentDelay = RETURN_DELAY;

        gameOverTitle = GameObject.Find("GameOver").GetComponent<Text>();
        scoreTitle = GameObject.Find("Score").GetComponent<Text>();
        lossMessage = GameObject.Find("LossReason").GetComponent<Text>();
        score = GameObject.Find("Count").GetComponent<Text>();
        newRecordMessage = GameObject.Find("Record").GetComponent<Text>();
        returnButton = GameObject.Find("Return").GetComponent<Button>();

        clearNewRecordText();
        disableReturnButton();
        setGameOverTitle();
        setAndUpdateScores();
        setLossReason();
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

    void disableReturnButton() {
        returnButton.interactable = false;
    }

    /**
     * Delay the return button's enable state so that
     * a player can't accidently skip over the results screen
     * in the case that they try to tap on a lower answer button
     * and the time runs out before their finger makes the connection
     */
    void delayReturnButton() {
        if ((currentDelay -= Time.deltaTime) < 0) {
            returnButton.interactable = true;
            returnButton.GetComponentInChildren<Text>().text = DONE_TEXT;
        } else {
            returnButton.GetComponentInChildren<Text>().text = string.Format(WAITING_TEXT, currentDelay);
        }
    }

    /**
     * Sets scores and checks if a hiscore is broken or not
     */
    void setAndUpdateScores() {
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

    void clearNewRecordText() {
        newRecordMessage.text = "";
    }

    void setGameOverTitle() {
        gameOverTitle.text = PlayerPrefs.GetString(Game.GAME_MODE_LOCATION).ToUpper() + GAME_OVER_APPEND;
    }

    void setLossReason() {
        lossMessage.text = PlayerPrefs.GetString(Game.LOSS_REASON_LOCATION);
    }
	
	// Update is called once per frame
	void Update () {
        delayReturnButton();
	}
}
