using UnityEngine;
using System.Collections;

public class TimeEventsController : MonoBehaviour {

    public WindZone windE, windW, windS, windN;
    private float time;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        //windS.gameObject.setActive(true);

        randomWind();
	}

    /*
    * Generates wind randomly
    * 
    */
    void randomWind()
    {
       /* windE.gameObject.activeSelf;
        windW.gameObject.activeSelf;
        windN.gameObject.activeSelf;
        windS.gameObject.activeSelf;*/

        /*if (!windE.gameObject.activeSelf)
        {
            int rand = Random.Range(0, 10000);
            if (rand <= 10)
            {
                Debug.Log("WIND ON");
                windE.gameObject.SetActive(true);
               // randomObjects.currentWindBoxes--; //ask for wind boxes
            }
        }*/
    }
}
