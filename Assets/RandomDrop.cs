using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour {
    [SerializeField]
    public Vector3 LeftTopRange;
    [SerializeField]
    public Vector3 RightBottomRange;
    public float top;
    public float left;
    // Use this for initialization
    void Start () {
        Debug.Log("Starting");
        top = Random.Range(LeftTopRange.y, RightBottomRange.y);
        left = Random.Range(LeftTopRange.x, RightBottomRange.x);
        transform.localPosition = new Vector3(left, top);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
