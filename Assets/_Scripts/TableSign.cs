using UnityEngine;
using System.Collections;

public class TableSign : MonoBehaviour {
    public UILabel min;
    public UILabel max;
    public UILabel agr;
	// Use this for initialization
	void Start () {
        DisplayManager displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        //displayManager.tablesign = transform.gameObject;
        //setTableSign(displayManager.displayInfo.tableWagers);
    }
	// Update is called once per frame
	void Update () {
	
	}

    internal void setTableSign(TableWagers tableWagers)
    {
        
        min.text = "$"+tableWagers.Min;
        max.text = "$" + tableWagers.Max;
        agr.text = "$" + tableWagers.Aggregate;
        if (agr.text.Length > 5)
        {
            agr.text = agr.text.Insert(agr.text.Length - 3, ",");
        }
    }
}
