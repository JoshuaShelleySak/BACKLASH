using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the items randomly within a predetermined area defined by a box collider.
/// </summary>
public class ItemSpawning : MonoBehaviour {

    //Where the items can spawn
    Collider itemSpawnArea;
    float itemSpawnDelay = 2.5f;
    //Time until the next item spawns
    float timeUntilSpawn;

    //Locations where the items will spawn
    Vector3[] spawnLocations = new Vector3[10];
    //Max number of spawns allowed
    int maxNumberOfSpawns = 10;
    //How many items are currently spawned
    int currentNumSpawned = 0;

    //The minimum and maximum coordinates of where the items could potentially spawn
    Vector3 minPos;
    Vector3 maxPos;

    //The item that is going to be spawned/instantiated
    [SerializeField] private GameObject itemBeingSpawned;

	// Use this for initialization
	void Start () {
        //Gets the area that the items can spawn within
        itemSpawnArea = GetComponent<Collider>();        

        //Sets the min and max positions that the items can spawn at
        minPos = itemSpawnArea.bounds.min;
        maxPos = itemSpawnArea.bounds.max;

        CreateItemSpawnLocations();
    }
	
	// Update is called once per frame
	void Update () {
        //Removes time every frame
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0.0f)
        {
            //Sets the delay then spawns an item.
            timeUntilSpawn = itemSpawnDelay;
            SpawnItem();
        }
	}

    //Spawns items within the collider area and gives them a random itemID. 
    void SpawnItem()
    {
        //If the current number of spawned items hasn't exceeded the max allowed
        if (currentNumSpawned != maxNumberOfSpawns)
        {
            //Sets the item being spawned position to be one of the previously created spawn locations.
            itemBeingSpawned.transform.position = spawnLocations[currentNumSpawned];
            //Increments the current number of spawned items.
            currentNumSpawned++;
            //Creates a variable to store the item about to be spawned into.
            GameObject spawnedItem = null;

            //Creates a random item ID for the item.
            int randItemId = Random.Range(1, 4);
            //Instantiates the item being spawned.
            spawnedItem = Instantiate(itemBeingSpawned);
            //Sets the parent to be the spawn area. Keeps the hierarchy tidy.
            spawnedItem.transform.parent = this.transform;
            //Sets the item ID to the randomly created one to allow players to pick up the items.
            spawnedItem.GetComponent<Item>().SetItemID(randItemId);
        }
        else
        {
            //Creates more spawn locations to allow more items to spawn.
            CreateItemSpawnLocations();
        }
    }

    /// <summary>
    /// This will be ran when the game needs more location spawns, allows for records to be kept 
    /// of locations taken so far whilst still being random each game
    /// </summary>
    void CreateItemSpawnLocations()
    {
        //While i is less than the max number of allowed spawns.
        for (int i = 0; i < maxNumberOfSpawns; i++)
        {
            //If the current location is Vector3.zero (it is this by default)
            if (spawnLocations[i] == Vector3.zero)
            {
                //Set the x and y spawn position to a random position within the collider's min and max coordinates.
                float xSpawnPos = Random.Range(minPos.x, maxPos.x);
                float zSpawnPos = Random.Range(minPos.z, maxPos.z);

                //Sets the currently selected spawnLocation to be the randomly chosen x and y positions.
                //The Y is at 0.5 to stop the weapons from spawning in the ground on the saloon map.
                spawnLocations[i] = new Vector3(xSpawnPos, 0.5f, zSpawnPos);
            }            
        }
    }
}
