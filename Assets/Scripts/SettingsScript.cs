using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
	public int CONTROLS_ACCEL = 0;
	public  int CONTROLS_JOYSTICK = 1;

	// Audio
	public AudioSource audioSrcSettings;
	public AudioClip buttonAudio;

	// Slider
	public Slider controlsSlider;

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

	}
}
