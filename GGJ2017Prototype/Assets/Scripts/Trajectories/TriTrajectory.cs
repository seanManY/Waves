﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriTrajectory : Trajectory {

    
    int facingDireciton = 1;

    bool triangleWaveSatisfied = true;
    int triangleWaveStage;
    public float triangleWaveHeight;
    public float triangleWaveDistance;

	public override void AwakeOverride(){
	}
	public override void UpdateOverride(){
	}

	public override void HaltMovement(){
		triangleWaveSatisfied = true;
	}

	// Use this for initialization
	public override void StartOverride () {
        if (transform.rotation.eulerAngles.y == 180)
            facingDireciton = -1;
        startPosition = gameObject.transform.position;
        lerpGoal = startPosition + Vector2.up * triangleWaveHeight + Vector2.right * facingDireciton * triangleWaveDistance / 2;
    }
	
	// Update is called once per frame
	public override void FixedUpdateOverride () 
    {
        TriangleWaveUpdate();	
	}


		
    public void TriangleWaveUpdate()
    {
        //Move towards the goal
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, lerpGoal, .05f);

        //If goal reached, increment to next stage of movement
        if (Vector2.Distance(gameObject.transform.position, lerpGoal) < 0.1)
        {
            //Increment to next stage
            triangleWaveStage++;
            //Set movement goal, or set satisfied condition
            switch (triangleWaveStage)
            {
                case 1:
                    lerpGoal = startPosition + Vector2.right * facingDireciton * triangleWaveDistance;
                    break;
                case 2:
                    lerpGoal = startPosition - Vector2.up * triangleWaveHeight + Vector2.right * facingDireciton * triangleWaveDistance * 3 / 2;
                    break;
                case 3:
                    lerpGoal = startPosition + Vector2.right * facingDireciton * 2 * triangleWaveDistance;
                    break;
			default:
				Debug.Log ("Destory based on stage");
                    Destroy(gameObject);
                    break;
            }
        }
    }
		

}
