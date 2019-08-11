using UnityEngine;
using System.Collections;
using System;
public class TC_Session: ScriptableObject
{
    public int tcID;
    public string PlayerName;
    public string StartTime;
    public bool Completed = false;
    public string PayTier;
    public int PayTierID;
    public CrateHolder crateHolder = new CrateHolder();
    public void LoadCrates(string cratevalues)
    {

    }
}
