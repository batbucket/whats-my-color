using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class StartController : MonoBehaviour {

    Text title;
    Button achievements;
    Button leaderboards;
    Button relogin;
    Text failureText;

    public const string TITLE_PREFIX = "What's my ";
    public const string TITLE_COLORED = "COLOR";
    public const string TITLE_SUFFIX = "?";

    /**
     * These fields are used for the text bounce effect
     */
    public const float MAX_SIZE_INCREASE = .05f;
    public const float INCREASE_RATE = .2f;

    public const string AUTH_FAILURE = "Google Play login failed. Achievements and Leaderboards will not work.\nPlease attempt to sign in again.";

    // Use this for initialization
    void Start () {
        title = GameObject.Find("Title").GetComponent<Text>();
        achievements = GameObject.Find("Achievements").GetComponent<Button>();
        leaderboards = GameObject.Find("Leaderboards").GetComponent<Button>();
        failureText = GameObject.Find("Failure_Text").GetComponent<Text>();
        relogin = GameObject.Find("Relogin").GetComponent<Button>();

        //Disable google play services buttons unless if sign in is successful
        achievements.gameObject.SetActive(false);
        leaderboards.gameObject.SetActive(false);
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
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            achievements.gameObject.SetActive(success);
            leaderboards.gameObject.SetActive(success);
            relogin.gameObject.SetActive(!success);

            achievements.interactable = success;
            leaderboards.interactable = success;
            relogin.interactable = !success;
            failureText.text = success ? "" : AUTH_FAILURE;
            Debug.Log("Achievement success: " + success);
        });
    }

	// Update is called once per frame
	void Update () {
        Tool.bouncyEffect(GameObject.Find("Title"), MAX_SIZE_INCREASE, INCREASE_RATE);
	}
}
