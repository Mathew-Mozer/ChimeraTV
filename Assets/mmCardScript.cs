using UnityEngine;
using System.Collections;
using System;

public class mmCardScript : MonoBehaviour {
    public GameObject multiplier;
    public UILabel multLabel;
    public mmCard currentCard;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setCard(mmCard theCard)
    {
        currentCard = theCard;
        multiplier.SetActive(true);
        multLabel.text = theCard.Value;
    }

    public void resetCard()
    {
        multiplier.SetActive(false);
        multLabel.text = "";
    }
}
