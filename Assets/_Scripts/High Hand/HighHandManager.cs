using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System;
using TMPro;


public class HighHandManager : MonoBehaviour
{
    public TextMeshPro nameLabel;
    public UILabel congratsLabel;
    public GameObject congratsbox;
    public List<UISprite> cards = new List<UISprite>();
    public List<TweenPosition> cardTP = new List<TweenPosition>();
    public GameObject cardboard;
    public int HHHour;
    public GameObject mc;
    public UIAtlas CardAtlas;
    public GameObject promoWindow;
    public ParticleManager pm;
    public TextMeshPro money;
    public TextMeshPro moneyToBe;
    public TextMeshPro LeftBoxTitle;
    public int givenAmount;
    public int payAmount = 100;
    public int flipped;
    //public UILabel msg;
    public TextMeshProUGUI msg;
    public UILabel msgttl;
    public DisplayManager displayManager;
    public Transform theLastCard;
    public List<prevHand> thePrevHands = new List<prevHand>();
    public List<Vector3> Placements;
    bool moveCard = false;
    private bool enablePlaces;
    public UITexture background;
    public GameObject CardContainer;
    public List<Vector3> CardContainerPosition;
    public GameObject OdometerContainer;
    public List<Vector3> ODOContainerPosition;
    void Awake()
    {

    }
    void Start()
    {

        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        //updateHighHandBoard();
        //background.mainTexture = displayManager.sceneTextures[0];
        displayManager.updateSceneDataNow = true;
        switch (Application.loadedLevel)
        {

            case 1:

                break;
            case 2:
                enableTween(displayManager.currentScene.animation);
                break;
            case 7:
                enableTween(displayManager.currentScene.animation);
                if (displayManager.currentScene.highHandData.HandList.Count > 5)
                {
                    CardContainer.transform.localPosition = CardContainerPosition[1];
                }else
                {
                    CardContainer.transform.localPosition = CardContainerPosition[0];
                }
                    
                break;
            case 9:
                enableTween(displayManager.currentScene.animation);
                break;
        }


    }

    private void enableTween(bool animation)
    {
        cards[0].GetComponent<TweenPosition>().enabled = animation;
        cards[1].GetComponent<TweenPosition>().enabled = animation;
        cards[2].GetComponent<TweenPosition>().enabled = animation;
        cards[3].GetComponent<TweenPosition>().enabled = animation;
        cards[4].GetComponent<TweenPosition>().enabled = animation;
        try
        {
            cards[5].GetComponent<TweenPosition>().enabled = animation;
            cards[6].GetComponent<TweenPosition>().enabled = animation;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("NRE:" + ex.Message);
        }
       
    }

    void Update()
    {
        if (displayManager.updateSceneDataNow)
        {

            switch (Application.loadedLevel)
            {

                case 1:
                    updateCards();
                    break;
                case 2:
                    updateHighHandBoard();
                    updatePrevHands();
                    displayManager.updateSceneDataNow = false;
                    break;
                case 7:
                    updateHighHandBoard();
                    updatePrevHands();
                    updateOdoPosition();
                    displayManager.updateSceneDataNow = false;
                    break;
                case 9:

                    enablePlaces = true;
                    resetPlaceCards();
                    //updateHighHandBoard();
                    if (enablePlaces)
                    {
                        //Debug.Log("updating hands");
                        updatePlaceHands();
                    }
                    displayManager.updateSceneDataNow = false;
                    break;
            }


        }
        if (displayManager.currentScene.highHandData)
        {
            switch (Application.loadedLevel)
            {

                case 1:
                    if (displayManager.currentScene.highHandData.paytype == 1)
                    {
                        LeftBoxTitle.text = "Next Prize Worth";

                    }
                    else if (displayManager.currentScene.highHandData.paytype == 2)
                    {
                        LeftBoxTitle.text = "Prizes Awarded";
                    }
                    else if (displayManager.currentScene.highHandData.paytype == 3)
                    {
                        LeftBoxTitle.text = "Last Prize Won";
                    }

                    money.text = "$" + givenAmount;
                    //moneyToBe.text = "$" + payAmount;
                    break;


                case 2:
                    //msg.GetComponent<TextMeshPro> .text = displayManager.currentScene.highHandData.TitleMsg;
                    break;
                case 7:
                    msg.SetText(displayManager.currentScene.highHandData.TitleMsg);
                    /*
                    msgttl.text = "";
                    //Debug.Log("list type: " + displayManager.currentScene.highHandData.HandListType);
                    if (displayManager.currentScene.highHandData.HandListType == 0)
                    {
                        string[] themsg = displayManager.currentScene.highHandData.TitleMsg.Split('\n');
                        //Debug.Log(" count:" + themsg.Length);
                        msgttl.text = themsg[0];
                        for (int i = 1; i < themsg.Length; i++)
                        {
                            //msg.text += themsg[i] + '\n';
                        }

                        //Debug.Log("supposed to show message");
                    }
                    else
                    {
                        //Debug.Log("Not supposed to show message");
                        //msg.text = "";
                        msgttl.text = "";
                    }
                    */
                    break;
                case 9:
                    //msg.SetText(displayManager.currentScene.highHandData.TitleMsg);
                    break;

            }

        }

    }

