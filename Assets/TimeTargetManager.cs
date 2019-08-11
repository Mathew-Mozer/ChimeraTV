using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnitySampleAssets.Utility;
using UnityEngine.SceneManagement;

public class TimeTargetManager : MonoBehaviour
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
    public List<UISprite> cards;
    public List<GameObject> Layouts;
    private int listIndex;
    // Use this for initialization
    void Start()
    {
        
        Debug.Log("SceneID:" + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 15)
        {
            TimeTargetXInitialize();
        }else
        {
            amt = (int)(Math.Round(PerMinuteIncrease, 2) * 100);
            StartCoroutine(StartCounting());
            updateTimeTarget();
        }
        
    }

    private void TimeTargetXInitialize()
    {
        
        foreach(GameObject go in Layouts)
        {
            go.SetActive(false);
        }
        switch (DisplayManager.displayManager.currentScene.timeTargetXData.TimeTargetData.Count){
            case 4:
                Layouts[0].SetActive(true);
                listIndex = 0;
                break;
            case 13:
                Layouts[1].SetActive(true);
                listIndex = 1;
                break;
            case 6:
                Layouts[2].SetActive(true);
                listIndex = 2;
                break;
            default:
                Layouts[3].SetActive(true);
                listIndex = 2;
                break;
        }
    }

    private void updateTimeTarget()
    {
        timeTarget = DisplayManager.displayManager.currentScene.timeTargetData;
        PerSecondIncrease = timeTarget.add / (timeTarget.min * 60);
        string[] cardsStrings = timeTarget.cards.Split(',');
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].spriteName = cardsStrings[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayManager.displayManager.updateSceneDataNow)
        {
            DisplayManager.displayManager.updateSceneDataNow = false;
            updateTimeTarget();
        }

    }

    IEnumerator StartCounting()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            double sd = timeTarget.seed*100;
            double incAmt = sd + Math.Round((SecondCount() * PerSecondIncrease)*100, 0, MidpointRounding.AwayFromZero);

            if (incAmt > 0)
            {
                Odo.SetValue((int)incAmt);
            }
            else
            {
                Odo.SetValue(0);
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
