using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System;

public class HighHandManagerOLD: MonoBehaviour {
    public UILabel nameLabel;
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
    public UILabel money;
    public UILabel moneyToBe;
    public UILabel LeftBoxTitle;
    public int givenAmount;
    public int payAmount=100;
    public int flipped;
    public UILabel msg;
    public UILabel msgttl;
    public DisplayManager displayManager;
    public Transform theLastCard;
    public List<prevHand> thePrevHands = new List<prevHand>();
    public List<Vector3> Placements;
    bool moveCard = false;
    private bool enablePlaces;

    void Awake()
    {
        
    }
    /*
    void Start()
    {
     
       displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
       switch (Application.loadedLevel)
        {
            case 1:
                updateCards();
                    break;
                case 2:
                    updateHighHandBoard();
                    break;
            case 7:
                updateHighHandBoard();
                break;
            case 9:
                updateHighHandBoard();
                break;



        }
       displayManager.updateHighHand = true;
    }
    void Update()
    {
        if (displayManager.updateHighHand)
        {

            switch (Application.loadedLevel)
            {
                case 1:
                    updateCards();
                    
                    displayManager.updateHighHand = false;
                    break;
                case 2:
                    updateHighHandBoard();
                    resetTween();
                    displayManager.updateHighHand = false;
                    break;
                case 7:
                    updateHighHandBoard();
                    updatePrevHands();
                    displayManager.updateHighHand = false;
                    break;
                case 9:
                    enablePlaces = true;
                    resetPlaceCards();
                    //updateHighHandBoard();
                    if (enablePlaces) {
                        //Debug.Log("updating hands");
                        updatePlaceHands();
                    }
                    displayManager.updateHighHand = false;
                    break;
            }
            
            
        }
        switch (Application.loadedLevel)
        {
            case 1:
                 if(displayManager.TheHand.paytype ==1)
                    {
                         LeftBoxTitle.text = "Next Prize Worth";
                       
                    }
                 else if (displayManager.TheHand.paytype == 2)
                 {
                     LeftBoxTitle.text = "Prizes Awarded";
                 }
                 else if (displayManager.TheHand.paytype == 3)
                 {
                     LeftBoxTitle.text = "Last Prize Won";
                 } 
                   
                money.text = "$" + givenAmount;
                moneyToBe.text = "$" + payAmount;
                 break;
              
              
            case 2:
                 msg.text = displayManager.TheHand.TitleMsg;
                break;
            case 7:
                msg.text = "";
                msgttl.text = "";
                //Debug.Log("list type: " + displayManager.TheHand.HandListType);
                if (displayManager.TheHand.HandListType == 0)
                {
                    string[] themsg = displayManager.TheHand.TitleMsg.Split('\n');
                    //Debug.Log(" count:" + themsg.Length);
                    msgttl.text = themsg[0];
                    for(int i=1; i < themsg.Length-1; i++)
                    {
                        msg.text += themsg[i] + '\n';
                    }
                    
                    //Debug.Log("supposed to show message");
                }
                else
                {
                    //Debug.Log("Not supposed to show message");
                    msg.text = "";
                    msgttl.text = "";
                }
                break;
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
        msg.text = displayManager.TheHand.TitleMsg;
        GameObject skin = GameObject.FindGameObjectWithTag("Skin");
        int hnd = 0;
        foreach (prevHand ph in thePrevHands)
        {
            ph.visible(false);
        }
            switch (displayManager.TheHand.previousHands.Count)
            {
                case 0:
                nameLabel.text = "You could be next!";
                //Debug.Log("should be here");
                
                cards[0].atlas = skin.GetComponent<UIAtlas>();
                cards[1].atlas = skin.GetComponent<UIAtlas>();
                cards[2].atlas = skin.GetComponent<UIAtlas>();
                cards[3].atlas = skin.GetComponent<UIAtlas>();
                cards[4].atlas = skin.GetComponent<UIAtlas>();
                //deprecated
                //cards[5].atlas = skin.GetComponent<UIAtlas>();
                //cards[6].atlas = skin.GetComponent<UIAtlas>();
                cards[0].spriteName = "CB";
                cards[1].spriteName = "CB";
                cards[2].spriteName = "CB";
                cards[3].spriteName = "CB";
                cards[4].spriteName = "CB";
          
                //doesn't exist
                //cards[5].spriteName = "CB";
                //cards[6].spriteName = "CB";
                thePrevHands[0].visible(false);
                thePrevHands[1].visible(false);
                break;
                case 1:
                thePrevHands[0].visible(false);
                thePrevHands[1].visible(false);
                if (displayManager.TheHand.active)
                {
                    
                    updateHand(displayManager.TheHand.previousHands[0], CardAtlas, displayManager);
                }
                    break;
                case 2:
                
                if (displayManager.TheHand.active)
                {
                    
                    thePrevHands[0].visible(true);
                    thePrevHands[1].visible(false);
                    thePrevHands[0].transform.localPosition = Placements[1];
                    thePrevHands[0].updateHand(displayManager.TheHand.previousHands[1], CardAtlas, displayManager);
                    updateHand(displayManager.TheHand.previousHands[0], CardAtlas, displayManager);
                }
                break;
            case 3:
                if (displayManager.TheHand.active)
                {
                    
                    thePrevHands[0].visible(true);
                    thePrevHands[1].visible(true);
                    thePrevHands[0].transform.localPosition = Placements[0];
                    updateHand(displayManager.TheHand.previousHands[0], CardAtlas, displayManager);
                    thePrevHands[0].updateHand(displayManager.TheHand.previousHands[1], CardAtlas, displayManager);
                    thePrevHands[1].updateHand(displayManager.TheHand.previousHands[2], CardAtlas, displayManager);
                }
                break;

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

    }
    private void updatePrevHands()
    {
        int hnd = 0;
         foreach(prevHand ph in thePrevHands)
        {
            if (displayManager.TheHand.previousHands.Count == 0)
            {
                ph.visible(false);
            }
            if (displayManager.TheHand.previousHands.Count-1 >= hnd)
            {
                ph.visible(displayManager.TheHand.previousHands[hnd]);
                ph.updateHand(displayManager.TheHand.previousHands[hnd], CardAtlas,displayManager);
            }else
            {
                ph.visible(false);
            }
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
    /*
    public void setPayAmount(){
        payAmount = 200;
        if (displayManager.TheHand.GoldCards.Count!=0)
        {
            payAmount = getPayAmount(displayManager.TheHand.GoldCards.Count);
        }else
        {
            payAmount = payAmount = getPayAmount(1);
        }
                
    }
    public int getPayAmount(int CardCount)
    {
        int payamt=201;
        switch (displayManager.TheHand.paytype)
        {
            case 1:
                payamt = int.Parse(displayManager.TheHand.payouts);
                break;
            case 2:
                string[] payouts = displayManager.TheHand.payouts.Split(',');
                //Debug.Log("cc:" + CardCount + 1);
                payamt = int.Parse(payouts[CardCount++]);
                break;
            case 3:
                payamt = int.Parse(displayManager.TheHand.payouts);
                break;
        }
        return payamt;
    }

    private void updateHighHandBoard()
    {
        resetMainHand();

        setPayAmount();
        if (displayManager.TheHand != null&& displayManager.TheHand.theHand&& displayManager.TheHand.active)
        {
                if (!displayManager.TheHand.theHand.handID.Equals("0"))
                {
                enablePlaces = true;
                nameLabel.text = displayManager.TheHand.theHand.fname;
                    //congratsLabel.text = TheHand.theHand.fname;
                    cards[0].atlas = CardAtlas.GetComponent<UIAtlas>();
                    cards[1].atlas = CardAtlas.GetComponent<UIAtlas>();
                    cards[2].atlas = CardAtlas.GetComponent<UIAtlas>();
                    cards[3].atlas = CardAtlas.GetComponent<UIAtlas>();
                    cards[4].atlas = CardAtlas.GetComponent<UIAtlas>();
                    //deprecated
                    //cards[5].atlas = CardAtlas.GetComponent<UIAtlas>();
                    //cards[6].atlas = CardAtlas.GetComponent<UIAtlas>();

                    cards[0].spriteName = displayManager.TheHand.theHand.hand[0];
                    cards[1].spriteName = displayManager.TheHand.theHand.hand[1];
                    cards[2].spriteName = displayManager.TheHand.theHand.hand[2];
                    cards[3].spriteName = displayManager.TheHand.theHand.hand[3];
                    cards[4].spriteName = displayManager.TheHand.theHand.hand[4];
                
                
              
                //deprecated
                //cards[5].spriteName = displayManager.TheHand.theHand.hand[5];
                //cards[6].spriteName = displayManager.TheHand.theHand.hand[6];
            }
        }
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
        //deprecated
        //cards[5].atlas = skin.GetComponent<UIAtlas>();
        //cards[6].atlas = skin.GetComponent<UIAtlas>();
        cards[0].spriteName = "CB";
        cards[1].spriteName = "CB";
        cards[2].spriteName = "CB";
        cards[3].spriteName = "CB";
        cards[4].spriteName = "CB";
        //doesn't exist
        //cards[5].spriteName = "CB";
        //cards[6].spriteName = "CB";

//        cardTP[0].ResetToBeginning();
    }

    private void updateCards()
    {
        
         foreach (Transform cardd in cardboard.transform)
        {
             //Checks to see if a card has been removed
            if (!displayManager.TheHand.GoldCards.Contains(cardd.GetComponent<UISprite>().spriteName.ToString()))
            {
                //set card face up and reset values
                if (!cardd.GetComponent<card>().Face)
                {
                    cardd.GetComponent<card>().flipCard(0);
                    recalcstuff();

            
                }

                
            }

             //If none were deleted
            if (displayManager.TheHand.GoldCards.Any(i => i.Equals(cardd.GetComponent<UISprite>().spriteName.ToString())))
            {
                if (cardd.GetComponent<card>().Face)
                {
                    cardd.GetComponent<card>().flipCard(payAmount);
                    //givenAmount = givenAmount + payAmount;
                    flipped++;
                    moveCard = true;
                    theLastCard = cardd;
                    if (displayManager.TheHand.GoldCards[0] == cardd.GetComponent<UISprite>().spriteName.ToString())
                    {
                    theLastCard = cardd;
                    }
                    setPayAmount();
                }
            }
        }
        
         FlipLastCard();
         calcGivenAmount();
    }

    private void calcGivenAmount()
    {
        int amt = 0;
        switch (displayManager.TheHand.paytype)
        {
            case 1:
               amt = int.Parse(displayManager.TheHand.payouts);
                break;
            case 2:
                for (int i=0; i < displayManager.TheHand.GoldCards.Count; i++)
                     {
            //Debug.Log("GA:" + amt);
                           amt = amt + getPayAmount(i);
                      }
                break;
            case 3:
                amt = int.Parse(displayManager.TheHand.givenAmount);
                break;
        }
        givenAmount = amt;
        
    }

    void FlipLastCard()
    {
        if (moveCard) {
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
    */
}
