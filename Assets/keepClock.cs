using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keepClock : MonoBehaviour {

    int loadedLevel=0;
    bool canDestroy = false;
    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
        loadedLevel = SceneManager.GetActiveScene().buildIndex;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == loadedLevel)
        {
            if (canDestroy)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }else
        {
            canDestroy = true;
            switchToMiniClock();
        }
	}

    private void switchToMiniClock()
    {
        TweenPosition tp = gameObject.GetComponent<TweenPosition>();
        tp.PlayForward();
        TweenScale ts = gameObject.GetComponent<TweenScale>();
        ts.PlayForward();

    }
}
