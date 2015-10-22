using UnityEngine;
using System.Collections;
using System;

public class Tool : MonoBehaviour {

	public static T GetRandomEnum<T>()
	{
		Array values = Enum.GetValues(typeof(T));
		return  (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}
}
