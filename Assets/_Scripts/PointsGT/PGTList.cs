using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class PGTList
{
    [XmlArray]
    public List<pgtPlayer> PointsGTPlayers = new List<pgtPlayer>();
    [XmlArray]
    public List<pgtInstantWinner> InstantWinners = new List<pgtInstantWinner>();
    public pgtPlayer getCarByTrackLocation(int TrackLocation)
    {
        pgtPlayer tmpPlayer = PointsGTPlayers.Find(r => r.trackLocation == TrackLocation+1);
        return tmpPlayer;
    }
    public void RandomizeTrack(){
        PointsGTPlayers.Sort((a,b) => a.Points.CompareTo(b.Points));
        PointsGTPlayers.Reverse();
    }
}