    private void updateOdoPosition()
    {
        if (payAmount > 999)
        {
            OdometerContainer.transform.localPosition = ODOContainerPosition[1];
        }else
        {
            OdometerContainer.transform.localPosition = ODOContainerPosition[0];
        }
        
    }

    private void resetTween()
    {

        cardTP[0].ResetToBeginning();
        cardTP[1].ResetToBeginning();
        cardTP[2].ResetToBeginning();
        cardTP[3].ResetToBeginning();
        cardTP[4].ResetToBeginning();
      
        //Debug.Log("Reset Tween");
        cardTP[0].PlayForward();
        cardTP[1].PlayForward();
        cardTP[2].PlayForward();
        cardTP[3].PlayForward();
        cardTP[4].PlayForward();
        
    }

    private void resetPlaceCards()
    {
        thePrevHands[0].visible(false);
        thePrevHands[1].visible(false);
    }

    private void updatePlaceHands()
    {
        highHand HighHand = displayManager.currentScene.highHandData;
        //msg.text = displayManager.currentScene.highHandData.TitleMsg;
        GameObject skin = GameObject.FindGameObjectWithTag("Skin");
        int hnd = 0;
        foreach (prevHand ph in thePrevHands)
        {
            ph.visible(false);
        }
        /*if (HighHand.handcount > 1)
        {
            for (int i = 0; i < HighHand.handcount - 2; i++)
            {
                thePrevHands[i].visible(true);
            }
        }
        */

        if (HighHand.HandList.Count == 0 || !displayManager.sceneClock.active)
        {
            nameLabel.text = "You could be next!";
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

            if (cards.Count > 5)
            {
                cards[5].atlas = skin.GetComponent<UIAtlas>();
                cards[6].atlas = skin.GetComponent<UIAtlas>();
                cards[5].spriteName = "CB";
                cards[6].spriteName = "CB";
            }


            //cards[5].spriteName = "CB";
            //cards[6].spriteName = "CB";
            thePrevHands[0].visible(false);
            thePrevHands[1].visible(false);
        }

        if (HighHand.HandList.Count > 0 && HighHand.handcount > 0)
        {
            thePrevHands[0].visible(false);
            thePrevHands[1].visible(false);
            if (displayManager.currentScene.active)
            {
                updateHand(HighHand.HandList[0], CardAtlas, displayManager);
            }


        }
        if (HighHand.HandList.Count >= 2 && HighHand.handcount > 1)
        {
            if (displayManager.currentScene.active)
            {
                if (HighHand.handcount > 1)
                {
                    thePrevHands[0].visible(true);
                    thePrevHands[1].visible(false);
                    thePrevHands[0].transform.localPosition = Placements[1];
                }

                thePrevHands[0].updateHand(HighHand.HandList[1], CardAtlas, displayManager);
                updateHand(HighHand.HandList[0], CardAtlas, displayManager);
            }
        }
        if (HighHand.HandList.Count >= 3 && HighHand.handcount == 3)
        {
            if (displayManager.currentScene.active)
            {
                thePrevHands[0].visible(true);
                if (HighHand.handcount == 3)
                {
                    thePrevHands[1].visible(true);
                    thePrevHands[0].transform.localPosition = Placements[0];
                }

                updateHand(HighHand.HandList[0], CardAtlas, displayManager);
                thePrevHands[0].updateHand(HighHand.HandList[1], CardAtlas, displayManager);
                thePrevHands[1].updateHand(HighHand.HandList[2], CardAtlas, displayManager);
            }


        }




    }
    public void updateHand(Hand theHand, UIAtlas CardAtlas, DisplayManager dm)
    {
        
        nameLabel.text = theHand.fname;
        cards[0].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[1].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[2].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[3].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[4].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[0].spriteName = theHand.hand[0];
        cards[1].spriteName = theHand.hand[1];
        cards[2].spriteName = theHand.hand[2];
        cards[3].spriteName = theHand.hand[3];
        cards[4].spriteName = theHand.hand[4];

        if (theHand.hand.Length > 5)
        {
            
            cards[5].atlas = CardAtlas.GetComponent<UIAtlas>();
            cards[6].atlas = CardAtlas.GetComponent<UIAtlas>();
            cards[5].spriteName = theHand.hand[5];
            cards[6].spriteName = theHand.hand[6];
        }
        

    }
    private void updatePrevHands()
    {
        int hnd = 0;
        foreach (prevHand ph in thePrevHands)
        {
            /*  if (displayManager.currentScene.highHandData.previousHands.Count == 0)
              {
                  ph.visible(false);
              }
              if (displayManager.currentScene.highHandData.previousHands.Count - 1 >= hnd)
              {
                  ph.visible(displayManager.currentScene.highHandData.previousHands[hnd]);
                  ph.updateHand(displayManager.currentScene.highHandData.previousHands[hnd], CardAtlas, displayManager);
              }
              else
              {
                  ph.visible(false);
              }
              */
            hnd++;
        }
    }

