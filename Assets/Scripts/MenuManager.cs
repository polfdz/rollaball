using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string gameSceneName = "MiniGame";
    public string infoSceneName = "InfoScene";
    public UnityEngine.UI.Text highscoreMenuText;

    object Button;

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
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadInfo() {
        SceneManager.LoadScene(infoSceneName);
        PlayerPrefs.SetInt("prevlevel", Application.loadedLevel);
    }

}
