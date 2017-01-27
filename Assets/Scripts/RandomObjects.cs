using UnityEngine;
using System.Collections;

public class RandomObjects : MonoBehaviour
{
    //Game Objects
	public GameObject player;
    private GameObject ground; //terrain
    public GameObject ground1, ground2, ground3, ground4;

    public GameObject pickUpToPlace, pickUp2, pickUp3, pickUp4, bomb; // GameObject to place
    public GameObject boxTimeToPlace; // GameObject to place
    public GameObject boxWindToPlace;
    public GameObject groundBoxToPlace;
    public PlayerController playerController;
    public WindZone windW, windE, windN, windS;

    //Couts of game objects "boxes"
    public int numberOfPickUp1, numberOfPickUp2, numberOfPickUp3, numberOfPickUp4, numberOfBombs; // number of objects to place
    public int numberOfTimeBoxes, numberOfTimeBoxes2, numberOfTimeBoxes3, numberOfTimeBoxes4; // number of objects to place
    public int numberOfBombs1, numberOfBombs2, numberOfBombs3, numberOfBombs4;

	public int currentPickUp1, currentPickUp2, currentPickUp3, currentPickUp4; // number of placed objects
	public int currentTimeBoxes, currentTimeBoxes2, currentTimeBoxes3, currentTimeBoxes4; // number of placed objects
	public int currentBombs1, currentBombs2, currentBombs3, currentBombs4;

    public int numberOfWindBoxes; // number of objects to place
    public int currentWindBoxes; // number of placed objects

    public bool levelChanged = false;
    public int currentPlatform = 1;

    //Ground size parameters
    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z
    private float scaleX = 2.0f;
    private float scaleZ = 2.0f;

