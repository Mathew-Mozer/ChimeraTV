using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MonteCarloManager : MonoBehaviour {
    private DisplayManager displayManager;
    public List<GameObject> FieldTemplates = new List<GameObject>();
    // Use this for initialization
    void Awake () {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
