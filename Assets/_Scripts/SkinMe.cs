using UnityEngine;
using System.Collections;

public class SkinMe : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {
        gameObject.GetComponent<UISprite>().atlas = GameObject.FindGameObjectWithTag("Skin").GetComponent<UIAtlas>();
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
