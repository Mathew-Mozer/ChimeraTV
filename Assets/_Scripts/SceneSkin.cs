using System;
using System.Collections.Generic;
using System.Xml.Serialization;
[Serializable]
public class SceneSkin
{
    [XmlAttribute]
    public int SceneID;
    public int skinID;
    public List<SkinElement> skinData = new List<SkinElement>();
    private int v;
    public string skinName;

    public SceneSkin(int v, List<SkinElement> skinElements)
    {
        SceneID = v;
        this.skinData = skinElements;
    }
    public SceneSkin()
    {
    }
}