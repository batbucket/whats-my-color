using UnityEngine;
using System.Collections;

public class GameSelectionManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		int difficulty = PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION);
		if (difficulty == EasyGame.ID) {
			Debug.Log ("EasyMode Loaded");
			gameObject.AddComponent<EasyGame>();
		} else if (difficulty == NormalGame.ID) {
			Debug.Log ("NormalMode Loaded");
			gameObject.AddComponent<NormalGame>();
		} else if (difficulty == HardGame.ID) {
			Debug.Log ("HardMode Loaded");
			gameObject.AddComponent<HardGame>();
		} else if (difficulty == ImpossibleGame.ID) {
			Debug.Log ("ImpossibleMode Loaded");
			gameObject.AddComponent<ImpossibleGame>();
		} else {
			throw new UnityException("Unknown difficulty ID: " + difficulty + ".");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
