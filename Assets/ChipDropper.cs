using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDropper : MonoBehaviour {

    public GameObject BlackChip;
    public GameObject GreenChip;
    public GameObject RedChip;
    public GameObject WhiteChip;
    public float dropVariance;
    public List<Vector3> DropLocations;
    public float dropSpeed;


    // Use this for initialization
    void Start () {
        
	}

    private IEnumerator StartDrop()
    {
        int cnt = 0;
        while (true)
        {
            GameObject chipToDrop = new GameObject();
            int chip = (int)UnityEngine.Random.Range(0, 3);
            float droplocation = (float)UnityEngine.Random.Range(-1.8f, 1.8f);
            switch (chip)
            {
                case 0:
                    chipToDrop = BlackChip;
                    break;
                case 1:
                    chipToDrop = GreenChip;
                    break;
                case 2:
                    chipToDrop = RedChip;
                    break;
                case 3:
                    chipToDrop = WhiteChip;
                    break;
            }
            GameObject newChip=Instantiate(chipToDrop);
            newChip.GetComponent<rotateobject>().killDelay();
            newChip.transform.parent = gameObject.transform;
            newChip.transform.position = new Vector3(droplocation,2,0);
            yield return new WaitForSeconds(.5f);
            cnt++;
        }
    }
  
    // Update is called once per frame
    void Update () {
		
	}

    public void StartTheDrop()
    {
        StartCoroutine(StartDrop());
    }
}
