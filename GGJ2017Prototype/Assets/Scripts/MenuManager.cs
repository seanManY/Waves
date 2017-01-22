using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public SoundManager soundScript;
    string[] scenes = new string[] { "level_one 1", "level_one 2", "level_one"};
    int currentScene = 0;

	public void Restart()
    {
        soundScript.EndSoundAbrupt("GGJ 17_Waves_v1");
		SceneManager.LoadScene (scenes[currentScene]);
	}

    public void nextLevel()
    {
        soundScript.EndSoundAbrupt("GGJ 17_Waves_v1");
        currentScene++;
        if(currentScene >= scenes.Length)
            SceneManager.LoadScene("main_menu");
        SceneManager.LoadScene(scenes[currentScene + 1]);
    }

	public void Quit()
    {
		Application.Quit ();
	}
}
