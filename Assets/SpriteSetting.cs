using UnityEngine;
using System.Collections;

public class SpriteSetting : MonoBehaviour {
/*    public int column;
    public int row;
    //for Lerping
    Vector3 endMarker;
    float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private bool _isLerping;
    //end for lerping
    //for shrinking
    private bool _isShrinking;
    public float targetScale = 0.1f;
    public float shrinkSpeed = 1f;
    //end for shrinking
    public PictureManager pm;
    public GameObject pmgo;
	// Use this for initialization
	void Awake () {
        pm = GameObject.FindGameObjectWithTag("PictureManager").GetComponent<PictureManager>();
        pmgo = GameObject.FindGameObjectWithTag("PictureManager");
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (_isLerping)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, endMarker, fracJourney);
        }
        if (_isShrinking)
        {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale),shrinkSpeed);
        }
    }
    public void animateDestroy(int effectID)
    {
        
        switch (effectID)
        {
            case 1:
                endMarker = pm.AttractPoints[0].transform.localPosition;
                startTime = Time.time;
                journeyLength = Vector3.Distance(transform.position, endMarker);
                _isLerping = true;
                _isShrinking = true;
                break;
            case 2:
                endMarker = pm.AttractPoints[1].transform.localPosition;
                startTime = Time.time;
                journeyLength = Vector3.Distance(transform.position, endMarker);
                _isLerping = true;
                break;
            case 3:
                StartCoroutine(destroyit(Random.Range(0F, 1F)));
                break;
        }
    }
    IEnumerator destroyit(float ttl)
    {
        yield return new WaitForSeconds(ttl);
        DestroyObject(gameObject);
    } */
}
