using UnityEngine;
using System.Collections;

public class RenderLine : MonoBehaviour {

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed;

    public float widthX;
    public float widthY;

    private LineRenderer lineRenderer;
    private float counter;
    private float distance;
    private float originSpriteWidth;
    private float destSpriteWidth;
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(widthX, widthY);
        originSpriteWidth = origin.gameObject.GetComponent<UISprite>().width / 2;
        destSpriteWidth = destination.gameObject.GetComponent<UISprite>().width / 2;
        
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(origin.position, destination.position);
        if (counter < distance) {
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, distance, counter);
            
            Vector3 pointA = origin.position;
            pointA = new Vector3(pointA.x+.5f, pointA.y, 1.2f);
            Vector3 pointB = destination.position;
            pointB = new Vector3(pointB.x-.45f, pointA.y, 1.2f);
            Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(1, pointALongLine);
        }
	}
}
