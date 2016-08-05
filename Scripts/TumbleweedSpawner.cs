using UnityEngine;
using System.Collections;

public class TumbleweedSpawner : MonoBehaviour {
	
	private float timeToSpawn = 0;
	private float spawnPos = -10.0f;


	public GameObject item;
	private float spawnRate;
	
	void Update () {
		spawnRate = Random.Range (0.3f, 0.6f);
		if ( Time.time > timeToSpawn) {
			timeToSpawn = Time.time + 10/spawnRate;	
			SpawnItem();
		}
	}
	
	
	void SpawnItem () {
		Vector3 spawnPosition = new Vector3 (spawnPos, 0.0f, 0.0f);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (item, spawnPosition, spawnRotation);
	}
}
