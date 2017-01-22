using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string nextScene;
    public GameObject loadingScreen;

	// Use this for initialization
	void Start () {
        //Play main menu music
        //SoundManager.i.PlaySound(Sound.Song1);

	}
	


    public void StartButton()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(nextScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
