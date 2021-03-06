﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemManager : MonoBehaviour {


	public static SystemManager i;								//Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	List<GameObject> activeObjects = new List<GameObject>();	//All active objects controlled by this script

	public float playerSpawnX;
	public float playerSpawnY;

	void Start(){
		i = this;
		//SpawnObject(Prefab.Player, new Vector3(playerSpawnX, playerSpawnY, 0));
	}

	void Update(){
		//Remove any objects that have been deleted
		activeObjects.RemoveAll(item => item == null);
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public void SpawnObject(int index, Vector3 location){
		activeObjects.Add(Instantiate (prefabs [index], location, Quaternion.identity) as GameObject);
	}

	//Convert enum to index and call SpawnObject
	public void SpawnObject(Prefab obj, Vector3 location){
		SpawnObject((int)obj, location);
	}
}
	
//Enum to easily convert prefab names to the appropriate index
public enum Prefab{
	Explosion = 0,
	Player = 1
};
