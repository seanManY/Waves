﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinTrajectory : MonoBehaviour {

    Vector2 startPosition;
    int facingDireciton = 1;

    int waveSelect; 

    bool sinWaveSatisfied = true;
    public float sinWaveHeight;
    public float sinWaveWavelength;
    public float sinWaveFrequency;
    float sinWaveTime;
    int sinWaveStage;
    public float sinWaveTail;

	// Use this for initialization
	void Start () {
        startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        SinWaveUpdate();		
	}

    public void SinWaveUpdate()
    {
        //lerpGoal += Vector2.right*facingDireciton * Time.deltaTime * sinWaveDistance;
        //transform.position = lerpGoal + Vector2.up * Mathf.Sin (Time.time * sinWaveFrequency) * sinWaveHeight;

        startPosition += (Vector2)transform.right * facingDireciton * Time.deltaTime * sinWaveWavelength;
        transform.position = startPosition - (Vector2)transform.up * Mathf.Sin((sinWaveTime + 2 * Mathf.PI) * sinWaveFrequency) * sinWaveHeight;
        sinWaveTime += Time.deltaTime;
        if (sinWaveTime >= 6.3f * sinWaveTail / sinWaveFrequency)
        {
            Destroy(gameObject);
        }

    }

    public void setWaveSelect(int newWaveSelect)
    {
        waveSelect = newWaveSelect;
    }
}
