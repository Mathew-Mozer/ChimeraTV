using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.IO;

[Serializable]
public class PGTSession
{

    public string Value1;
    public string Value2;
    public string Value3;
    [XmlAttribute]
    public string Value3Title;
    [XmlAttribute]
    public string Value2Title;
    [XmlAttribute]
    public int DayOfSession;
    [XmlAttribute]
    public int DaysInSession;
    [XmlAttribute]
    public string title;
    [XmlAttribute]
    public string timestamp;
    [XmlAttribute]
    //other
    public bool finished;
    [XmlAttribute]
    public string PayoutList="";
    public PGTList playerlist = new PGTList();
    public List<pgtInstantWinner> InstantWinners = new List<pgtInstantWinner>();
    public List<pgtPlayer> playerListJson = new List<pgtPlayer>();
    public int SpriteAtlas;
    public DateTime StartDate;

    public PGTSession(string val1, string val2, string timestamp, string data)
    {
        // TODO: Complete member initialization
        this.Value1 = val1;
        this.Value2 = val2;
        this.timestamp = timestamp;
        //this.playerlist = deserializePlayers(data);
    }

    private PGTList deserializePlayers(string XMLData)
    {
        PGTList tempCrateHolder = new PGTList();
        XmlSerializer Xml_Serializer = new XmlSerializer(typeof(PGTList));
        using (StringReader reader = new StringReader(XMLData))
        {
            tempCrateHolder = (PGTList)Xml_Serializer.Deserialize(reader);
        }
        return tempCrateHolder;
    }
    public PGTSession()
    {

    }

    public List<pgtPlayer> sortlist(List<pgtPlayer> tmp)
    {
        tmp.Sort((a, b) => a.Points.CompareTo(b.Points));
        tmp.Reverse();
        return tmp;
    }
    
}
