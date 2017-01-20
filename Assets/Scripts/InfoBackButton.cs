using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoBackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goBackToPrevLevel() {
		string prevScene = PlayerPrefs.GetString ("prevScene", "1MainMenu");
		Debug.Log ("clickBack prevScene: " + prevScene);
		SceneManager.LoadScene (prevScene);
	}
}
