using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class PrizeEvent
{
    
    public int ID;
    public List<PeWinner> winnerlist = new List<PeWinner>();
    public int JackPotAmount;
    public string Title;
    public int RandomPrize;
    public int IncrementNumber;
    public int TimerType;
    public int TimerActive;
    public int clockRemainingVisible;
    public int NextTimeVisible;
    public int isOddHr;
    public int secondsToHorn;
}