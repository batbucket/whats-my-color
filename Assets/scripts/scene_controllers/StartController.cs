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

    public const string AUTH_FAILURE = "Google Play sign in failed.";
    float timer;

    // Use this for initialization
    void Start() {
        title = GameObject.Find("Title").GetComponent<Text>();
        achievements = GameObject.Find("Achievements").GetComponent<Button>();
        leaderboards = GameObject.Find("Leaderboards").GetComponent<Button>();
        failureText = GameObject.Find("Failure_Text").GetComponent<Text>();
        relogin = GameObject.Find("Relogin").GetComponent<Button>();

        //Disable google play services buttons unless if sign in is successful
        achievements.interactable = false;
        leaderboards.interactable = false;
        ColorTitle();

        StartGooglePlay();
    }

    void ColorTitle() {
        title.text = TITLE_PREFIX + "<color=#" + Tool.colorToHex(ColorWord.CreateRandomColorWord().color) + ">" + TITLE_COLORED + "</color>" + TITLE_SUFFIX;
    }

    public void LoadMode() {
        SceneManager.LoadScene("Mode");
    }

    public void LoadAchievements() {
        Social.ShowAchievementsUI();
    }

    public void LoadLeaderboards() {
        Social.ShowLeaderboardUI();
    }

    public void StartGooglePlay() {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            achievements.interactable = success;
            leaderboards.interactable = success;
            relogin.gameObject.SetActive(!success);

            achievements.interactable = success;
            leaderboards.interactable = success;
            relogin.interactable = !success;
            failureText.text = success ? "" : AUTH_FAILURE;
            failureText.color = success ? Color.clear : Color.red;
            timer = 0;
            Debug.Log("Achievement success: " + success);
        });
    }

    // Update is called once per frame
    void Update() {
        Tool.bouncyEffect(GameObject.Find("Title"), MAX_SIZE_INCREASE, INCREASE_RATE);

        timer += Time.deltaTime;
        Color c = failureText.color;
        c = Color.Lerp(c, Color.clear, (timer - 2.5f) / 20);
        failureText.color = c;
    }
}
