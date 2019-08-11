using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateobject : MonoBehaviour {

    public float speed = 10f;
    public bool isSpinning = true;
	// Use this for initialization
	void Start () {
        
	}
	
    public void killDelay()
    {
        StartCoroutine(killChip());
    }
    private IEnumerator killChip()
    {
        //Debug.Log("Should die");
        yield return new WaitForSeconds(25);
        GameObject.Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.name.Equals("Cube"))
        {
            if (collision.gameObject.GetComponent<rotateobject>())
            {
                if (!collision.gameObject.GetComponent<rotateobject>().isSpinning)
                {
                    isSpinning = false;
                }
            }
        }else
        {
            isSpinning = false;
        }
            
        
    }
    void FixedUpdate()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}
