using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
    public int EffectID;
    DisplayManager displayManager;
	// Use this for initialization
    void Awake()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
    }
	void Update () {
        if (displayManager.currentScene.EffectID != EffectID)
        {
            gameObject.SetActive(false);
        }
	}
}
