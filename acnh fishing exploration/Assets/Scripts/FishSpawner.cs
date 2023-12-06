using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// spawns fish at the pos of a random water block
/// </summary>
public class FishSpawner : MonoBehaviour
{
    public static FishSpawner instance;
    public GameObject[] waterBlocks;
    public GameObject[] fishPrefabs;
    public GameObject bigFish, medFish, smallFish;
    private int maxFish = 3;
    public int fish = 0;
    private Vector3 spawnPoint;
    private int selectedBlock;
    private int selectedFish;

    private void Awake()
    {
        //initialize fish array to hold max fish
        //fish = new GameObject[maxFish];

        //make an array of all water blocks
        instance = this;

        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //check how many fish are in the scene
        if (fish < maxFish)
        {
            //get a random fish size and water block
            RollRandomFish();
            RollRandomWater();
            //print(selectedBlock);
            //print(selectedFish);
            //set the spawn point to the pos of the selected water block
            //spawn fish at spawn point
            print("spawning fish");
            spawnPoint = waterBlocks[selectedBlock].transform.position;
            spawnPoint.y = -1.1f;
            Instantiate(fishPrefabs[selectedFish], spawnPoint, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
            fish++;
        }
    }

    private int RollRandomWater()
    {
        selectedBlock = Random.Range(0, waterBlocks.Length);

        return selectedBlock;
    }

    private int RollRandomFish()
    {
       selectedFish = Random.Range(0, 3);
        //print("Selected fish" + selectedFish);
        return selectedFish;
    }

    
}
