using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public GameObject gateA;
	public GameObject gateB;


    public SoundManager soundScript;

	public Vector2 offset;

	//public float activityDelay;
	bool active = true;

	void Update(){
		//if (activityDelay > 0) {
		//	activityDelay -= Time.deltaTime;
		//	if (activityDelay <= 0) {
				active = true;
		//	}
		//}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("player")) {
			Vector2 distance = (Vector2)other.gameObject.transform.position - (Vector2)gameObject.transform.position + offset*MovementController.i.facingDireciton;
			if (gameObject == gateA) {
				MovementController.i.SetNewPosition((Vector2)gateB.transform.position + distance);
				//					gateB.GetComponent<Teleporter> ().Deactivate (.1f);
			} else {
				MovementController.i.SetNewPosition((Vector2)gateA.transform.position + distance);
				//					gateA.GetComponent<Teleporter> ().Deactivate (.1f);
			}

            soundScript.PlaySound(1, 1f);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Teleporter Collision");
		if (active) {
			if (other.gameObject.CompareTag ("player")) {
				Vector2 distance = (Vector2)other.gameObject.transform.position - (Vector2)gameObject.transform.position + offset*MovementController.i.facingDireciton;
				if (gameObject == gateA) {
					MovementController.i.SetNewPosition((Vector2)gateB.transform.position + distance);
//					gateB.GetComponent<Teleporter> ().Deactivate (.1f);
				} else {
					MovementController.i.SetNewPosition((Vector2)gateA.transform.position + distance);
//					gateA.GetComponent<Teleporter> ().Deactivate (.1f);
				}
			}
			else if (other.gameObject.CompareTag ("trajectory")) {
				Vector2 distance = (Vector2)other.gameObject.transform.position - (Vector2)gameObject.transform.position + offset*MovementController.i.facingDireciton;
				if (gameObject == gateA) {
					other.GetComponent<Trajectory>().SetNewPosition((Vector2)gateB.transform.position + distance);
//					gateB.GetComponent<Teleporter> ().Deactivate (.1f);
				} else {
					other.GetComponent<Trajectory>().SetNewPosition((Vector2)gateA.transform.position + distance);
//					gateA.GetComponent<Teleporter> ().Deactivate (.1f);
				}
			}
//			Deactivate(.1f);
		}
	}

	public void Deactivate(float time){
		active = false;
		//activityDelay = time;
	}
}
