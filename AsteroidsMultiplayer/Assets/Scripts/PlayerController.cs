using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject projectile;
	public Transform projectileOrigin;
	public ParticleSystem accelerateEffect;

	public Material playerMat;

	public GameObject explosionPrefab;

	public float projectileSpeed = 8;
	float maxVelocity = 3;
	float speedMultiplier = 2;
	float rotateValue = 2;
	Rigidbody rb;
	//AI ai;
	bool canShoot = true;
	[HideInInspector]public bool accelerate = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		accelerateEffect.Stop ();
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material = playerMat;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		
		InputManager.ReadInputs (this);
		//else
			//ai.SimulateMovement (this);

			if (accelerate)
				Accelerate ();
			else
				accelerateEffect.Stop ();
	}

	void Accelerate(){
		rb.AddRelativeForce (new Vector3(0, speedMultiplier, 0), ForceMode.Force);
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity);
		if (!accelerateEffect.isEmitting)
			accelerateEffect.Play();
	}

	public void Turn(float dir){
		if (dir != 0) {
			rb.AddTorque (Vector3.back * dir);
			rb.angularVelocity = Vector3.ClampMagnitude (rb.angularVelocity, rotateValue);
		} else
			rb.angularVelocity = Vector3.zero;
	}

	[Command]
	public void CmdShoot(){
		if (canShoot) {
			canShoot = false;
			StartCoroutine (shootTimer ());
			GameObject bullet = Instantiate (projectile, projectileOrigin.position, projectileOrigin.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = transform.up * projectileSpeed;
			NetworkServer.Spawn (bullet);
		}
	}

	public void Shoot(){
		if (canShoot) {
			canShoot = false;
			StartCoroutine (shootTimer());
		}
	}

	IEnumerator shootTimer(){
		yield return new WaitForSeconds (0.5f);
		canShoot = true;
	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.CompareTag ("Wall"))
			return;

		if (collision.gameObject.CompareTag ("Player") || (collision.gameObject.CompareTag ("Projectile") && collision.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer))
			KillPlayer ();
	}

	void KillPlayer(){
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
