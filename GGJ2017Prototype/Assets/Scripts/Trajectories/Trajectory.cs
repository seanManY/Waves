using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trajectory : MonoBehaviour {

	public Vector2 startPosition;
	public Vector2 lerpGoal;

	// Use this for initialization
	void Awake(){
		AwakeOverride ();
	}

	void Start () {
		StartOverride ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateOverride ();
	}

	void FixedUpdate(){
		FixedUpdateOverride ();
	}

	public abstract void AwakeOverride ();
	public abstract void StartOverride ();
	public abstract void UpdateOverride ();
	public abstract void FixedUpdateOverride();


	public void SetNewPosition(Vector2 location){
		Vector2 distance = location - (Vector2)gameObject.transform.position;

		startPosition += distance;
		lerpGoal += distance;

		gameObject.transform.position = location;
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (!other.gameObject.CompareTag ("teleporter") && !other.gameObject.CompareTag("player") && !other.gameObject.CompareTag("trajectory")) {
			Debug.Log ("Collision with " + other);
			Destroy (gameObject);
		}
	}

	public abstract void HaltMovement ();
}
