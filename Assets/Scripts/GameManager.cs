using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public string menuSceneName = "MainMenu";
	public string infoSceneName = "InfoScene";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Esc pressed go back to menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }

	public void helpButton() {
		PlayerPrefs.SetString("prevScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene(infoSceneName);
	}
}
