using UnityEngine;
using System.Collections;

public class RandomObjects : MonoBehaviour
{
    //Game Objects
    public GameObject ground; //terrain
    public GameObject pickUpToPlace; // GameObject to place
    public GameObject boxTimeToPlace; // GameObject to place
    public GameObject boxWindToPlace;
    public GameObject groundBoxToPlace;

    public WindZone windW, windE, windN, windS;


    //Couts of game objects "boxes"
    public int numberOfPickUp; // number of objects to place
    public int currentPickUp; // number of placed objects
    public int numberOfTimeBoxes; // number of objects to place
    public int currentTimeBoxes; // number of placed objects
    public int numberOfWindBoxes; // number of objects to place
    public int currentWindBoxes; // number of placed objects

    //Ground size parameters
    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z
    private float scaleX = 2.0f;
    private float scaleZ = 2.0f;
    void Start()
    {
        //Ground size parametes
        updateGroundParams();

        //Desactivate windZones
        desactivateWind();
    }

    // Update is called once per frame
    void Update()
    {
        //make ground small randomly
        resizeGround(1);

        // create new gameObject on random position
        if (currentPickUp < numberOfPickUp)
        {
            Vector3 randoms = getRandomPositions();
            generateObject(pickUpToPlace, randoms);
            currentPickUp += 1;
        }

        if (currentTimeBoxes < numberOfTimeBoxes) {
            Vector3 randoms = getRandomPositions();
            generateObject(boxTimeToPlace, randoms);
            currentTimeBoxes += 1;
        }

        //generate random winds
        generateRandomWind();


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

        return new Vector3(posx, posy, posz);
    }


    //Generates new GameObject and places it to vec position
    void generateObject(GameObject gameObject, Vector3 vec) {
        GameObject newObject = (GameObject)Instantiate(gameObject, vec, Quaternion.identity);
    }

    //generate Wind Box
    void generateWindBox()
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
    void updateGroundParams() {
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
    }
}
