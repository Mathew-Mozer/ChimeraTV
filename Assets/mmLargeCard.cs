using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class mmLargeCard : MonoBehaviour
{
    private mmCard currentCard;
    public TextMeshPro CardValue;
    public TextMeshPro CardValueLarge;
    public GameObject heart;
    public GameObject diamond;
    public GameObject club;
    public GameObject spade;
    public GameObject suitContainer;
    public string CardString;
    private bool isBlack;
    // Use this for initialization
    void Start()
    {
        if (!String.IsNullOrEmpty(CardString))
        {
            setCard(new mmCard(CardString), "start");
        }

    }

    // Update is called once per frame
    public void setCard(mmCard theCard, string what)
    {
        CardString = theCard.Card;
        //Debug.Log(what + " is setting Card: " + theCard.Card + "on " + gameObject.name);
        currentCard = theCard;
        string[] cardVal;
        if (theCard.Card.Length > 1)
        {
            cardVal = new string[] { theCard.Card.Substring(0, 1), theCard.Card.Substring(1, 1) };
            suitContainer.transform.localPosition = new Vector3(0, 0, 0);
            suitContainer.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            cardVal = new string[] { theCard.Card};
            suitContainer.transform.localPosition = new Vector3(-16, 35, 0);
            suitContainer.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
        
        if (theCard.Card.Contains("10"))
        {
            if (!theCard.Card.Equals("10"))
            {
                cardVal = new string[] { theCard.Card.Substring(0, 2), theCard.Card.Substring(2, 1) };
            }
            else
            {
                cardVal = new string[] { theCard.Card };
            }
            
        }
        disableAllSuits();
        if (cardVal.Length > 1)
        {
            CardValueLarge.text = "";
            CardValueLarge.gameObject.SetActive(false);
            switch (cardVal[1])
            {
                case "H":
                    heart.SetActive(true);
                    isBlack = false;
                    break;
                case "D":
                    diamond.SetActive(true);
                    isBlack = false;
                    break;
                case "C":
                    club.SetActive(true);
                    isBlack = true;
                    break;
                case "S":
                    spade.SetActive(true);
                    isBlack = true;
                    break;
            }
            CardValue.text = cardVal[0];
        }
        else
        {
            CardValue.text = "";
            CardValueLarge.gameObject.SetActive(false);
            switch (cardVal[0].ToUpper())
            {
                case "H":
                    heart.SetActive(true);
                    isBlack = false;
                    break;
                case "D":
                    diamond.SetActive(true);
                    isBlack = false;
                    break;
                case "C":
                    club.SetActive(true);
                    isBlack = true;
                    break;
                case "S":
                    spade.SetActive(true);
                    isBlack = true;
                    break;
                default:
                    CardValueLarge.gameObject.SetActive(true);
                    CardValueLarge.text = cardVal[0];
                    break;
            }
        }

        if (isBlack)
        {
            CardValue.color = Color.black;
        }
        else
        {
            CardValue.color = Color.red;
        }


    }

    public void setPayed(bool cardPayed)
    {
        
        if (cardPayed)
        {
            
            gameObject.GetComponent<UISprite>().color = DisplayManager.HexToColor("404242");
        }else
        {
            gameObject.GetComponent<UISprite>().color = DisplayManager.HexToColor("ffffff");
        }
        
    }

    private void disableAllSuits()
    {
        heart.SetActive(false);
        club.SetActive(false);
        diamond.SetActive(false);
        spade.SetActive(false);
    }
}
