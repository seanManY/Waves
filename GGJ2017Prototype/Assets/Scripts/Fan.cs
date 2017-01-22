using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("player")){
			MovementController.i.HaltMovement ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("trajectory")){
			other.gameObject.GetComponent<Trajectory> ().HaltMovement ();
		}
	}
}
