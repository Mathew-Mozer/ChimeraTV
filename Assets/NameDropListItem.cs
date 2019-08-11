using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDropListItem : MonoBehaviour {
    public float TimeToDestroy;
    public GameObject NameLabel;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, TimeToDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
