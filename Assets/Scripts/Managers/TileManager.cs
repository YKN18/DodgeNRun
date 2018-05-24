using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    [SerializeField] GameObject[] tileTypes;
    [SerializeField] int tileTypesLength = 3;
	[SerializeField] int tilesMaxLength;
	[SerializeField] GameObject currentSpawnPoint;
	[SerializeField] GameObject[] tilesList;
	[SerializeField] GameObject player;
	[SerializeField] float tileLength = 24f;
    [SerializeField] float rerootZ = 1000;
	private int tilesOnScreen = 10;
	private bool tileFound; 
	private int tileIndex = 0;
    private GameObject tile;

    public static TileManager instance;
	// Use this for initialization
	void Awake(){
        instance = this;
		tilesList = new GameObject[tilesMaxLength];
	}

	void Start () {
		for (int i = 0; i < tilesMaxLength; i++) {
            tile = tileTypes[Random.Range(0, tileTypesLength)];
			tilesList[i]=GameObject.Instantiate (tile,new Vector3(0, 0, 0), new Quaternion());
			tilesList[i].SetActive (false);
		}
		for (int i = 0; i < tilesOnScreen; i++) {
			SpawnTile ();
		}
	}
		
	// Update is called once per frame
	void Update () {
        if (player.transform.position.z >= rerootZ)
        {
            ReRootTiles();
            player.transform.position -= new Vector3(0, 0, player.transform.position.z);
        }
		if (player.transform.position.z > currentSpawnPoint.transform.position.z - tilesOnScreen * tileLength)
			SpawnTile ();
	}

    void ReRootTiles() {
        currentSpawnPoint.transform.position -= new Vector3(0, 0, player.transform.position.z);
        for (int i = 0; i < tilesMaxLength; i++)
        {
            if (tilesList[i].activeInHierarchy) {
                tilesList[i].transform.position -= new Vector3(0, 0, player.transform.position.z);
            }
        }
    }

	void SpawnTile(){
		tileFound = false;
		//Generiamo un indice casuale da cui partire per non generare le tiles sempre nello stesso ordine
		tileIndex = Random.Range (0, tilesMaxLength);
		while (!tileFound) {
			for (int i = tileIndex; i < tilesMaxLength; i++) {
				if (!tilesList [i].activeInHierarchy) {
					tilesList [i].GetComponent<Tile> ().Spawn (currentSpawnPoint);
					currentSpawnPoint.transform.position += new Vector3 (0, 0, tileLength);
					tileFound = true;
					break;
				}
			}
            if(!tileFound) tileIndex = 0;
		}
	}
}	