	// Difficulty
	private int difficulty;
    void Start()
    {

        //Ground size parametes
        ground = getCurrentPlatform();
        updateGroundParams();

        //Desactivate windZones
        desactivateWind();

		// Set difficulty, number of objects on each platform
		setObjectsForDifficulty (playerController.getDifficulty ());
    }
    void FixedUpdate() {
        
    }
    // Update is called once per frame
    void Update()
    {

       /* if (levelChanged) {
            StartCoroutine(waitSeconds());

        }*/
        ground = getCurrentPlatform();
        updateGroundParams();

        //make ground small randomly
        resizeGround(1);
        //generate random objects
        generateRandomObjects();

        //generate random winds
        generateRandomWind();


    }

    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(2.0f);
        levelChanged = false;
    }

    GameObject getCurrentPlatform() {
        currentPlatform = playerController.getCurrentPlatform();
        switch (currentPlatform)
        {
            case 1:
                return ground1;
            case 2:
                return ground2;
            case 3:
                return ground3;
            case 4:
                return ground4;
        }
        return ground1;
    }
 
    //generate Random Objects for each platform
    void generateRandomObjects()
    {
        switch (playerController.getCurrentPlatform())
        {

            case 1:
                generateRandomObjects1();
                break;
            case 2:
                generateRandomObjects2();
                break;
            case 3:
                generateRandomObjects3();
                break;
            case 4:
                generateRandomObjects4();
                break;
        }

    }

    void generateRandomObjects1() {
        // create new gameObject on random position
        if (currentPickUp1 < numberOfPickUp1)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(pickUpToPlace, randoms);
            currentPickUp1 += 1;
        }
        if (currentTimeBoxes < numberOfTimeBoxes)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(boxTimeToPlace, randoms);
            currentTimeBoxes++;
        }

    }

    void generateRandomObjects2()
    {
        // create new gameObject on random position
        if (currentPickUp2 < numberOfPickUp2)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(pickUp2, randoms);
            currentPickUp2 += 1;
        }
        if (currentBombs2 < numberOfBombs2)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(bomb, randoms);
            currentBombs2 += 1;
        }
        if (currentTimeBoxes2 < numberOfTimeBoxes2)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(boxTimeToPlace, randoms);
            currentTimeBoxes2++;
        }
    }

    void generateRandomObjects3()
    {
        // create new gameObject on random position
        if (currentPickUp3 < numberOfPickUp3)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(pickUp3, randoms);
            currentPickUp3 += 1;
        }
        if (currentBombs3 < numberOfBombs3)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(bomb, randoms);
            currentBombs3 += 1;
        }
        if (currentTimeBoxes3 < numberOfTimeBoxes3)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(boxTimeToPlace, randoms);
            currentTimeBoxes3++;
        }

    }

    void generateRandomObjects4()
    {

        // create new gameObject on random position
        if (currentPickUp4 < numberOfPickUp4)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(pickUp4, randoms);
            currentPickUp4 += 1;
        }
        if (currentBombs4 < numberOfBombs4)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(bomb, randoms);
            currentBombs4 += 1;
        }
        if (currentTimeBoxes4 < numberOfTimeBoxes4)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(boxTimeToPlace, randoms);
            currentTimeBoxes4++;
        }
    }

    //returns a random position inside the ground object
    Vector3 getRandomPositions()
    {
        // generate random x position
        int posx = Random.Range(-terrainWidth / 2, terrainWidth / 2);
        // generate random z position
        int posz = Random.Range(-terrainLength / 2, terrainLength / 2);
        // get the terrain height at the random position
        float posy = 0.5f;

        return new Vector3(terrainPosX + posx, posy, terrainPosZ + posz);
    }


    //Generates new GameObject and places it to vec position
    void generateObject(GameObject gameObject, Vector3 vec) {
        GameObject newObject = (GameObject)Instantiate(gameObject, vec, Quaternion.identity);
    }

    //generate Wind Box
    public void generateWindBox()
    {
        Vector3 randoms = getRandomPositions();
        generateObject(boxWindToPlace, randoms);
        currentWindBoxes += 1;
    }

     //Generates random windZone effect
     void generateRandomWind()
     {
        //if no wind
        if (!windW.gameObject.activeSelf && !windE.gameObject.activeSelf && !windN.gameObject.activeSelf && !windS.gameObject.activeSelf) {
            int rand = Random.Range(0, 10000);
            //activate a wind
            if (rand <= 10)
            {
                int wind = Random.Range(0, 3);
                switch (wind) {
                    case 0:
                        windW.gameObject.SetActive(true);
                        break;
                    case 1:
                        windE.gameObject.SetActive(true);
                        break;
                    case 2:
                        windN.gameObject.SetActive(true);
                        break;
                    case 3:
                        windS.gameObject.SetActive(true);
                        break;
                }
                Debug.Log("WIND ON "+wind);
                //ask for wind boxes
                generateWindBox();
            }
        }
    }

    //turns off all winds
    public void desactivateWind() {
        windW.gameObject.SetActive(false);
        windE.gameObject.SetActive(false);
        windN.gameObject.SetActive(false);
        windS.gameObject.SetActive(false);
    }

    //Returns wind that is activated for applying forces to player controller
    public GameObject getActiveWind() {
        if (windW.gameObject.activeSelf) {
            return windW.gameObject;
        }else if (windE.gameObject.activeSelf)
        {
            return windE.gameObject;
        }else if (windN.gameObject.activeSelf)
        {
            return windN.gameObject;
        }else if (windS.gameObject.activeSelf)
        {
            return windS.gameObject;
        }
        return null;
    }

    //ground resize
    public void resizeGround(int i) {    
        //big
        switch (i)
        {
            case 0:
                scaleX += 0.2f;
                scaleZ += 0.2f;
                ground.transform.localScale = new Vector3(scaleX, 1.0f, scaleZ);
                break;
            case 1:
                int rand = Random.Range(0, 10000);
                if (rand <= 10)
                {
                    scaleX -= 0.1f;
                    scaleZ -= 0.1f;
                    ground.transform.localScale = new Vector3(scaleX, 1.0f, scaleZ);
                    generateGroundBox();
                }
                break;
        }
        updateGroundParams();      
    }
    void generateGroundBox()
    {
        Vector3 randoms = getRandomPositions();
        generateObject(groundBoxToPlace, randoms);
    }
    bool updateGroundParams() {
        //Boxes
        var renderer = ground.GetComponent<Renderer>();
        // terrain size x
        terrainWidth = (int)renderer.bounds.size.x - 1;
        // terrain size z
        terrainLength = (int)renderer.bounds.size.z - 1;
        // terrain x position
        terrainPosX = (int)ground.transform.position.x;
        // terrain z position
        terrainPosZ = (int)ground.transform.position.z;
        return true;
    }

	private void setObjectsForDifficulty (float d) {
		
		if (d == 1) { // normal 
			numberOfPickUp1 = 10;
			numberOfPickUp2 = 9; 
			numberOfPickUp3 = 8; 
			numberOfPickUp4 = 7; 

			numberOfTimeBoxes = 2;
			numberOfTimeBoxes2 = 2;
			numberOfTimeBoxes3 = 3;
			numberOfTimeBoxes4 = 3;

			numberOfBombs1 = 1;
			numberOfBombs2 = 3;
			numberOfBombs3 = 4;
			numberOfBombs4 = 5;
		} else if (d == 2) { // hard 
			numberOfPickUp1 = 7;
			numberOfPickUp2 = 6; 
			numberOfPickUp3 = 4; 
			numberOfPickUp4 = 3; 

			numberOfTimeBoxes = 2;
			numberOfTimeBoxes2 = 2;
			numberOfTimeBoxes3 = 2;
			numberOfTimeBoxes4 = 2;

			numberOfBombs1 = 4;
			numberOfBombs2 = 5;
			numberOfBombs3 = 6;
			numberOfBombs4 = 7;
		} else { // easy
			numberOfPickUp1 = 10;
			numberOfPickUp2 = 10; 
			numberOfPickUp3 = 10; 
			numberOfPickUp4 = 10; 

			numberOfTimeBoxes = 2;
			numberOfTimeBoxes2 = 3;
			numberOfTimeBoxes3 = 4;
			numberOfTimeBoxes4 = 4;

			numberOfBombs1 = 0;
			numberOfBombs2 = 2;
			numberOfBombs3 = 3;
			numberOfBombs4 = 4;
		}
	}
}
