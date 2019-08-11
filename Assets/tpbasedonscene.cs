using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tpbasedonscene : MonoBehaviour {

    private TweenPosition tp;
	// Use this for initialization
	void Start () {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        tp = gameObject.GetComponent<TweenPosition>();
    }
    private void SceneManager_activeSceneChanged(Scene previousScene, Scene activeScene)
    {
        Vector3 toPoint;
        switch (activeScene.buildIndex)
        {
            case 4:
                toPoint = new Vector3(443, 381, 0);
                tp.to = toPoint;
                break;
            case 5:
                toPoint = new Vector3(475, 280, 0);
                tp.to = toPoint;
                break;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
