﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoBackButton : MonoBehaviour {

	public AudioSource audioSrcInfo;
	public AudioClip buttonAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goBackToPrevLevel() {
		print ("test");
		audioSrcInfo.PlayOneShot (buttonAudio);

		string prevScene = PlayerPrefs.GetString ("prevScene", "MainMenu");
		Debug.Log ("clickBack prevScene: " + prevScene);
		SceneManager.LoadScene (prevScene);
	}
}
