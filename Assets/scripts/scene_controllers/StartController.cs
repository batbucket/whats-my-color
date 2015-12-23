using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class StartController : MonoBehaviour {

    Text title;
    public const string TITLE_PREFIX = "What's my ";
    public const string TITLE_COLORED = "COLOR";
    public const string TITLE_SUFFIX = "?";

    /**
     * These fields are used for the text bounce effect
     */
    public const float MAX_SIZE_INCREASE = .05f;
    public const float INCREASE_RATE = .2f;

    public const string AUTH_FAILURE = "Google Play login failed. Related services will not work.";

    // Use this for initialization
    void Start () {
        title = GameObject.Find("Title").GetComponent<Text>();
        colorTitle();

        startGooglePlay();
	}

    void colorTitle() {
        title.text = TITLE_PREFIX + "<color=#" + Tool.colorToHex(ColorWord.generateRandomColorWord().color) + ">" + TITLE_COLORED + "</color>" + TITLE_SUFFIX;
    }

    public void loadMode() {
        SceneManager.LoadScene("Mode");
    }

    public void loadAchievements() {
        Social.ShowAchievementsUI();
    }

    public void loadLeaderboards() {
        Social.ShowLeaderboardUI();
    }

    public void startGooglePlay() {
        Button achievements = GameObject.Find("Achievements").GetComponent<Button>();
        Button leaderboards = GameObject.Find("Leaderboards").GetComponent<Button>();
        Text failureText = GameObject.Find("Failure_Text").GetComponent<Text>();

        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            achievements.interactable = success;
            leaderboards.interactable = success;
            failureText.text = success ? "" : AUTH_FAILURE;
            Debug.Log("Achievement success: " + success);
        });
    }
	
	// Update is called once per frame
	void Update () {
        Tool.bouncyEffect(GameObject.Find("Title"), MAX_SIZE_INCREASE, INCREASE_RATE);
	}
}
