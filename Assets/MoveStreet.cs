using UnityEngine;
using System.Collections;


public class MoveStreet : MonoBehaviour {
    public Camera mainCamera;
    public float endY;
    public float toY;
    public int speed;
    // Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * speed);
        if (transform.localPosition.y < endY)
        {
            Debug.Log("Done");
            transform.localPosition = new Vector3(transform.localPosition.x, toY, 0);

        }
	}
    
}
