using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public Rigidbody2D rgbd;
	public float moveSpeed;

	Vector2 startPosition;
	Vector2 lerpGoal;

	int facingDireciton = 1;

	public bool muteInput = false;

	//Square Wave
	bool squareWaveSatisfied = true;
	int squareWaveStage;
	public float squareWaveHeight;
	public float squareWaveDistance;

	//Triangle Wave
	bool triangleWaveSatisfied = true;
	int triangleWaveStage;
	public float triangleWaveHeight;
	public float triangleWaveDistance;

	//Sin Wave
	bool sinWaveSatisfied = true;
	public float sinWaveHeight;
	public float sinWaveWavelength;
	public float sinWaveFrequency;
	float sinWaveTime;
	int sinWaveStage;
	public float sinWaveTail;

	//Saw Wave
	bool sawWaveSatisfied = true;
	public float sawWaveHeight;
	public float sawWaveDistance;
	int sawWaveStage;

	void Start(){
		rgbd.gravityScale = 0;
	}

	// Update is called once per frame
	void Update () {
		if (!muteInput) {
			StandardMovement ();
		}
		if (!squareWaveSatisfied) {
			SquareWaveUpdate ();
		}
		if (!triangleWaveSatisfied) {
			TriangleWaveUpdate ();
		}
		if (!sinWaveSatisfied) {
			SinWaveUpdate ();
		}
		if (!sawWaveSatisfied) {
			SawWaveUpdate ();
		}

	}

	public void StandardMovement(){
		if (Input.GetKey (KeyCode.D)) {
			//rgbd.MovePosition ((Vector2)rgbd.transform.position + Vector2.right * moveSpeed * Time.deltaTime);
			facingDireciton = 1;
		}
		if (Input.GetKey (KeyCode.A)) {
			//rgbd.MovePosition ((Vector2)rgbd.transform.position - Vector2.right * moveSpeed * Time.deltaTime);
			facingDireciton = -1;
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			StartSquareWave ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			StartTriangleWave ();
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			StartSinWave ();
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			StartSawWave ();
		}
	}

	public void EndTriangleWave(){
		triangleWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
	}

	public void TriangleWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, lerpGoal, .05f);

		//If goal reached, increment to next stage of movement
		if (Vector2.Distance (gameObject.transform.position, lerpGoal) < 0.1) {
			//Increment to next stage
			triangleWaveStage++;
			//Set movement goal, or set satisfied condition
			switch (triangleWaveStage) {
			case 1:
				lerpGoal = startPosition + Vector2.right*facingDireciton * triangleWaveDistance;
				break;
			case 2:
				lerpGoal = startPosition - Vector2.up*triangleWaveHeight + Vector2.right*facingDireciton * triangleWaveDistance*3/2;
				break;
			case 3:
				lerpGoal = startPosition + Vector2.right*facingDireciton*2 * triangleWaveDistance;
				break;
			default:
				EndTriangleWave ();
				break;
			}
		}
	}

	public void StartTriangleWave(){
		muteInput = true;
		triangleWaveSatisfied = false;
		triangleWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.up*triangleWaveHeight + Vector2.right*facingDireciton * triangleWaveDistance/2;
		//rgbd.gravityScale = 0;
	}

	public void StartSquareWave(){
		muteInput = true;
		squareWaveSatisfied = false;
		squareWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.up*squareWaveHeight;
		//rgbd.gravityScale = 0;
	}

	public void SquareWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, lerpGoal, .05f);

		//If goal reached, increment to next stage of movement
		if (Vector2.Distance (gameObject.transform.position, lerpGoal) < 0.1) {
			//Increment to next stage
			squareWaveStage++;

			//Set movement goal, or set satisfied condition
			switch (squareWaveStage) {
				case 1:
				lerpGoal = startPosition + Vector2.up*squareWaveHeight + Vector2.right*facingDireciton*squareWaveDistance;
					break;
				case 2:
				lerpGoal = startPosition + Vector2.right*facingDireciton*squareWaveDistance;
					break;
				default:
					EndSquareWave ();
					break;
			}
		}
	}

	public void EndSquareWave(){
		squareWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
	}

	public void StartSinWave(){
		muteInput = true;
		sinWaveSatisfied = false;
		startPosition = gameObject.transform.position;
		//rgbd.gravityScale = 0;
		lerpGoal = startPosition + Vector2.right*facingDireciton * sinWaveWavelength;
		sinWaveTime = 0;
		sinWaveStage = 0;
	}

	public void SinWaveUpdate(){
		//lerpGoal += Vector2.right*facingDireciton * Time.deltaTime * sinWaveDistance;
		//transform.position = lerpGoal + Vector2.up * Mathf.Sin (Time.time * sinWaveFrequency) * sinWaveHeight;

		startPosition += (Vector2)transform.right*facingDireciton * Time.deltaTime * sinWaveWavelength;
		transform.position = startPosition - (Vector2)transform.up * Mathf.Sin ((sinWaveTime + 2*Mathf.PI)* sinWaveFrequency) * sinWaveHeight;
		sinWaveTime += Time.deltaTime;
		if (sinWaveTime >= 6.3f*sinWaveTail / sinWaveFrequency) {
			EndSinWave ();
		}

	}

	public void EndSinWave(){
		sinWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
	}


	public void StartSawWave(){
		muteInput = true;
		sawWaveSatisfied = false;
		sawWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.right*facingDireciton*sawWaveDistance + Vector2.up*sawWaveHeight;
		//rgbd.gravityScale = 0;
	}

	public void SawWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, lerpGoal, .05f);

		//If goal reached, increment to next stage of movement
		if (Vector2.Distance (gameObject.transform.position, lerpGoal) < 0.1) {
			//Increment to next stage
			sawWaveStage++;

			//Set movement goal, or set satisfied condition
			switch (sawWaveStage) {
			case 1:
				lerpGoal = startPosition + Vector2.right*facingDireciton*sawWaveDistance;
				break;
			default:
				EndSquareWave ();
				break;
			}
		}
	}

	public void EndSawWave(){
		sawWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
	}


	void OnCollisionEnter2D(Collision2D other){
		EndSinWave ();
		EndSawWave ();
		EndSquareWave ();
		EndTriangleWave ();

		SystemManager.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
		CameraController.i.ScreenShake (2f, .5f);
		Destroy (gameObject);
	}
}
