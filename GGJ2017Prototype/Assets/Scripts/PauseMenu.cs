using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;
    float timescale;

	// Use this for initialization
	void Start () {
        timescale = Time.timeScale;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
		}
	}

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = timescale;
        }
    }
}
