using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CnControls;

public class PlayerController : MonoBehaviour
{
    public GameObject ramp1, ramp2, ramp3, ramp4, ramp5, ramp6, ramp7, ramp8;
    public GameObject joystick;
    private int level = 1; //level of the current platform
    private int currentPlatform = 1; //current platform of the player. To generate random objects
    private String currentGround = "Ground1";
    //Objects
    private Rigidbody rb; //the ball -- private not shown in Unity editor
    public Text countText, winText, timerText, highscoreText; //if public can be access in Unity editor
	public Button restartButton;
	public Button menuButton;

    //Other classes for attributes access 
    public RandomObjects randomObjects;

    // Audio
	public AudioSource audioSrcMain;
	public AudioSource audioSrcEffects;
	public AudioSource audioSrcWind;

	public AudioClip gameMusic;
	public AudioClip gameOverMusic;
	public AudioClip timeDangerSound;
	public AudioClip coinObjectSound;
	public AudioClip timeObjectSound;
	public AudioClip windObjectSound;
	public AudioClip plusObjectSound;
	public AudioClip bombExplosionSound;
	public AudioClip windSound;
	public AudioClip rampSound;
	public AudioClip wallsSound;
	public AudioClip gameOverSound;

	//Timers
    private float colorTimer = 50.0f;

    //Counters
    private int count;

	// GameOver Flag
	private bool isGameOver;
    //Ball speed
    public float speed, windSpeed; //public will be shown in Unity editor
    public float boxSpeed;
    public bool joystickMode = false;
    Vector3 movement;
    // Use this for initialization
    void Start()
	{
        //ramp1.SetActive(false);
        //start game
		isGameOver = false;
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>(); //gets ball components
        count = 0;
        SetCountText();
        winText.text = "";
        timerText.text = "";
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);

		// Add music track
		audioSrcMain.clip = gameMusic;
		audioSrcMain.Play();

