using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {
    public float DestroyDelay;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, DestroyDelay);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
