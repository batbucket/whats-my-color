using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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

    // Use this for initialization
    void Start () {
        title = GameObject.Find("Title").GetComponent<Text>();
        colorTitle();
	}

    void colorTitle() {
        title.text = TITLE_PREFIX + "<color=#" + Tool.colorToHex(ColorWord.generateRandomColorWord().color) + ">" + TITLE_COLORED + "</color>" + TITLE_SUFFIX;
    }

    public void loadMode() {
        SceneManager.LoadScene("Mode");
    }
	
	// Update is called once per frame
	void Update () {
        Tool.bouncyEffect(GameObject.Find("Title"), MAX_SIZE_INCREASE, INCREASE_RATE);
	}
}
