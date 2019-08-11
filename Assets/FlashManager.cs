using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashManager : MonoBehaviour {
    public GameObject FlashPrefab;
    public GameObject DropContainer;
    public float dropInterval;
    public int FlashLimit;
    public int FlashCount;
    private bool continueDrop=true;
	// Use this for initialization
	void Start () {
        StartCoroutine(startDrop());
	}

    private IEnumerator startDrop()
    {

        while (continueDrop)
        {
            yield return new WaitForSeconds(dropInterval);
            if (FlashLimit == 0)
            {
                Debug.Log("FlashLimit==0");
                continueDrop = true;
                DropFlash();
            }
            else
            {
                Debug.Log("FlashLimit!=0");
                if (FlashCount == FlashLimit)
                {
                    continueDrop = false;
                }
                else
                {
                    DropFlash();
                }
            }
        }
    }

    private void DropFlash()
    {
        FlashCount++;
        GameObject currentFlash = NGUITools.AddChild(DropContainer, FlashPrefab);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
