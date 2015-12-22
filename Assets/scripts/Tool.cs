using UnityEngine;
using System.Collections;
using System;

public class Tool : MonoBehaviour {

    public static T GetRandomEnum<T>() {
		Array values = Enum.GetValues(typeof(T));
		return  (T) values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}

	/**
	 * Converts an AudioClip to an AudioSource
	 */
	public static AudioSource clipToSource(AudioClip clip) {
		AudioSource source = new AudioSource();
		source.clip = clip;
		return source;
	}

	public static bool randomBoolean() {
		return (UnityEngine.Random.Range(0, 1) == 1);
	}

    public static string colorToHex(Color32 color) {
        return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
    }

    public static string randomizeColors(string s) {
        string res = "";
        for (int i = 0; i < s.Length; i++) {
            string letter = s[i].ToString();
            res += "<color=#" + Tool.colorToHex(ColorWord.generateRandomColorWord().color) + ">" + letter + "</color>";
        }
        return res;
    }

    /**
     * Makes a GameObject with a transform bouncy
     * @param maxSize how much larger (and smaller) than 1 (normal scale) you want it to get
     * @param increaseRate the rate at which the transform will grow and shrink
     */
    public static void bouncyEffect(GameObject g, float maxSize, float increaseRate) {
        Vector4 cwScale = g.GetComponent<Transform>().localScale;
        float timeValue = Time.deltaTime * increaseRate;

        //We store the status of whether the Object should
        //increase or decrease in size in the GameObject's z
        //value as it is of no use in this game
        if (cwScale.z != 0) {

            //Grow scale
            cwScale.x += timeValue;
            cwScale.y += timeValue;

            //If past the greater limit, start shrinking
            if (cwScale.x > 1 + maxSize) {
                cwScale.z = 0;
            }
        } else {

            //Shrink scale
            cwScale.x -= timeValue;
            cwScale.y -= timeValue;

            //If past the lower limit, start growing
            if (cwScale.x < 1 - maxSize) {
                cwScale.z = 1;
            }
        }

        g.GetComponent<Transform>().localScale = cwScale;
    }

}
