using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class cdTimer : MonoBehaviour {
    public DisplayManager displayManager;
    // Use this for initialization
    public TextMeshPro TimeRemainingTM;
    Color origColor;
    private int hr;
    private int minute;
    private int second;
    private int sceneID;
    private int isHrOdd;
    private bool active;
    string[] times;
    public AudioClip[] audioClips;
    bool blink;
    public bool playAudio;
    private int TimerType;
    private int SecondsToHorn;
    public TweenPosition currentObjectTP;
    public TweenScale currentObjectTS;
    public TweenScale nameOdoTS;
    public TweenPosition nameOdoTP;
    private string timeremaining;
    private bool useTextMesh;
    DateTime PayoutTime;
    void Awake()
    {
         displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        if (TimeRemainingTM == null)
        {
            TimeRemainingTM = gameObject.GetComponent<TextMeshPro>();
        }
        
        
    }
	void Start () {
	   
	        origColor = TimeRemainingTM.color;
	    
        
	}
    private void StartTimer()
    {
        active = displayManager.sceneClock.active;
        TimerType=displayManager.sceneClock.TimerType;
        isHrOdd = displayManager.sceneClock.isHrOdd;
        SecondsToHorn = displayManager.sceneClock.SecondsToHorn;
        /*
        sceneID = SceneManager.GetActiveScene().buildIndex;
        switch (sceneID)
        {
            case 8:
                active = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].TimerActive;
                TimerType = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].TimerType;
                isHrOdd = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].isOddHr;
                SecondsToHorn = displayManager.displayInfo.prizeEvents[displayManager.currentPrizeEventID].secondsToHorn;
                break;
            case 13:
                active = displayManager.displayInfo.matchMadness.timerActive;
                TimerType = 4;
                SecondsToHorn = 0;
                break;
            default:
                active = displayManager.currentScene.highHandData.active;
                TimerType = displayManager.currentScene.highHandData.sessionTimer;
                isHrOdd = displayManager.currentScene.highHandData.isOdd;
                SecondsToHorn = displayManager.currentScene.highHandData.secondstohorn;
                break;
        }
        */
        switch (TimerType)
        {

            case 0:
                hourlyTimerUpdate();
                break;
            case 1:
                twohourlyTimerUpdate();
                break;
            case 2:
                fifteenMinuteUpdate();
                break;
            case 3:
                thirtyMinuteUpdate();
                break;
            case 4:
                TimeDifference();
                break;
        }

           
        
    }

    private void hourlyTimerUpdate()
    {
        if (active)
        {
            times = displayManager.currentTime.ToString("h:mm:ss").Split(':');
            //string[] times = System.DateTime.Now.ToString("h:mm:ss").Split(':');
            int tr = 59 - int.Parse(times[1]);
            int sr = 59 - int.Parse(times[2]);
            minute = tr;
            second = sr;
            int hr = int.Parse(times[0]);
            string srt = sr.ToString();
            string trt = tr.ToString();
            string hrnum = "";
            hrnum = "";
           
            if (sr.ToString().Length < 2)
                srt = "0" + sr;
            if (tr.ToString().Length < 2)
                trt = "0" + tr;


            setTimeRemaining(hrnum + trt.ToString() + ":" + srt);
            
            if (sr < 11 && tr == 0 && IsOdd(hr))
            {
                blink = !blink;
                if (blink)
                {
                    //TimeRemaining.color = Color.red;
                }
                else
                {
                    //TimeRemaining.color = Color.white;
                }
                //ActivateTweening(false);
            }
            else
            {
                //TimeRemaining.color = origColor;
                //ActivateTweening(true);
            }
        }
        else
        {
            setTimeRemaining("--:--");
        }
    }

    private void setTimeRemaining(string v)
    {
      TimeRemainingTM.text = v;
        //Debug.Log("time: " + v);
    }
    private void setTimeRemaining(TimeSpan v)
    {
        if (active)
        {
            TimeRemainingTM.text = v.ToString();
            int hornAt = 7200;
            if (SecondsToHorn != 0)
            {
                hornAt = SecondsToHorn;
            }
            if (v.TotalSeconds == hornAt)
            {
                if (playAudio)
                {
                    playSound(0);
                }
                playAudio = false;
            }
            else
            {
                playAudio = true;
            }
        }else
        {
            TimeRemainingTM.text = "--:--";
            //Destroy(transform.parent.gameObject);
        }
    }

    private void TimeDifference()
    {
        /*
        DateTime t1 = Convert.ToDateTime(displayManager.displayInfo.matchMadness.buzzerTimer);
        DateTime t2 = Convert.ToDateTime(displayManager.currentTime);
        TimeSpan ts = t1.Subtract(t2);
        if (ts.ToString().Contains("-"))
        {
            active = false;
            TimeRemaining.text = "--:--";
        }else {
            active = true;
        TimeRemaining.text = ts.ToString();
        times = ts.ToString().Split(':');
            if (times[0].Contains("."))
            {
                string[] tm = times[0].Split('.');
                hr = int.Parse(tm[1]);
            }else
            {
                hr = int.Parse(times[0]);
            }
            
            minute = int.Parse(times[1]);
            second = int.Parse(times[2]);
        }
        */
    }

    private void thirtyMinuteUpdate()
    {
        if (active)
        {
            times = displayManager.currentTime.ToString("h:mm:ss").Split(':');
            //times = System.DateTime.Now.ToString("h:mm:ss").Split(':');
            minute = 59 - int.Parse(times[1]);
            second = 59 - int.Parse(times[2]);
            hr = 0;
            if (minute >= 0 && minute <= 29)
            {
                minute = minute - 0;

            }
            if (minute >= 30 && minute <= 59)
            {
                minute = minute - 30;

            }
            string mnt = minute.ToString();
            string scd = second.ToString();
            string color = "ffffff";
            if (minute.ToString().Length < 2)
                mnt = "0" + minute;
            if (second.ToString().Length < 2)
                scd = "0" + second;
            
                if (IsOdd(second) && minute == 0 && second < 10)
            {
                //Debug.Log("Is Less");
                color = "ff0000";
                
            }
            TimeRemainingTM.color = SkinSettings.HexToColor(color);
            setTimeRemaining(mnt.ToString() + ":" + scd.ToString());
        }
        else
        {
            setTimeRemaining("--:--");
        }
    }

    private void ActivateTweening(bool Reverse)
    {
        switch (Reverse)
        {
            case true:
                currentObjectTP.PlayReverse();
                currentObjectTS.PlayReverse();
                nameOdoTP.PlayReverse();
                nameOdoTS.PlayReverse();
                break;
            case false:
                currentObjectTP.PlayForward();
                currentObjectTS.PlayForward();
                nameOdoTP.PlayForward();
                nameOdoTS.PlayForward();
                break;
        }

    }

    private void fifteenMinuteUpdate()
    {
        if (active)
        {
            times = displayManager.currentTime.ToString("h:mm:ss").Split(':');
            //times = System.DateTime.Now.ToString("h:mm:ss").Split(':');
            minute = 59 - int.Parse(times[1]);
            second = 59 - int.Parse(times[2]);
            hr = 0;
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
            string color = "ffffff";
            if (minute.ToString().Length < 2)
                mnt = "0" + minute;
            if (second.ToString().Length < 2)
                scd = "0" + second;
            if (IsOdd(second) && minute == 0 && second < 10)
            {
                //Debug.Log("Is Less");
                color = "ff0000";
            }
            TimeRemainingTM.color = SkinSettings.HexToColor(color);
            setTimeRemaining(mnt.ToString() + ":" + scd.ToString());
        }
        else
        {
            setTimeRemaining("--:--");
        }
    }
    void playSound(int clipID)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[clipID]);
        Debug.Log("Played");
    }
    private void twohourlyTimerUpdate()
    {
        //times = displayManager.currentTime.ToString("h:mm:ss").Split(':');
        DateTime currentTime = System.DateTime.Now;
        string[] times = currentTime.ToString("h:mm:ss").Split(':');
        string hr = times[0];
        if (isHrOdd == 1)
        {
            if (!IsOdd(int.Parse(hr)))
            {
                PayoutTime = currentTime.AddHours(+1).AddMinutes(-int.Parse(times[1])).AddSeconds(-int.Parse(times[2]));
              
            }
            else
            {
                PayoutTime = currentTime.AddHours(+2).AddMinutes(-int.Parse(times[1])).AddSeconds(-int.Parse(times[2]));
            }
            
        }else{
            if (IsOdd(int.Parse(hr)))
            {
                PayoutTime = currentTime.AddHours(2).AddMinutes(-int.Parse(times[1])).AddSeconds(-int.Parse(times[2]));
            }
            else
            {
                PayoutTime = currentTime.AddHours(+1).AddMinutes(-int.Parse(times[1])).AddSeconds(-int.Parse(times[2]));
            }
        }
        TimeSpan duration = PayoutTime - currentTime;
        //Debug.Log("Left in Seconds:" +duration.TotalSeconds);
        setTimeRemaining(duration);
    }
    /*
    private void twohourlyTimerUpdate()
    {
        if (active)
        {
            //times = displayManager.currentTime.ToString("h:mm:ss").Split(':');
            string[] times = System.DateTime.Now.ToString("h:mm:ss").Split(':');
            int tr = 59 - int.Parse(times[1]);
            int sr = 59 - int.Parse(times[2]);
            minute = tr;
            second = sr;
            int hr = int.Parse(times[0]);
            string srt = sr.ToString();
            string trt = tr.ToString();
            string hrnum = "";
            if (isHrOdd == 1)
            {
                if (IsOdd(hr + 1))
                {

                    hrnum = "";
                    hr = 0;
                }
                else
                {
                    hrnum = "1:";
                    hr = 1;
                }
            }
            else
            {
                if (IsOdd(hr))
                {

                    hrnum = "1:";
                    hr = 1;
                }
                else
                {
                    hrnum = "";
                    hr = 0;
                }
            }
            if (TimerType == 0)
            {
                hrnum = "";
            }
            if (sr.ToString().Length < 2)
                srt = "0" + sr;
            if (tr.ToString().Length < 2)
                trt = "0" + tr;



            setTimeRemaining(hrnum + trt.ToString() + ":" + srt);
            if (sr < 11 && tr == 0 && IsOdd(hr))
            {
                blink = !blink;
                if (blink)
                {
                    TimeRemainingTM.faceColor = Color.red;
                }
                else
                {
                    TimeRemainingTM.faceColor = Color.white;
                }
                //ActivateTweening(false);
            }
            else
            {
                TimeRemainingTM.faceColor = origColor;
                //ActivateTweening(true);
            }
        }
        else
        {
            setTimeRemaining("--:--");
        }
    }
    */
    public bool IsOdd(int value)
    {
        bool isOdd = false;
        
        if (isHrOdd ==1)
        {
            isOdd= value % 2 != 0;
        }
        else
       {
            isOdd = value % 2 == 0;
        }
        return isOdd;
    }
    
	// Update is called once per frame
	void Update () {
        
        StartTimer();
        if (hr == 0 && minute == 0 && second==30)
        {
            //ActivateTweening(false);
        }
        if (second == 00)
        {
            //ActivateTweening(true);
        }

    }
}
