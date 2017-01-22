using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public SoundManager soundScript;
    public string nextScene;
    //string[] scenes = new string[] { "level_one 1", "level_one 2", "level_one"};
    //string currentScene = SceneManager.GetActiveScene().name;

	public void Restart()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        soundScript.EndSoundAbrupt("GGJ 17_Waves_v1");
		SceneManager.LoadScene (currentScene);
	}

    public void nextLevel()
    {
        soundScript.EndSoundAbrupt("GGJ 17_Waves_v1");
    //    currentScene += 2;
    //    if(currentScene >= scenes.Length)
    //        SceneManager.LoadScene("main_menu");
    //    else
            SceneManager.LoadScene(nextScene);
    }

	public void Quit()
    {
		Application.Quit ();
	}
}
