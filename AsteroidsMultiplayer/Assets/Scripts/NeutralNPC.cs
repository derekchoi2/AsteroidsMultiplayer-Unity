using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralNPC : MonoBehaviour {

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		rb.AddForce (new Vector3 (Random.Range (-50f, 50f), Random.Range (-50f, 50f), Random.Range (-50f, 50f)));
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Projectile"))
			Destroy (collision.gameObject);

		if (!collision.gameObject.CompareTag("Wall"))
			Destroy (gameObject); //neutral npc gets destroyed if comes into contact with anything other than wall
	}

	
}
