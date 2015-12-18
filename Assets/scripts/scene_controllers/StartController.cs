using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void loadMode() {
        SceneManager.LoadScene("Mode");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
