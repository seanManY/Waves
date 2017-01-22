using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateGIF : MonoBehaviour {
    public Image background;
    public TwoD[] GIFs;
    int indexToDisplay;
    int indexToAnimate;

    int slowCounter;
    public int slow;

	// Use this for initialization
	void Start () {
        indexToAnimate = 0;
        indexToDisplay = 0;
        slowCounter = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        slowCounter++;
        if(slowCounter%2 == 0)
        {
            Animate();
        }
    }
    
    void Animate()
    {       
        if (GIFs.Length > 0 && indexToAnimate < GIFs[Mathf.Clamp(indexToDisplay, 0, GIFs.Length - 1)].g.Length)
        {
            background.sprite = GIFs[indexToDisplay].g[indexToAnimate];
            indexToAnimate++;
        }
        else
        {
            indexToAnimate = 0;
            if(GIFs.Length > 0)
            {
                indexToDisplay = (indexToDisplay + 1) % (GIFs.Length);
            }
            
        }
    }

    [System.Serializable]
    public class TwoD
    {
        public Sprite[] g;

    }
}
