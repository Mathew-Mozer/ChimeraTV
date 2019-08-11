using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
[Serializable]
public class TableWagers : ScriptableObject
{
 [XmlAttribute]
    public int Min;
    [XmlAttribute]
    public int Max;
    [XmlAttribute]
    public int Aggregate;
}