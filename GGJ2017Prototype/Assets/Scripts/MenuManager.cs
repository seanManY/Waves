using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public SoundManager soundScript;

	public void Restart(){
        soundScript.EndSoundAbrupt("GGJ 17_Waves_v1");
		SceneManager.LoadScene ("level_one");
	}

	public void Quit(){
		Application.Quit ();
	}
}
