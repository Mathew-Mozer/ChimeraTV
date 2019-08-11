using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
[XmlRoot]
public class SkinElements
{
    public List<SkinElement> skinElements = new List<SkinElement>();
}


