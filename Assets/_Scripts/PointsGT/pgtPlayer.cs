using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class pgtPlayer
{
    [XmlAttribute]
    public int trackLocation;
    [XmlAttribute]
    public string PlayerName;
    [XmlAttribute]
    public int Points;
    [XmlAttribute]
    public int carIcon;

    public pgtPlayer(int i1, string tmpName, int tmpPoints, int i2)
    {
        // TODO: Complete member initialization
        this.trackLocation = i1;
        this.PlayerName = tmpName;
        this.Points = tmpPoints;
        this.carIcon = i2;
    }
    public pgtPlayer()
    {
    }
}
