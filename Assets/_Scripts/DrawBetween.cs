using UnityEngine;
using System.Collections;

public class DrawBetween : MonoBehaviour {
    public Transform TargetObject;
    public GameObject lineSprite;
    public float MaxDistance;
    public float MaxWidth;
    public float tmpPerc;
    public float tmp;
    public int tmp2;
    public float dist;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*if (TargetObject)
        {
            Vector3 obj1 = TargetObject.transform.position;
            Vector3 obj2 = transform.position;
            float tmp3 = obj1.y-obj2.y;
            lineSprite.transform.position.Set(transform.position.x, tmp3, transform.position.z);
            dist = Vector3.Distance(TargetObject.position, transform.position);
            tmpPerc = dist / MaxDistance ;
            tmp = MaxWidth * tmpPerc;
            tmp2 = (int)tmp;
            print("tmp2:"+ tmp2);
            if (lineSprite)
            {
                lineSprite.GetComponent<UISprite>().width = tmp2;
            }
        }
         */
	}
}
