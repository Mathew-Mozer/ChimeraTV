using UnityEngine;
using System.Collections;

/*
 * SelectionScript
 * 
 * Author - Stephen King
 * Version - 1.0
 * 
 * This is a terrible script that is meant to be generic but is currently over specialized. It should be refactored
 * Sepecifically as a script to exectute visual effects for the pot of gold promotion.
 * 
 */ 

public class SelectionScript : MonoBehaviour {

    public ParticleSystem particle;
    public GameObject coins;
    public GameObject rewardBanner;
    public UILabel award;

    //If this item is selected, make fireworks
    public void OnSelected(string awardText){
        coins.SetActive(false);
        particle.Play();
        rewardBanner.SetActive(true);
        award.text = awardText;
    }

    //Reset the top texture so it looks like there is gold.
    //I assume you will loop through and reset everything at once.
    public void ResetObject() {
        coins.SetActive(true);
        rewardBanner.SetActive(false);
    }
}
