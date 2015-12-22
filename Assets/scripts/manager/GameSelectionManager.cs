using UnityEngine;
using System.Collections;

public class GameSelectionManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
        setGameMode();
	}

    /**
     * Determines which Game mode script to add to the Master GameObject
     * Based on the selection made in the mode scene
     */
    void setGameMode() {
        switch (PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION)) {
            case EasyGame.ID:
                Debug.Log("EasyMode Loaded");
                gameObject.AddComponent<EasyGame>();
                break;
            case NormalGame.ID:
                Debug.Log("NormalMode Loaded");
                gameObject.AddComponent<NormalGame>();
                break;
            case HardGame.ID:
                Debug.Log("HardMode Loaded");
                gameObject.AddComponent<HardGame>();
                break;
            case ImpossibleGame.ID:
                Debug.Log("ImpossibleMode Loaded");
                gameObject.AddComponent<ImpossibleGame>();
                break;
            default:
                throw new UnityException(string.Format("Unknown difficulty ID: {0}.", PlayerPrefs.GetInt(ModeController.MODE_ID_LOCATION)));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
