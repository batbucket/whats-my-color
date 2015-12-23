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
    Text scoreText;
    Text newRecordMessage;
    Button returnButton;

    int score;

    // Use this for initialization
    void Start () {
        currentDelay = RETURN_DELAY;

        gameOverTitle = GameObject.Find("GameOver").GetComponent<Text>();
        scoreTitle = GameObject.Find("Score").GetComponent<Text>();
        lossMessage = GameObject.Find("LossReason").GetComponent<Text>();
        scoreText = GameObject.Find("Count").GetComponent<Text>();
        newRecordMessage = GameObject.Find("Record").GetComponent<Text>();
        returnButton = GameObject.Find("Return").GetComponent<Button>();

        clearNewRecordText();
        disableReturnButton();
        setGameOverTitle();
        setAndUpdateScores();
        setLossReason();

        achievementCheck();
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
        scoreText.text = "" + scoreToSet;
        this.score = scoreToSet;
    }

    //Also includes leaderboards
    void achievementCheck() {
        int mode = PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION);

        //Mode specific achievements
        switch (mode) {
            case EasyGame.ID:
                if (score >= NormalGame.SCORE_REQUIREMENT) { Tool.unlockAchievement(Achievements.achievement_time_drops_in_decay); } //Unlock Normal mode
                break;
            case NormalGame.ID:
                Social.ReportScore(score, Achievements.leaderboard_normal_mode, (bool success) => {
                    Debug.Log("Post to Normal leaderboard status: " + success);
                });
                if (score >= HardGame.SCORE_REQUIREMENT) { Tool.unlockAchievement(Achievements.achievement_thanks_fisheryates); } //Unlock Hard mode
                if (score >= Achievements.BRONZE_SCORE) { Tool.unlockAchievement(Achievements.achievement_normal_pencil); } //Normal bronze
                if (score >= Achievements.SILVER_SCORE) { Tool.unlockAchievement(Achievements.achievement_normal_brush); } //Normal silver
                if (score >= Achievements.GOLD_SCORE) { Tool.unlockAchievement(Achievements.achievement_normal_bucket); } //Normal gold
                break;
            case HardGame.ID:
                Social.ReportScore(score, Achievements.leaderboard_hard_mode, (bool success) => {
                    Debug.Log("Post to Hard leaderboard status: " + success);
                });
                if (score >= ImpossibleGame.SCORE_REQUIREMENT) { Tool.unlockAchievement(Achievements.achievement_your_reward_more_questions); } //Unlock Impossible mode
                if (score >= Achievements.BRONZE_SCORE) { Tool.unlockAchievement(Achievements.achievement_hard_pencil); } //Hard bronze
                if (score >= Achievements.SILVER_SCORE) { Tool.unlockAchievement(Achievements.achievement_hard_brush); } //Hard silver
                if (score >= Achievements.GOLD_SCORE) { Tool.unlockAchievement(Achievements.achievement_hard_bucket); } //Hard gold
                break;

            case ImpossibleGame.ID:
                Social.ReportScore(score, Achievements.leaderboard_impossible_mode, (bool success) => {
                    Debug.Log("Post to Impossible leaderboard status: " + success);
                });
                if (score >= Achievements.BRONZE_SCORE) { Tool.unlockAchievement(Achievements.achievement_impossible_pencil); } //Impossible bronze
                if (score >= Achievements.SILVER_SCORE) { Tool.unlockAchievement(Achievements.achievement_impossible_brush); } //Impossible silver
                if (score >= Achievements.GOLD_SCORE) { Tool.unlockAchievement(Achievements.achievement_impossible_bucket); } //Impossible gold
                break;

            default:
                throw new UnityException("Unknown ID: " + PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION));
        }

        /**
         * General achievements
         */
        int easyHi = PlayerPrefs.GetInt(EasyGame.HISCORE_LOCATION);
        int normalHi = PlayerPrefs.GetInt(NormalGame.HISCORE_LOCATION);
        int hardHi = PlayerPrefs.GetInt(HardGame.HISCORE_LOCATION);
        int impossibleHi = PlayerPrefs.GetInt(ImpossibleGame.HISCORE_LOCATION);

        //All bronze unlock
        if (easyHi >= Achievements.BRONZE_SCORE 
            && normalHi >= Achievements.BRONZE_SCORE 
            && hardHi >= Achievements.BRONZE_SCORE 
            && impossibleHi >= Achievements.BRONZE_SCORE) {
            Tool.unlockAchievement(Achievements.achievement_palette_novice);
        }

        //All silver unlock
        if (easyHi >= Achievements.SILVER_SCORE
            && normalHi >= Achievements.SILVER_SCORE
            && hardHi >= Achievements.SILVER_SCORE
            && impossibleHi >= Achievements.SILVER_SCORE) {
            Tool.unlockAchievement(Achievements.achievement_palette_adept);
        }

        //All gold unlock
        if (easyHi >= Achievements.GOLD_SCORE
            && normalHi >= Achievements.GOLD_SCORE
            && hardHi >= Achievements.GOLD_SCORE
            && impossibleHi >= Achievements.GOLD_SCORE) {
            Tool.unlockAchievement(Achievements.achievement_palette_master);
        }
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
