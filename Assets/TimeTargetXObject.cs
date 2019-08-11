using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnitySampleAssets.Utility;

public class TimeTargetXObject: MonoBehaviour
{
    public float perHourIncrease;
    public float PerMinuteIncrease;
    public double PerSecondIncrease;
    private float speed;
    public Odometer Odo;
    private float amt;
    public DateTime currentTime;
    public string startTime;
    public string curTime;
    private DateTime beginTime;
    private TimeTarget timeTarget;
    public UILabel title;
    public UILabel contenttitle;
    public UILabel content;
    public mmLargeCard card;
    public int TimeTargetID;
    public int withChange=1;
    public float payout;
    public bool startedCounting=false;
    public bool cardPayed = false;
    public bool hasDecimal = false;
    // Use this for initialization

    void Awake()
    {
        amt = (int)(Math.Round(PerMinuteIncrease, 2) * withChange);

        if (Odo.maxValue == 9999)
        {
            withChange = 1;
        }
        else
        {
            withChange = 100;
        }
        updateTimeTarget();

    }
    void Start()
    {

    }

    private void updateTimeTarget()
    {
        timeTarget = DisplayManager.displayManager.currentScene.timeTargetXData.TimeTargetData[TimeTargetID] ;
        PerSecondIncrease = timeTarget.add / (timeTarget.min * 60);
        if(title)
        title.text = timeTarget.title;
        if(contenttitle)
        contenttitle.text = timeTarget.contentTitle;
        if(content)
        content.text = timeTarget.content;

        card.setCard(new mmCard(timeTarget.cards),"yo");
        if (!timeTarget.endTime.Contains("0000"))
        {
            Odo.gameObject.SetActive(false);
            cardPayed = true;
        }else
        {
            Odo.gameObject.SetActive(true);
            cardPayed = false;
        }
        card.setPayed(cardPayed);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartCounting());

        if (DisplayManager.displayManager.updateSceneDataNow)
        {
            DisplayManager.displayManager.updateSceneDataNow = false;
            updateTimeTarget();
        }
        
    }

    IEnumerator StartCounting()
    {
        double incAmt;
        if (!startedCounting)
        {
            startedCounting = true;
            while (true)
            {


                startedCounting = true;
                double sd = timeTarget.seed * withChange;
                double mp = timeTarget.MaxPayout * withChange;

                if (timeTarget.progressive == 1)
                {
                    incAmt = sd + Math.Round((SecondCount() * PerSecondIncrease) * 100, 0, MidpointRounding.AwayFromZero);
                    payout = (float)incAmt;
                    if (!hasDecimal)
                    {
                        int payoutDigits = payout.ToString().Length;
                        payout = int.Parse(payout.ToString().Substring(0, payoutDigits - 2));
                        incAmt = sd+ payout;
                        
                    }else
                    {
                        
                    }
                }
                else
                {
                    var tmp = (SecondCount() / 60) / timeTarget.min;
                    incAmt = sd + (tmp * (timeTarget.add * withChange));
                    payout = (float)incAmt;
                   
                }


                if (incAmt > 0)
                {
                    Odo.SetValue((int)incAmt);
                }
                else
                {
                    Odo.SetValue(0);
                }
                if (mp < (int)incAmt)
                {
                    payout = timeTarget.MaxPayout * withChange;
                    Odo.SetValue((int)incAmt);
                    
                }
                else
                {
                    payout = (int)incAmt;
                }

                if ((int)payout < 0)
                {
                    Odo.SetValue(0);
                }
                else
                {
                   
                    Odo.SetValue((int)payout);
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private int SecondCount()
    {
        DateTime endTime;
        if (timeTarget.endTime.Contains("000"))
        {
            endTime = DisplayManager.displayManager.currentTime;
        }
        else
        {
            endTime = Convert.ToDateTime(timeTarget.endTime);
        }
        beginTime = Convert.ToDateTime(timeTarget.startTime);
        TimeSpan ts = -beginTime.Subtract(endTime);
        int totalSeconds = (ts.Days * 86400) + (ts.Hours * 3600) + (ts.Minutes * 60) + ts.Seconds;
        return totalSeconds;

    }
    private void AddToJackpot()
    {


        //speed = 60/amt;
        //Debug.Log("Amount:" + amt);
        //Odo.SetValue((int)PerMinuteIncrease);
    }
}
