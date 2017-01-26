using UnityEngine;
using System.Collections;

public class LightAndColor : MonoBehaviour {
	private float DURATION = 1f;
	private float TOGGLE_LIGHTS_TIME = 0.5f;

	private float t;
	private Color color1, color2;	// Use this for initialization

	public GameObject lightUnder;
	public GameObject player;
	public GameObject pointLightFollows;
	// Platform lights
	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	public GameObject light4;

	private PlayerController playerScript;

	private bool alarmLights = false;
	//private Light light1Comp;

	void Start () {
//		Light light1Comp = lightUnder.GetComponent<Light>();
		playerScript = player.GetComponent<PlayerController> ();

		StartCoroutine (changePlatformColors());
	}
	
	// Update is called once per frame
	void Update () {
		//changeMaterialColor ();
		//	pointLightFollowPlayer ();
		rotateLightUnder ();
		alarmLights = playerScript.getColorTimer () < 5.0f ? true : false;
	}

	private IEnumerator changePlatformColors() {
		
		while(true) {
			light1.SetActive (!light1.activeSelf);
			light2.SetActive (!light2.activeSelf);
			light3.SetActive (!light3.activeSelf);
			light4.SetActive (!light4.activeSelf);

			if (alarmLights) {
				setPlatformLightsAlarm ();
			} else {
				setPlatformLightsNormal();
			}

			yield return new WaitForSeconds(TOGGLE_LIGHTS_TIME);
		}
	}

	private void setPlatformLightsAlarm() {
		TOGGLE_LIGHTS_TIME = 0.2f;
		light1.GetComponent<Light> ().color = Color.red;
		light2.GetComponent<Light> ().color = Color.red;
		light3.GetComponent<Light> ().color = Color.red;
		light4.GetComponent<Light> ().color = Color.red;

	}

	private void setPlatformLightsNormal() {
		TOGGLE_LIGHTS_TIME = 0.8f;
		Color color1;
		Color color2;
		Color color3;
		Color color4;

		if (ColorUtility.TryParseHtmlString ("#2FB9DDFF", out color1)) {
			light1.GetComponent<Light> ().color = color1;	
		}

		if (ColorUtility.TryParseHtmlString ("#3CDD2EFF", out color2)) {
			light2.GetComponent<Light> ().color = color2;	
		}

		if (ColorUtility.TryParseHtmlString ("#FF8300FF", out color3)) {
			light3.GetComponent<Light> ().color = color3;
		}

		if (ColorUtility.TryParseHtmlString ("#B100FFFF", out color4)) {
			light4.GetComponent<Light> ().color = color4;
		}

	}


	private void rotateLightUnder() {
		lightUnder.transform.Rotate (Vector3.right * Time.deltaTime * 100);
	}

	// NEEDS WORK
	private void pointLightFollowPlayer () {
		pointLightFollows.transform.Translate (player.transform.position.x, 0, 0);
	}

	private void changeMaterialColor() {
		t += Time.deltaTime / DURATION;
		color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		color2 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		Color color = Color.Lerp(color1, color2, t);

		player.GetComponent<Renderer> ().material.color = color;
	}
}
