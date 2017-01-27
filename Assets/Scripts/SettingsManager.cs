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
		showSliderCorrectValues ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickBackButton() {
		audioSrcSettings.PlayOneShot (buttonAudio);

		PlayerPrefs.SetFloat("controls", controlsSlider.value);
		PlayerPrefs.SetFloat("difficulty", difficultySlider.value);

		SceneManager.LoadScene("MainMenu");
	}

	private void showSliderCorrectValues() {
		controlsSlider.value = PlayerPrefs.GetFloat("controls", 0);
		difficultySlider.value = PlayerPrefs.GetFloat("difficulty", 0);
	}
}
