using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

public class Hand : ScriptableObject{
    [XmlAttribute("fname")]
    public string fname;
    [XmlAttribute("handID")]
    public string handID="0";
    [XmlArrayAttribute("hand")]
    public string[] hand;
    [XmlAttribute("timestamp")]
    public string timestamp;
    [XmlAttribute]
    public int handRank;

}
