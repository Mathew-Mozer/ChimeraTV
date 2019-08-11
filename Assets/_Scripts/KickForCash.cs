using System;
using System.Xml.Serialization;
[Serializable]
public class KickForCash
{
    [XmlAttribute]
    public int jackpotAmount;
    [XmlAttribute]
    public int selectedBall; //Ball Player selected or first winning ball
    [XmlAttribute]
    public int winningBall; //Ball Player should have selected
    [XmlAttribute]
    public short fieldPosition;
    [XmlAttribute]
    public string playerName;
    [XmlAttribute]
    public string peTeam1;
    [XmlAttribute]
    public string peTeam2;
    [XmlAttribute]
    public string peatsign;
    [XmlAttribute]
    public string peLabel;
}