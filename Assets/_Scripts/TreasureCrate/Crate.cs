using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
[Serializable]
public class Crate
{
    [XmlAttribute]
    public int crateID;
    [XmlAttribute]
    public int value;
    [XmlAttribute]
    public bool isOpen;
    [XmlAttribute]
    public int crateType;

    public Crate()
    {

    }
    public Crate(int crateID, int value, bool isOpen, int extraPicks)
    {
        // TODO: Complete member initialization
        this.crateID = crateID;
        this.value = value;
        this.isOpen = isOpen;
        this.crateType = extraPicks;
    }
}