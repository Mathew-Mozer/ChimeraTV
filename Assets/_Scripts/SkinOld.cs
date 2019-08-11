using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
[XmlRoot]
public class SkinOld
{
    [XmlAttribute]
    public int id=0;
    [XmlAttribute]
    public string name = "";
    [XmlAttribute]
    public int HighHandTopLeftTitle = 0;
    [XmlAttribute]
    public int HighHandTopLeftContent = 0;
    [XmlAttribute]
    public int HighHandTopRightTitle = 0;
    [XmlAttribute]
    public int HighHandTopRightContent = 0;
    [XmlAttribute]
    public int HighHandPrize = 0;
    [XmlAttribute]
    public int HighHandName = 0;
    [XmlAttribute]
    public int HighHandScene;
    [XmlAttribute]
    public int HighHandBoardScene;
    [XmlAttribute]
    public int HighHandPrevHand;
    [XmlAttribute]
    public int HighHandPrevBox;


    public int getSkinElementID(String skinTag){
        int tmp=0;
        //tmp=(int)this.GetType().GetField(skinTag).GetValue(this);
        return tmp;
    }
}

