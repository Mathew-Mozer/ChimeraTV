using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.IO;

[Serializable]
public class TimeTarget
{
    public int id;
    public float seed;
    public string startTime;
    public string endTime;
    public int min;
    public float add;
    public string title;
    public string contentTitle;
    public string content;
    public string cards;
    public int MaxPayout;
    public int progressive;
}