using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
[Serializable]
public  class pgtInstantWinner
{
    [XmlAttribute]
    public string PrizeAmount;
    [XmlAttribute]
    public int PointAmount;
    [XmlAttribute]
    public string IconColor;
}