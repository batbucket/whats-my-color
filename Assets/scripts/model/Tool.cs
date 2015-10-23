using UnityEngine;
using System.Collections;
using System;

public class Tool : MonoBehaviour {

	public static T GetRandomEnum<T>()
	{
		Array values = Enum.GetValues(typeof(T));
		return  (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
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
}
