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
        for (int i = 0; i < 3; i++)
        {
            powerup = powerups[i];
            powerupList[i] = Instantiate(powerup, powerupSpawnPoint.transform.position, powerupSpawnPoint.transform.rotation, powerupSpawnPoint.transform);
            powerupList[i].SetActive(false);
        }
    }

	public void Spawn(GameObject currentSpawnPoint){

        //Rendiamo visibili tutte le monete già prese
        foreach (Transform child in coins.transform)
            child.gameObject.SetActive(true);

        //Rendiamo visibili anche gli ostacoli già colpiti
        obstacle.gameObject.SetActive(true);

        //Rendiamo visibile un powerup a caso
        if (Random.Range(1, 10) == 1)
            powerupList[Random.Range(0, 3)].SetActive(true);

        //Infine attiviamo la tile
        transform.SpawnObject(currentSpawnPoint.transform);
	}

	void OnBecameInvisible(){
        foreach (Transform child in powerupSpawnPoint.transform)
            child.gameObject.SetActive(false);

        gameObject.SetActive (false);
	}

}
