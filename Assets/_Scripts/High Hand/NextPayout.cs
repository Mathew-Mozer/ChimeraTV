using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class NextPayout : MonoBehaviour {
    DisplayManager displayManager;
    private int minute;
    private int second;
    private int Hour;
    private int sceneID;
    private bool active;
    public int TimerType;
    private int isHrOdd;
    void Awake()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        


    }
	// Use this for initialization
	void Update () {
        string[] times = DisplayManager.displayManager.currentTime.ToString("h:mm:ss").Split(':');
	    minute = int.Parse(times[1]);
        second = int.Parse(times[2]);
	    Hour = int.Parse(times[0]);
        active = displayManager.sceneClock.active;
        TimerType = displayManager.sceneClock.TimerType;
        isHrOdd = displayManager.sceneClock.isHrOdd;
        gameObject.GetComponent<TextMeshPro>().text = getNextTime();

        /*
                sceneID = SceneManager.GetActiveScene().buildIndex;
                switch (sceneID)
                {
                    case 8:
                        active = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].TimerActive;
                        TimerType = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].TimerType;
                        isHrOdd = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].isOddHr;
                        break;
                    default:
                        active = displayManager.currentScene.highHandData.active;
                        TimerType = displayManager.currentScene.highHandData.Hours;
                        isHrOdd = displayManager.currentScene.highHandData.isOdd;
                        break;
                }

            */
    }

    private string getNextTime()
    {
        string returnTime = "--:--";
        if (active)
        {
            switch (TimerType)
            {
                case 0:
                    returnTime = SixtyMinuteClock();
                    break;
                case 1:
                    returnTime = TwoHourClock();
                    break;
                case 2:
                    returnTime=FifteenMinuteClock();
                    break;
                case 3:
                    returnTime = ThirtyMinuteClock();
                    break;
            }
        }
        
        return returnTime;
    }

    private string TwoHourClock()
    {
        string rMinute = "00";
        string rHour="";
        if (isHrOdd == 1)
        {
            if (IsOdd(Hour))
            {
                rHour = "" + (Hour + 2);
            }
            else
            {
                rHour = "" + (Hour + 1);
            }
            
        }
        else
        {
            if (IsOdd(Hour))
            {
                rHour = "" + (Hour + 1);
            }
            else
            {
                rHour = "" + (Hour + 2);
            }
        }
        
        if (rHour.Equals("13"))
        {
            rHour = "1";
        }
        return rHour + ":" + rMinute;
    }

    private string SixtyMinuteClock()
    {
        
        string rMinute = "00";
        string rHour = "" + (Hour+1);
        if (rHour.Equals("13"))
        {
            rHour = "1";
        }
        return rHour + ":" + rMinute;
    }

    private string ThirtyMinuteClock()
    {
        string rMinute = "--";
        string rHour = Hour.ToString();
        if (minute >= 0 && minute <= 29)
        {
            rMinute = "30";

        }
        if (minute >= 31 && minute <= 59)
        {
            rMinute = "00";
            rHour = "" + (Hour + 1);
            if (rHour == "13")
            {
                rHour = "1";
            }
        }
        
        
        


        return rHour + ":" + rMinute;
    }

    private string getOddhr()
    {
        
            if (active)
            {
                string[] times = DisplayManager.displayManager.currentTime.ToString("h:mm:ss").Split(':');
                int tr = 59 - int.Parse(times[1]);
                int sr = 59 - int.Parse(times[2]);
                int hr = int.Parse(times[0]);//11
            int hrmod = 0;
            string minmod = "00";

                
            if (TimerType == 2)
            {
                if (minute >= 0 && minute <= 14)
                {
                    minute = minute - 0;

                }
                if (minute >= 15 && minute <= 29)
                {
                    minute = minute - 15;

                }
                if (minute >= 30 && minute <= 44)
                {
                    minute = minute - 30;

                }
                if (minute >= 45 && minute <= 59)
                {
                    minute = minute - 45;

                }
                string mnt = minute.ToString();
                string scd = second.ToString();
            }
            else
            {
                hrmod = 1;
            }
                int curPayHour = hr+hrmod;//12
                if (isHrOdd == 1)
                {
                    if (IsOdd(hr))
                    {
                        
                        curPayHour = curPayHour + TimerType;
                        
                    }
                    else
                    {
                        //Debug.Log("2");
                        //curPayHour= curPayHour+displayManager.TheHand.Hours;
                        
                    }
                }
                else
                {
                    if (!IsOdd(hr))//11
                    {
                        //Debug.Log("3");
                        curPayHour= curPayHour+ TimerType;
                    }
                    else
                    {
                       // Debug.Log("4");
                        //curPayHour++;
                    }
                }
                
                if (curPayHour>12)
                {
                    curPayHour = curPayHour- 12;
                }

                return curPayHour.ToString() + ":" + minmod;
            }
            else
            {
                return "--:--";
            }
        
        
    }

    private string FifteenMinuteClock()
    {
        string rMinute="--";
        string rHour=Hour.ToString();
        if (minute >= 0 && minute <= 14)
        {
            rMinute = "15";

        }
        if (minute >= 15 && minute <= 29)
        {
            rMinute = "30";

        }
        if (minute >= 30 && minute <= 44)
        {
            rMinute = "45";

        }
        if (minute >= 45 && minute <= 59)
        {
            rMinute = "00";
            rHour = "" + (Hour + 1);
            if (rHour == "13")
            {
                rHour = "1";
            }
        }
        
        
        return rHour +":"+ rMinute;
    }

    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
}
