using UnityEngine;
using System.Collections;

public class LightAndColor : MonoBehaviour {
	private float duration;
	private float t;
	private Color color1, color2;	// Use this for initialization

	public GameObject light1;
	private Light light1Comp;

	void Start () {
		duration = 1f;
		Light light1Comp = light1.GetComponent<Light>();
//		StartCoroutine(changeLight());

	}
	
	// Update is called once per frame
	void Update () {
		//changeMaterialColor ();
		changeLight1 ();
	}

	private void changeLight1() {
		light1.transform.Rotate (Vector3.right * Time.deltaTime * 100);
		}

//	IEnumerator changeLight() {
//		while (true) {
//			yield return new WaitForSeconds (1);
//
//		}
//	}

//	private void changeLight() {
////		light1Comp.color;
//	}

	private void changeMaterialColor() {
		t += Time.deltaTime / duration;
		color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		color2 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		Color color = Color.Lerp(color1, color2, t);

		GetComponent<Renderer> ().material.color = color;
	}
}
