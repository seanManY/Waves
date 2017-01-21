using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public static MovementController i;

	public Rigidbody2D rgbd;
	public float moveSpeed;

    public GameObject SinTrajectory;
    public GameObject TriTrajectory;
    public GameObject SawTrajectory;
    public GameObject SqrTrajectory;

	Vector2 startPosition;
	Vector2 lerpGoal;

	public int facingDireciton = 1;
    int waveSelect = 0;
    int tempWaveSelect = 0;
    int[] waveCount = new int[4];

	public bool muteInput = false;

    int fireRate = 200;
    int fireCount = 0;

    //Square Wave
    bool squareWaveSatisfied = true;
	int squareWaveStage;
	public float squareWaveHeight;
	public float squareWaveDistance;
    public float squareWaveReturn;

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
    public float sawWaveReturn;
	int sawWaveStage;

    //Joystick
    int joyVert = 0;

	void Start(){
		i = this;

		rgbd.gravityScale = 0;

        //Set Wave Limits
        waveCount = new int[] {3,3,3,3};
	}

	// Update is called once per frame
	void FixedUpdate () {
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

        instantiateTrajectory();
	}

	public void StandardMovement(){
        //Keyboard Controller
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			//rgbd.MovePosition ((Vector2)rgbd.transform.position + Vector2.right * moveSpeed * Time.deltaTime);
			facingDireciton = 1;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			//rgbd.MovePosition ((Vector2)rgbd.transform.position - Vector2.right * moveSpeed * Time.deltaTime);
			facingDireciton = -1;
		}
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
			Debug.Log ("Pre-increment: " + waveSelect);
            waveSelect = (waveSelect + 1) % 4;
            Debug.Log(waveSelect);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            waveSelect = (waveSelect + 3) % 4;
            Debug.Log(waveSelect);
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if(waveCount[waveSelect] > 0)
            {
                switch (waveSelect)
                {
                    case 1:
                        waveCount[waveSelect]--;
                        StartSinWave();
                        break;
                    case 2:
                        waveCount[waveSelect]--;
                        StartSquareWave();
                        break;
                    case 3:
                        waveCount[waveSelect]--;
                        StartSawWave();
                        break;
                    default:
                        waveCount[waveSelect]--;
                        StartTriangleWave();
                        break;
                }
            }
        }
        /*
        //Joystick Controller
        if (Input.GetAxis("Horizontal") < 0)
        {
            facingDireciton = -1;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            facingDireciton = 1;
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            joyVert = 0;
        }
        if (joyVert == 0)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
				Debug.Log ("Controller Increment");
                waveSelect = (waveSelect + 1) % 4;
                joyVert++;
            }
        }
        if (joyVert == 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                waveSelect = (waveSelect + 3) % 4;
                joyVert--;
            }
        }
		*/
    }

	public void EndTriangleWave(){
		triangleWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
        waveSelect = tempWaveSelect;
	}

	public void TriangleWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, lerpGoal, .05f);

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
        tempWaveSelect = 0;
        waveSelect = -1;
		muteInput = true;
		triangleWaveSatisfied = false;
		triangleWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.up*triangleWaveHeight + Vector2.right*facingDireciton * triangleWaveDistance/2;
		//rgbd.gravityScale = 0;
	}

	public void StartSquareWave(){
        tempWaveSelect = 2;
        waveSelect = -1;
		muteInput = true;
		squareWaveSatisfied = false;
		squareWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.up*squareWaveHeight;
		//rgbd.gravityScale = 0;
	}

	public void SquareWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, lerpGoal, .05f);

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
				lerpGoal = startPosition + Vector2.right*facingDireciton*squareWaveDistance + Vector2.up*squareWaveHeight*(1-squareWaveReturn);
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
        waveSelect = tempWaveSelect;
	}

	public void StartSinWave(){
        tempWaveSelect = 1;
        waveSelect = -1;
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
        waveSelect = tempWaveSelect;
	}


	public void StartSawWave(){
        tempWaveSelect = 3;
        waveSelect = -1;
		muteInput = true;
		sawWaveSatisfied = false;
		sawWaveStage = 0;
		startPosition = gameObject.transform.position;
		lerpGoal = startPosition + Vector2.right*facingDireciton*sawWaveDistance + Vector2.down*sawWaveHeight;
		//rgbd.gravityScale = 0;
	}

	public void SawWaveUpdate(){
		//Move towards the goal
		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, lerpGoal, .05f);

		//If goal reached, increment to next stage of movement
		if (Vector2.Distance (gameObject.transform.position, lerpGoal) < 0.1) {
			//Increment to next stage
			sawWaveStage++;

			//Set movement goal, or set satisfied condition
			switch (sawWaveStage) {
			case 1:
				lerpGoal = startPosition + Vector2.right*facingDireciton*sawWaveDistance + Vector2.down*sawWaveHeight*(1-sawWaveReturn);
				break;
			default:
				EndSawWave ();
				break;
			}
		}
	}

	public void EndSawWave(){
		sawWaveSatisfied = true;
		//rgbd.gravityScale = 1;
		muteInput = false;
        waveSelect = tempWaveSelect;
	}


	void OnCollisionEnter2D(Collision2D other){
		if (!other.gameObject.CompareTag ("teleporter")) {
			EndSinWave ();
			EndSawWave ();
			EndSquareWave ();
			EndTriangleWave ();

			SystemManager.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
			CameraController.i.ScreenShake (2f, .5f);
			Destroy (gameObject);
		}
	}


    //Trajectory
    public void instantiateTrajectory()
    {
        if (fireCount >= 20)
        {
            switch (waveSelect)
            {
                case 0 : if (facingDireciton < 0)
                        Instantiate(TriTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                    else
                        Instantiate(TriTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    break;

                case 1: if( facingDireciton < 0)
                        Instantiate(SinTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        else
                        Instantiate(SinTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        break;

                case 2: if (facingDireciton < 0)
                            Instantiate(SqrTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        else
                            Instantiate(SqrTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        break;

                case 3: if (facingDireciton < 0)
                            Instantiate(SawTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        else
                            Instantiate(SawTrajectory, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        break;

                default: break;

            }
           
            //SystemManager.i.SpawnObject(Prefab.Trajectory, gameObject.transform.position);
            fireCount = 0;
        }
        fireCount++;
        
        //traj.setWaveSelect(waveSelect);
    }

	public void SetNewPosition(Vector2 location){
		Vector2 distance = location - (Vector2)gameObject.transform.position;

		startPosition += distance;
		lerpGoal += distance;

		gameObject.transform.position = location;
	}
}
