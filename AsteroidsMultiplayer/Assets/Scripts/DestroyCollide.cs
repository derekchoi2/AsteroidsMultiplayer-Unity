using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyCollide : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("Wall") || !collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			Destroy (gameObject);
	}

}
