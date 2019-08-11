using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuitem : MonoBehaviour {

    public bool selected = false;
    public string packagename;
    public string ButtonTitle;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            gameObject.GetComponent<UISprite>().color = Color.gray;
        }else
        {
            gameObject.GetComponent<UISprite>().color = Color.white;
        }
	}
}
