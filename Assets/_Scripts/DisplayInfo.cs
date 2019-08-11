using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
[Serializable]
public class DisplayInfo : ScriptableObject
{
    [XmlArray]
    public List<MonteCarloBoard> MonteCarloBoards = new List<MonteCarloBoard>();
    [XmlAttribute]
    string LinkCode;
    public string linkCode { get; set; }
    [XmlAttribute]
    public int displayType;
    [XmlAttribute]
    public int casinoID;
    [XmlAttribute]
    public string DisplayName { get; set; }
    public List<scene> scenes = new List<scene>();
    [XmlAttribute]
    public int DisplayMode = 1;
    public TableWagers tableWagers;
    [XmlAttribute]
    public string AppVersion { get; set; }
    [XmlAttribute]
    public string BundleAndroidUrl;
    [XmlAttribute]
    public string BundleWindowsURL;
    [XmlAttribute]
    public string BundleVer = "1";
    [XmlAttribute]
    public string AssetName;
    [XmlAttribute]
    public string DefaultLogo;
    [XmlAttribute]
    public int lockedScene = 0;
    [XmlAttribute]
    public string lastUpdated;
    public List<string> svrErrors = new List<string>();
    public List<PGTSession> pgtSessions = new List<PGTSession>();
    public List<PrizeEvent> prizeEvents = new List<PrizeEvent>();
    public List<PictureData> PictureList = new List<PictureData>();
    public MatchMadness matchMadness=new MatchMadness();
    public KickForCash kickForCash=new KickForCash();
}