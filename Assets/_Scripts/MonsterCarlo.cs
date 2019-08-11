using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
public class MonsterCarlo :ScriptableObject
{
    public int id;
    public int active;
    public int session;
    public string Payouts;
    public highHand HighHandSettings;
    public List<string> cardList = new List<string>();
}