    public void sethhWinnercr()
    {
        StartCoroutine("sethhWinner");
    }

    /*
        IEnumerator startShow()
        {
            Debug.Log("set winner now");
            congratsbox.SetActive(true);
            promoWindow.SetActive(false);
            pm.LaunchFireworks();
            yield return new WaitForSeconds(13);
            congratsbox.SetActive(false);
            promoWindow.SetActive(true);

        }*/

    public void setPayAmount()
    {
        payAmount = 200;
        if (displayManager.currentScene.highHandData.GoldCards.Count != 0)
        {
            payAmount = getPayAmount(displayManager.currentScene.highHandData.GoldCards.Count);
        }
        else
        {
            payAmount = payAmount = getPayAmount(0);
        }

    }
    public int getPayAmount(int CardCount)
    {
        int payamt = 199;
        switch (displayManager.currentScene.highHandData.paytype)
        {
            case 1:
                payamt = int.Parse(displayManager.currentScene.highHandData.payouts);
                break;
            case 2:
                string[] payouts = displayManager.currentScene.highHandData.payouts.Split(',');
                payamt = int.Parse(payouts[CardCount++]);
                break;
            case 3:
                payamt = int.Parse(displayManager.currentScene.highHandData.payouts);
                break;
        }
        return payamt;
    }

    private void updateHighHandBoard()
    {

        resetMainHand();
        setPayAmount();
        if (displayManager.currentScene.highHandData != null && displayManager.currentScene.highHandData.HandList.Count > 0 && displayManager.currentScene.active)
        {

            if (displayManager.currentScene.highHandData.HandList.Count > 0)
            {

                enablePlaces = true;
                if (displayManager.currentScene.animation)
                {

                    resetTween();
                }
                prevHand pfh = new prevHand();
                int currentWinningMinute = int.Parse(pfh.winningMinute(displayManager.currentScene.highHandData.HandList[0].timestamp, displayManager.currentScene.highHandData.sessionTimer));
                int currentMinute = int.Parse(pfh.winningMinute(displayManager.currentTime.ToString(), displayManager.currentScene.highHandData.sessionTimer));
                //Debug.Log("Current Time:" + displayManager.currentTime.ToString() + " -  Hand Timestamp:" + displayManager.currentScene.highHandData.HandList[0].timestamp);
                //Debug.Log("Current Minute:" + pfh.winningMinute(displayManager.currentTime.ToString()) + " -  Hand Timestamp:" + pfh.winningMinute(displayManager.currentScene.highHandData.HandList[0].timestamp));

                if (Convert.ToBoolean(displayManager.currentScene.highHandData.LockToTime))
                {
                    if (currentMinute == currentWinningMinute)
                    {
                        setHand();
                    }
                }
                else
                {
                    setHand();
                }

            }
        }
    }

    private void setHand()
    {
        Debug.Log("Update Hands CAlled");
        Hand theHand = displayManager.currentScene.highHandData.HandList[0];
        nameLabel.text = displayManager.currentScene.highHandData.HandList[0].fname;
        cards[0].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[1].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[2].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[3].atlas = CardAtlas.GetComponent<UIAtlas>();
        cards[4].atlas = CardAtlas.GetComponent<UIAtlas>();
        Debug.Log("Hands count:" + cards.Count);
        try
        {
            Debug.Log("Handsasds:" + theHand.hand.Length);
            cards[5].atlas = CardAtlas.GetComponent<UIAtlas>();
            cards[6].atlas = CardAtlas.GetComponent<UIAtlas>();
            cards[5].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[5];
            cards[6].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[6];
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("NRE:" + ex.Message);
        }


        
       

        cards[0].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[0];
        cards[1].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[1];
        cards[2].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[2];
        cards[3].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[3];
        cards[4].spriteName = displayManager.currentScene.highHandData.HandList[0].hand[4];
    }

