using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class SkinElement : ScriptableObject
{
    [XmlAttribute]
    public int id=0;
    [XmlAttribute]
    public string tagname;
    [XmlAttribute]
    public string forecolor;
    [XmlAttribute]
    public string backcolor;
    [XmlAttribute]
    public string bordercolor;
    [XmlAttribute]
    public string textcolor;
    [XmlAttribute]
    public string xCoord;
    [XmlAttribute]
    public string yCoord;
    [XmlAttribute]
    public string width;
    [XmlAttribute]
    public string height;
    [XmlAttribute]
    public string backsprite;
    [XmlAttribute]
    public string foresprite;

}
