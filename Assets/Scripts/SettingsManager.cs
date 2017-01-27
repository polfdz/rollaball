using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	// Audio
	public AudioSource audioSrcSettings;
	public AudioClip buttonAudio;

	// Slider
	public Slider controlsSlider;
	public Slider difficultySlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickBackButton() {
		audioSrcSettings.PlayOneShot (buttonAudio);
		SceneManager.LoadScene("MainMenu");
		PlayerPrefs.SetFloat("controls", controlsSlider.value);
		PlayerPrefs.SetFloat("difficulty", controlsSlider.value);
	}
}
