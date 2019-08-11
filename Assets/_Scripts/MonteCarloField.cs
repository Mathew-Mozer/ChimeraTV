using System;
using System.Collections.Generic;
using System.Xml.Serialization;
[Serializable]
public class MonteCarloField
{
    [XmlAttribute]
    public string DisplayedName = "";
    [XmlAttribute]
    public string FieldName = "";
    
    public List<string> FieldValues = new List<string>();
    [XmlAttribute]
    public int gridY = 0;
    [XmlAttribute]
    public int gridX = 0;
    [XmlAttribute]
    public int gridSize = 1;
}