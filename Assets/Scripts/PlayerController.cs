using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed; //public apareixera a l'editor Unity
    public Text countText, winText;

    private Rigidbody rb; //private no apareixera a Unity
    private int count;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //obté el component rigidbody de la pilota
        count = 0;
        SetCountText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Before performing any physics calculation
    void FixedUpdate()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVeritcal = Input.GetAxis("Vertical");

            float x = moveHorizontal;
            float y = 0.0f;
            float z = moveVeritcal;

            Vector3 movement = new Vector3(x, y, z);
            rb.AddForce(movement * speed);

        }
        else {
            float moveHorizontal = Input.acceleration.x;
            float moveVertical = Input.acceleration.y;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed * 2);
        }


    }

    // Collisions
    void OnTriggerEnter(Collider other) {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive (false);
            count++;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 10) {
            winText.text = "You Win!";
        }
    }

}
