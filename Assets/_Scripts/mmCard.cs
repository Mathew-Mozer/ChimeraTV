using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class mmCard
{
    [XmlAttribute]
    public string Card;
    [XmlAttribute]
    public string Value ="";
    [XmlAttribute]
    public string TimeStampStarted = "";
    [XmlAttribute]
    public string TimeStampHit = "";
    public mmCard()
    {

    }
    public mmCard(string c)
    {
        Card = c;
    }
}
