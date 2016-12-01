using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            offset = transform.position - player.transform.position;
        }
        else {
  
            offset = transform.position - player.transform.position;
        }

    }
	
	// Update is called once per frame
	void Update () {
	}

    // Better for camera has goes after update
    void LateUpdate() {
        transform.position = player.transform.position + 2*offset;
    }
}
