using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

	public GameObject victoryScreen;



	void OnCollisionEnter2D(Collision2D other){
		victoryScreen.SetActive (true);
	}
}
