using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string gameSceneName = "MiniGame";
    public string infoSceneName = "InfoScene";

    public Text highscoreMenuText;
	public static string PreviousScene = "";

	// Audio
	public AudioSource audioSrcMenu;
	public AudioClip buttonAudio;

    // Use this for initialization
    void Start()
    {
        highscoreMenuText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //End application
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadGame()
    {
		audioSrcMenu.PlayOneShot (buttonAudio);
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadInfo() 
	{
		audioSrcMenu.PlayOneShot (buttonAudio);
		PlayerPrefs.SetString("prevScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(infoSceneName);
    }

}