    private void resetMainHand()
    {
        nameLabel.text = "You could be next!";
        //Debug.Log("should be here");
        GameObject skin = GameObject.FindGameObjectWithTag("Skin");
        cards[0].atlas = skin.GetComponent<UIAtlas>();
        cards[1].atlas = skin.GetComponent<UIAtlas>();
        cards[2].atlas = skin.GetComponent<UIAtlas>();
        cards[3].atlas = skin.GetComponent<UIAtlas>();
        cards[4].atlas = skin.GetComponent<UIAtlas>();
        if (displayManager.currentScene.highHandData.HandList.Count > 5)
        {
            cards[5].atlas = skin.GetComponent<UIAtlas>();
            cards[6].atlas = skin.GetComponent<UIAtlas>();
        }
         
        //deprecated
        //cards[5].atlas = skin.GetComponent<UIAtlas>();
        //cards[6].atlas = skin.GetComponent<UIAtlas>();
        cards[0].spriteName = "CB";
        cards[1].spriteName = "CB";
        cards[2].spriteName = "CB";
        cards[3].spriteName = "CB";
        cards[4].spriteName = "CB";
        if (displayManager.currentScene.highHandData.HandList.Count > 5)
        {
            cards[5].spriteName = "CB";
            cards[6].spriteName = "CB";
        }
        
        
    }

    private void updateCards()
    {
        MonsterCarlo monsterCarlo = displayManager.currentScene.monsterCarloData;
        if (monsterCarlo)
        {
            List<string> tmpList = monsterCarlo.cardList;


            foreach (Transform cardd in cardboard.transform)
            {
                //Checks to see if a card has been removed

                if (tmpList != null && tmpList.Any() && tmpList.FirstOrDefault() != null)
                {
                    if (
                        !displayManager.currentScene.monsterCarloData.cardList.Contains(
                            cardd.GetComponent<mmLargeCard>().CardString))
                    {
                        //set card face up and reset values
                        if (!cardd.GetComponent<card>().Face)
                        {
                            cardd.GetComponent<card>().flipCard(0);
                            recalcstuff();
                        }


                    }

                    //If none were deleted
                    if (tmpList.Any(i => i.Equals(cardd.GetComponent<mmLargeCard>().CardString)))
                    {
                        if (cardd.GetComponent<card>().Face)
                        {
                            cardd.GetComponent<card>().flipCard(payAmount);
                            //givenAmount = givenAmount + payAmount;
                            flipped++;
                            moveCard = true;
                            theLastCard = cardd;
                            if (displayManager.currentScene.monsterCarloData.cardList[0] == cardd.GetComponent<mmLargeCard>().CardString)
                            {
                                theLastCard = cardd;
                            }
                            setPayAmount();
                        }
                    }
                }

            }

            FlipLastCard();
            calcGivenAmount();
        }
    }

    private void calcGivenAmount()
    {
        int amt = 0;
        switch (displayManager.currentScene.highHandData.paytype)
        {
            case 1:
                amt = int.Parse(displayManager.currentScene.highHandData.payouts);
                break;
            case 2:
                for (int i = 0; i < displayManager.currentScene.monsterCarloData.cardList.Count; i++)
                {
                    //Debug.Log("GA:" + amt);
                    amt = amt + getPayAmount(i);
                }
                break;
            case 3:
                amt = int.Parse(displayManager.currentScene.highHandData.givenAmount);
                break;
        }
        givenAmount = amt;

    }

    void FlipLastCard()
    {
        if (moveCard)
        {
            Debug.Log("setting Card: " + displayManager.currentScene.monsterCarloData.cardList[displayManager.currentScene.monsterCarloData.cardList.Count - 1]);
            theLastCard.GetComponent<mmLargeCard>().setCard(new mmCard(displayManager.currentScene.monsterCarloData.cardList[displayManager.currentScene.monsterCarloData.cardList.Count-1]),"flip");
            theLastCard.GetComponent<card>().LaunchFlyingCard();
            moveCard = false;
        }
    }

    private void recalcstuff()
    {
        givenAmount = 0;
        flipped = 0;
        payAmount = 200;
        foreach (Transform cardd in cardboard.transform)
        {
            cardd.GetComponent<card>().reset();

        }
    }
    public void ExitClick()
    {
        Application.Quit();
    }
}
