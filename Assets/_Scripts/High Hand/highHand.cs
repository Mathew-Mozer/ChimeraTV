using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class highHand : ScriptableObject
{
    
    public int session = 0;
    public int cards = 0;
    public bool hasrows = false;
    //public List<Hand> previousHands = new List<Hand>();
    public List<string> GoldCards = new List<string>();
    public string givenAmount;
    public int paytype;
    public int handcount;
    public string payouts;
    public string TitleMsg;
    public int isOdd;
    public int includeTime; //0=no,1 = Minute only,2=hour only,3=Hour and Minute
    public int includeTable; //0=no,1 = Yes
    public int secondstohorn; //How many seconds left before horn plays
    public int HandListType;//Show previous winners or show current non winners (ex 1st,2nd 3rd)
    //Json additions
    public List<Hand> HandList = new List<Hand>();
    public int sessionTimer;
    public int LockToTime;
}
