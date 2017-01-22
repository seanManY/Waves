using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

    public GameObject TriGUI;
    public GameObject SinGUI;
    public GameObject SqrGUI;
    public GameObject SawGUI;

    // Use this for initialization
    void Start ()
    {
        moveHL(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void moveHL(int waveSelect)
    {
        switch(waveSelect)
        {
            case 1:
                gameObject.transform.position = new Vector2(SinGUI.transform.position.x, SinGUI.transform.position.y);
                break;
            case 2:
                gameObject.transform.position = new Vector2(SqrGUI.transform.position.x, SqrGUI.transform.position.y);
                break;
            case 3:
                gameObject.transform.position = new Vector2(SawGUI.transform.position.x, SawGUI.transform.position.y);
                break;
            default:
                gameObject.transform.position = new Vector2(TriGUI.transform.position.x, TriGUI.transform.position.y);
                break;
        }
    }
}
