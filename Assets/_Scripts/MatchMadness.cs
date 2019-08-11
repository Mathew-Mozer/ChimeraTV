using System;
using System.Xml.Serialization;
using System.Collections.Generic;
[Serializable]
public class MatchMadness
{
    public List<mmCard> cardList;
    public string lastUpdated="";
    public DateTime buzzerTimer;
    public bool timerActive;
}