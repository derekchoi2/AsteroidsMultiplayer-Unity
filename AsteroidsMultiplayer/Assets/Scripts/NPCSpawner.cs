using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

	public int neutralNum = 2;
	public GameObject neutralPrefab;

	public int enemyNum = 2;
	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < neutralNum; i++)
			Spawn (neutralPrefab);
		
		for (int i = 0; i < enemyNum; i++)
			Spawn (enemyPrefab);
	}

	Vector3 RandomPosition(){
		Vector3 output = Vector3.zero;
		output.z = 0;

		while (output.magnitude < 3) {
			output.x = Random.Range (-2f, 2f);
			output.y = Random.Range (-4.5f, 4.5f);
		}

		return output;
	}

	void Spawn(GameObject prefab){
		Instantiate (prefab, RandomPosition (), Quaternion.identity);
	}
}
