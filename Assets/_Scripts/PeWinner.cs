using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class PeWinner
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public string prize;
    [XmlAttribute]
    public string ptype;
    [XmlAttribute]
    public string lefticon;
    [XmlAttribute]
    public string righticon;


}