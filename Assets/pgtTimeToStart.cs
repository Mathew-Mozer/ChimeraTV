using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pgtTimeToStart : MonoBehaviour {
    public TextMeshPro TimeRemainingTM;
    public TextMeshPro TotalPayouts;
    int TotalPayout;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (TimeRemainingTM)
        {
            TotalPayout = 0;
String[] pays = DisplayManager.displayManager.currentScene.pointsGTData.PayoutList.Split(',');
        foreach(String pay in pays)
        {
            TotalPayout += int.Parse(pay.Substring(1, pay.Length - 1));
        }
        TotalPayouts.text = String.Format("${0:n0}", TotalPayout);

            TimeSpan span = (DisplayManager.displayManager.currentScene.pointsGTData.StartDate - DateTime.Now);
            if (span.Days > 0)
            {
                TimeRemainingTM.text = String.Format("Starts in: \r\n{0} days, {1}:{2}:{3}",
                span.Days, span.Hours,doDouble(span.Minutes),doDouble(span.Seconds));
            }
            else
            {
                TimeRemainingTM.text = String.Format("Starts in: \r\n{1}:{2}:{3}",
                span.Days, span.Hours, doDouble(span.Minutes), doDouble(span.Seconds));
            }

            if (span.TotalMilliseconds < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private string doDouble(int mins)
    {
        if (mins<10)
        {
            return "0" + mins;
        }
        else{
            return mins.ToString();
        }
    }
}
