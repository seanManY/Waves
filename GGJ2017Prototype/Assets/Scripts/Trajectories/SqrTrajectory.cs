using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqrTrajectory : MonoBehaviour {

    Vector2 startPosition;
    Vector2 lerpGoal;

    int facingDireciton = 1;

    bool squareWaveSatisfied = true;
    int squareWaveStage;
    public float squareWaveHeight;
    public float squareWaveDistance;
    public float squareWaveReturn;

	// Use this for initialization
	void Start () {
        if (transform.rotation.eulerAngles.y == 180)
            facingDireciton = -1;

        squareWaveStage = 0;
        startPosition = gameObject.transform.position;
        lerpGoal = startPosition + Vector2.up * squareWaveHeight;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SquareWaveUpdate();
	}

    public void SquareWaveUpdate()
    {
        //Move towards the goal
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, lerpGoal, .05f);

        //If goal reached, increment to next stage of movement
        if (Vector2.Distance(gameObject.transform.position, lerpGoal) < 0.1)
        {
            //Increment to next stage
            squareWaveStage++;

            //Set movement goal, or set satisfied condition
            switch (squareWaveStage)
            {
                case 1:
                    lerpGoal = startPosition + Vector2.up * squareWaveHeight + Vector2.right * facingDireciton * squareWaveDistance;
                    break;
                case 2:
                    lerpGoal = startPosition + Vector2.right * facingDireciton * squareWaveDistance + Vector2.up * squareWaveHeight * (1 - squareWaveReturn);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
