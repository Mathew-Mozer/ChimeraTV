using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayListManager : MonoBehaviour {
    private DisplayManager displayManager;
    public float nameDropDelay;
    public GameObject NameGrid;
    public GameObject NamePrefab;
    public UILabel MainTextTitle;
    public UILabel MainText;
    public bool namesdone = false;
    // Use this for initialization
    void Awake () {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        MainText.text = displayManager.currentScene.DisplayListData.text1;
        MainTextTitle.text = displayManager.currentScene.DisplayListData.text1title;
        StartCoroutine(StartNameDrop());
    }

    private IEnumerator StartNameDrop()
    {
        
        int count = 0;
        while (count!= displayManager.currentScene.DisplayListData.ListData.Count)
        {
            Debug.Log("NEW ITEM DROPPED!");
            GameObject tmp = NGUITools.AddChild(NameGrid, NamePrefab);
            tmp.GetComponent<NameDropListItem>().NameLabel.GetComponent<UILabel>().text = displayManager.currentScene.DisplayListData.ListData[count].Name;
            tmp.transform.localScale = new Vector3(.1f, .1f);
            count++;

            yield return new WaitForSeconds(nameDropDelay);
            if (count == 50)
            {
                namesdone = true;
            }
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
