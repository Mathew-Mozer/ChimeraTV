using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {
	private Transform inventory;
	public float smooth;
	private bool used;
    private HighHandManager hhManager;
	// Use this for initialization
	void Start () {
    inventory = GameObject.FindGameObjectWithTag("SilverCoinDeposit").transform;
	hhManager = GameObject.FindGameObjectWithTag("HighHandManager").GetComponent<HighHandManager>();
	}
	
	// Update is called once per frame
	void Update () {
	transform.position = Vector3.Lerp(transform.position, inventory.position,smooth * Time.deltaTime);
		smooth+=.4f;
	}
	void OnCollisionEnter(Collision collision) {
//        Debug.Log("contact is made");
        foreach (ContactPoint contact in collision.contacts) {
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
//			Debug.Log("Collided with:" + contact.otherCollider.name);
			switch(contact.otherCollider.name){
			case "SilverCoinDeposit":
				switch(used){
				case false:
					//Debug.Log("Added silver coin:");
				hhManager.givenAmount=hhManager.givenAmount+2;
				DestroyObject(gameObject);
					used = true;
					break;
					
				}
					
				
				break;
			}
		
        }
	}
}
