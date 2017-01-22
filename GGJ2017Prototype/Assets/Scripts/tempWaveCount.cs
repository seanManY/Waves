using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCount : MonoBehaviour {

    int[] waveCount = new int[4];

    public Text triText;
    public Text sinText;
    public Text sqrText;
    public Text sawText;

    // Use this for initialization
    void Start ()
    {
        waveCount = new int[] { 3, 3, 3, 3 };
        setText();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//setText() when space is pressed.
	}

    void setText()
    {
        triText.text = waveCount[1].ToString();
        sinText.text = waveCount[1].ToString();
        sqrText.text = waveCount[1].ToString();
        sawText.text = waveCount[1].ToString();
    }
}
