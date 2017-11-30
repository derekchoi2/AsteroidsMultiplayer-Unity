using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	float turn;
	bool accelerate;
	bool shoot;

	bool randomise = true;

	public void SimulateMovement(PlayerController player){

		if (randomise) {
			randomise = false;
			StartCoroutine (RandomiseTimer ());

			turn = (float)Random.Range (-1, 2);
			accelerate = (Random.Range (0, 2) == 0) ? false : true;
			shoot = (Random.Range (0, 2) == 0) ? false : true;
		}
			
		player.accelerate = accelerate;
		if (shoot)
			player.Shoot ();
		player.Turn (turn);

	}

	IEnumerator RandomiseTimer(){
		yield return new WaitForSeconds (0.2f);
		randomise = true;
	}

}
