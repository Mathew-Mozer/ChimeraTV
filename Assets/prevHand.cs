using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class prevHand : MonoBehaviour {
    public Hand theHand;
    public TextMeshPro nameLabel;
    public TextMeshPro timeLabel;
    public List<UISprite> cards = new List<UISprite>();
    // Use this for initialization
    void Start () {
        theHand = new Hand();
        nameLabel.text = "You could be next!";
        GameObject skin = GameObject.FindGameObjectWithTag("Skin");
        cards[0].atlas = skin.GetComponent<UIAtlas>();
        cards[1].atlas = skin.GetComponent<UIAtlas>();
        cards[2].atlas = skin.GetComponent<UIAtlas>();
        cards[3].atlas = skin.GetComponent<UIAtlas>();
        cards[4].atlas = skin.GetComponent<UIAtlas>();
        cards[0].spriteName = "CB";
        cards[1].spriteName = "CB";
        cards[2].spriteName = "CB";
        cards[3].spriteName = "CB";
        cards[4].spriteName = "CB";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void updateHand(Hand theHand, UIAtlas CardAtlas,DisplayManager dm)
    {
        this.theHand = theHand;
        nameLabel.text = theHand.fname;
        if (dm.currentScene.highHandData.includeTime==1)
        {
            timeLabel.text =":"+ winningMinute(theHand.timestamp,dm.currentScene.highHandData.sessionTimer);
        }
        cards[0].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[1].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[2].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[3].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[4].atlas = CardAtlas.GetComponent<UIAtlas>();
        //deprecated
        //cards[5].atlas = CardAtlas.GetComponent<UIAtlas>();
        //cards[6].atlas = CardAtlas.GetComponent<UIAtlas>();

        cards[0].spriteName = theHand.hand[0];
        cards[1].spriteName = theHand.hand[1];
        cards[2].spriteName = theHand.hand[2];
        cards[3].spriteName = theHand.hand[3];
        cards[4].spriteName = theHand.hand[4];
    }

    internal void visible(Hand hand)
    {
        gameObject.SetActive(hand);
    }
    internal void visible(bool hand)
    {
        gameObject.SetActive(hand);
    }
    public string winningMinute(string timestamp,int timerType)
    {
        string[] st = timestamp.Split(' ');
        string[] tm = st[1].Split(':');
        //            DateTime date = new DateTime(int.Parse(dt[0]),int.Parse(dt[1]),int.Parse(dt[2]),int.Parse(tm[0]),int.Parse(tm[1]),int.Parse(tm[2]));
        int minute = int.Parse(tm[1]);
        int hour = int.Parse(tm[1]);
        int displMin = 0;
        switch (timerType)
        {
            case 3:
                if (minute >= 0 && minute <= 29)
                {
                    displMin = 30;
                }
                if (minute >= 30 && minute <= 59)
                {
                    displMin = 0;
                }
                break;
            case 2:
                if (minute >= 0 && minute <= 14)
                {
                    displMin = 15;
                }
                if (minute >= 15 && minute <= 29)
                {
                    displMin = 30;
                }
                if (minute >= 30 && minute <= 44)
                {
                    displMin = 45;
                }
                if (minute >= 45 && minute <= 59)
                {
                    displMin = 0;
                }
                break;
            case 1:
                displMin= hour;
                break;
        }
        
        return displMin.ToString();
    }
}
