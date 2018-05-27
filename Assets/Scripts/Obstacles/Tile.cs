using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject[] powerups;
    [SerializeField] GameObject[] powerupList;
    [SerializeField] GameObject powerupSpawnPoint;
    [SerializeField] GameObject coins, obstacle;
    private GameObject powerup;


    void Awake() {
        //Instantiates one of each powerup for each tile
        for (int i = 0; i < 3; i++)
        {
            powerup = powerups[i];
            powerupList[i] = Instantiate(powerup, powerupSpawnPoint.transform.position, powerupSpawnPoint.transform.rotation, powerupSpawnPoint.transform);
            powerupList[i].SetActive(false);
        }
    }

	public void Spawn(GameObject currentSpawnPoint){

        //Resets the visibility of the collected coins in the scene
        foreach (Transform child in coins.transform)
            child.gameObject.SetActive(true);

        //Resets the visibility of the hit obstacles
        obstacle.gameObject.SetActive(true);

        //Spawns a random powerup (occasionally)
        if (Random.Range(1, 10) == 1)
            powerupList[Random.Range(0, 3)].SetActive(true);

        //Sets the tile visible
        transform.SpawnObject(currentSpawnPoint.transform);
	}

	void OnBecameInvisible(){
        //When the tile becomes invisible, it's set invisible and
        //powerups are deactivated
        foreach (Transform child in powerupSpawnPoint.transform)
            child.gameObject.SetActive(false);

        gameObject.SetActive (false);
	}

}
