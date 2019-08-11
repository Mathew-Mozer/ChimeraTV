using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class scene
{
    public int promoID=0;
    public int sceneID;
    public int duration;
    public int sceneDuration;
    public int promotionStatus;
    public bool active;
    public int EffectID;
    public int handcount;
    public bool animation=false;
    public int _sceneType;
    public SceneSkin sceneSkin = new SceneSkin();
    public string lastUpdated="";
    public KickForCash kickForCashData;
    public PGTSession pointsGTData;
    public DisplayList DisplayListData;
    public TimeTarget timeTargetData;
    public TimeTargetX timeTargetXData;
    public highHand highHandData;
    public MonsterCarlo monsterCarloData;
    public MatchMadness MatchMadnessData;
    public PrizeEvent PrizeEventData;
    public PictureViewer PictureViewerData;


    public scene()
    {
     
    }

    public void setActive()
    {
        if (promotionStatus == 1)
            active = true;
        if (promotionStatus == 2)
            active = false;
    }
    public int sceneType
    {
        get { return _sceneType; }
        set { _sceneType = value;
            switch (_sceneType)
            {
                case 4:
                    break;
                case 11:
                    break;
            }
        }
    }
    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }
}