		setControls ();
		if (joystickMode && SystemInfo.deviceType != DeviceType.Desktop)
		{
			joystick.gameObject.SetActive(true);
		}
		else {
			joystick.gameObject.SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (!isGameOver) {
			//check if ball falled
			if (rb.position.y < -10) {
				GameOver(0);
			}

			//color timer behavior
			updateColorTimer();

			//Wind force if is active
			applyWind(2);

            //Platforms and ramps display and behavior
            displayRamps();

        }


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

            movement = new Vector3(x, y, z);
            rb.AddForce(movement * speed);

        }else { //MOBILE DEVICE
            float moveHorizontal;
            float moveVertical;
            if (joystickMode)
            {
                moveHorizontal = CnInputManager.GetAxis("Horizontal");
                moveVertical = CnInputManager.GetAxis("Vertical");
                movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed);
            }else {
                moveHorizontal = Input.acceleration.x * 1.2f;
                moveVertical  = Input.acceleration.y * 1.8f;
                movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed * 3);
            }
        }
    }

    // Collisions
    void OnTriggerEnter(Collider collider) {
        //Destroy(other.gameObject);

        switch (collider.gameObject.tag) {
            case "Ground":
                if(currentGround!= collider.gameObject.name) {
                    setCurrentPlatform(collider.gameObject.name);
                }
                break;

            case "Pick Up":
                randomObjects.currentPickUp1--;
                Destroy(collider.gameObject);
                count++;
                SetCountText();
				audioSrcEffects.PlayOneShot(coinObjectSound);
                break;
            case "Pick Up2":
                randomObjects.currentPickUp2--;
                Destroy(collider.gameObject);
                count += 5;
                SetCountText();
                audioSrcEffects.PlayOneShot(coinObjectSound);
                break;
            case "Pick Up3":
                randomObjects.currentPickUp3--;
                Destroy(collider.gameObject);
                count += 10;
                SetCountText();
                audioSrcEffects.PlayOneShot(coinObjectSound);
                break;
            case "Pick Up4":
                randomObjects.currentPickUp4--;
                Destroy(collider.gameObject);
                count += 20;
                SetCountText();
                audioSrcEffects.PlayOneShot(coinObjectSound);
                break;
		case "Bomb":
			switch (randomObjects.currentPlatform) {
			case 1:
				randomObjects.currentBombs1--;
				count -= 10;
				break;
			case 2:
				randomObjects.currentBombs1--;
				count -= 20;
				break;
			case 3:
				randomObjects.currentBombs1--;
				count -= 30;
				break;
			case 4:
				randomObjects.currentBombs1--;
				count -= 40;
				break;
			}				
				showExplosion (collider.gameObject);
                Destroy(collider.gameObject);
                SetCountText();
                audioSrcEffects.PlayOneShot(bombExplosionSound);  ///SOUND BOOMB!
                break;
            case "Wall":
                //Move wall
				collider.transform.Translate (0.0f, -0.3f, 0.0f);
				audioSrcEffects.PlayOneShot (wallsSound);
                break;
            case "Box Time":
                switch (randomObjects.currentPlatform) {
                    case 1:
                        randomObjects.currentTimeBoxes--;
                        colorTimer += 1;
                        break;
                    case 2:
                        randomObjects.currentTimeBoxes2--;
                        colorTimer += 3;
                        break;
                    case 3:
                        randomObjects.currentTimeBoxes3--;
                        colorTimer += 4;
                        break;
                    case 4:
                        randomObjects.currentTimeBoxes4--;
                        colorTimer += 5;
                        break;                
                }
                Destroy(collider.gameObject);
				audioSrcEffects.PlayOneShot(timeObjectSound, 0.3f);
                break;
            case "Box Wind":
                Destroy(collider.gameObject);
                randomObjects.desactivateWind();
				audioSrcEffects.PlayOneShot(windObjectSound, 1.0f);
                break;
            case "Box Ground":
                Destroy(collider.gameObject);
                randomObjects.resizeGround(0);
				audioSrcEffects.PlayOneShot(plusObjectSound);            
			break;
			case "Ramp":
				Debug.Log ("RAMP");
                rb.AddForce (movement * speed * 50);
				// play ramp sound when ball is y > 1 to ensure it is not just colliding with ramp
				if (rb.position.y > 1) {
					audioSrcEffects.PlayOneShot(rampSound);
				}
                break;
        }
    }

    /*
     * Coins counting text
     * 
     */
    void SetCountText() {
        countText.text = "Score: " + count.ToString();
       /* if (count >= 10) {
            winText.text = "You Win!";
        }*/
    }

    /*
     * Color Timer behavior
     * 
     */

    void updateColorTimer() {
        colorTimer -= Time.deltaTime;
        timerText.text = "Time:\n" + colorTimer.ToString("0.#");
        if (colorTimer < 11.0f)
        {
            timerText.color = Color.red;
        }else {
            timerText.color = Color.green;
        }
        if (colorTimer < 0)
        {
            GameOver(1);
        }

		// Play alarm sound if less than five seconds
		if (colorTimer < 5.0f) {
			if (!audioSrcEffects.isPlaying) {				
				audioSrcEffects.PlayOneShot (timeDangerSound);
			}
		}
    }
    //current platform number of items to be generated
    void setCurrentPlatform(String plat) {
        switch (plat) {
            case "Ground1":
                makeLevel1();
                currentGround = "Ground1";
                break;
            case "Ground2":
                makeLevel2();
                currentGround = "Ground2";
                break;
            case "Ground3":
                makeLevel3();
                currentGround = "Ground3";
                break;
            case "Ground4":
                makeLevel4();
                currentGround = "Ground4";
                break;
        }
        //always on platform change
        randomObjects.desactivateWind();

    }
    void makeLevel1()
    {
        currentPlatform = 1;
        randomObjects.currentWindBoxes = 0;
    }
    void makeLevel2()
    {
        currentPlatform = 2;
        randomObjects.currentWindBoxes = 0;
    }
    void makeLevel3()
    {
        currentPlatform = 3;
        randomObjects.currentWindBoxes = 0;
    }
    void makeLevel4()
    {
        currentPlatform = 4;
        randomObjects.currentWindBoxes = 0;
    }

    public int getCurrentPlatform() {
        return currentPlatform;
    }
    //Platforms and Ramps behavior
    void displayRamps()
    {

        level = checkLevel();

        switch (level)
        {
            case 2:
                ramp1.SetActive(true);
                ramp2.SetActive(true);
                break;
            case 3:
                ramp3.SetActive(true);
                ramp4.SetActive(true);
                break;
            case 4:
                ramp5.SetActive(true);
                ramp6.SetActive(true);
                ramp7.SetActive(true);
                //ramp8.SetActive(true);
                break;
        }

    }
    //gets the current level for displaying ramps depending on counting coins
    public int checkLevel()
    {
        if (count > 15)
        {
            return level = 4;
        }
        else if (count > 5)
        {
            return level = 3;

        }
        else if (count > 1)
        {
            return level = 2;
        }
        return level;
    }

    /*
     * Adds force to the ball 
     * factor - determines strength of the force
     * 
     */
    void applyWind(int factor) {
        GameObject wind = randomObjects.getActiveWind();
        float windForce = 0.2f;
        Vector3 movement;
		if (wind != null) {
			playWindSound ();
			switch (wind.name) {
			case "WindZoneW":
				movement = new Vector3 (windForce, 0.0f, 0.0f);
				rb.AddForce (movement * windSpeed * factor);
				break;
			case "WindZoneE":
				movement = new Vector3 (-windForce, 0.0f, 0.0f);
				rb.AddForce (movement * windSpeed * factor);
				break;
			case "WindZoneN":
				movement = new Vector3 (0.0f, 0.0f, -windForce);
				rb.AddForce (movement * windSpeed * factor);
				break;
			case "WindZoneS":
				movement = new Vector3 (0.0f, 0.0f, windForce);
				rb.AddForce (movement * windSpeed * factor);
				break;

			}
		} else {
			audioSrcWind.Stop ();
		}

    }

	void playWindSound() {
		if (!audioSrcWind.isPlaying) {
			audioSrcWind.PlayOneShot(windSound);		
		}
	}

	void showExplosion(GameObject collider) {
		Vector3 position = collider.transform.position;
		GameObject explosion = Instantiate(Resources.Load("porglezomppyroclastics/pyroclastic puff"), position, Quaternion.identity) as GameObject; 
		ExplosionMat e = new ExplosionMat ();
		e._alpha = 0;
	}
    /*
     * Ends the game depending on type of ending
     * 0 ball out of the terrain
     * 1 Timer expired
     * 2
     */
    void GameOver(int type)
    {		
		isGameOver = true;

        switch (type)
        {
            case 0:
                //Ball out the floor
                winText.text = "You lost...";
                break;
            case 1:
                //ColorTime expired
                winText.text = "Time is over...";
                timerText.text = "--";
                break;
                
        }
        StoreHighscore(count);

		// Show restart button
		restartButton.gameObject.SetActive (true);
		menuButton.gameObject.SetActive (true);

		// Audio stuff
		audioSrcMain.clip = gameOverMusic;
		audioSrcMain.Play ();
		audioSrcWind.Stop ();
		audioSrcEffects.PlayOneShot(gameOverSound);
        
		//Stop app
        Time.timeScale = 0;
    }

    void StoreHighscore(int newHighscore) {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore) {
            PlayerPrefs.SetInt("highscore", newHighscore);
        }
    }

	void setControls() {
		//show or hide joystick
		float joystickPrefs = PlayerPrefs.GetFloat("controls", 0);
		print (joystickPrefs);
		if (joystickPrefs == 0.0f) {
			joystickMode = false;
		} else {
			joystickMode = true;
		}				
		print (joystickMode);
	}
}
