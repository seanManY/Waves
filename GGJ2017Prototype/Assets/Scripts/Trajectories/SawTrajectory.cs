using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrajectory : MonoBehaviour {

    Vector2 startPosition;
    int facingDireciton = 1;
    Vector2 lerpGoal;

    int waveSelect;

    bool sawWaveSatisfied = true;
    public float sawWaveHeight;
    public float sawWaveDistance;
    public float sawWaveReturn;
    int sawWaveStage;

	// Use this for initialization
	void Start () {
        if (transform.rotation.eulerAngles.y == 180)
            facingDireciton = -1;

        sawWaveStage = 0;
        startPosition = gameObject.transform.position;
        lerpGoal = startPosition + Vector2.right * facingDireciton * sawWaveDistance + Vector2.down * sawWaveHeight;
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        SawWaveUpdate();	
	}

    public void SawWaveUpdate()
    {
        //Move towards the goal
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, lerpGoal, .05f);

        //If goal reached, increment to next stage of movement
        if (Vector2.Distance(gameObject.transform.position, lerpGoal) < 0.1)
        {
            //Increment to next stage
            sawWaveStage++;

            //Set movement goal, or set satisfied condition
            switch (sawWaveStage)
            {
                case 1:
                    lerpGoal = startPosition + Vector2.right * facingDireciton * sawWaveDistance + Vector2.down * sawWaveHeight * (1 - sawWaveReturn);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
