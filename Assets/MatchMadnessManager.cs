using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This class manages something in the match promotion
/// <Author>Mathew Mozer</Author>
/// <Date>10/15/2016</Date>
/// <Version>10.15.2016.??</Version>
/// 
/// 1) Add comments
/// 2) Why are 3 of the variables public?
/// 3) Why are you setting the display manager every frame???????????
/// 4) There are god methods in here that need to be broken down
/// 
/// </summary>
public class MatchMadnessManager : MonoBehaviour
{
    private DisplayManager displayManager;
    public MatchMadness displayedMatchMadness;
    public GameObject CardGrid;
    public GameObject currentHitCard;


    private MultiplierBanner multiplierBanner;
    private bool cardset;
    private MatchMadness matchMadnessDataFile;
    private bool initialLaunch;

    private mmCard previousCard;
    private int previousCardMultiplier;
    //For Audio Playback
    public AudioClip[] audioClips;
    AudioSource audio = new AudioSource();
    public bool playAudio;
    // Use this for initialization
    void Start()
    {
        //Set initial objects
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        multiplierBanner = gameObject.GetComponent<MultiplierBanner>();
        /*
        matchMadnessDataFile = displayManager.displayInfo.matchMadness;
        */
        audio = GetComponent<AudioSource>();

        cardset = false;
        initialLaunch = true;
        displayManager.updateMatchMadness = true;
        InvokeRepeating("ScanCards", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void playSound(int clipID, float duration)
    {
        audio.PlayOneShot(audioClips[clipID], duration);
    }


    /// <summary>
    /// Check through each card to see if a bonus should be revealed
    /// </summary>
    private void ScanCards()
    {

        
        if (displayManager.updateMatchMadness)
        {
            displayManager.updateMatchMadness = false;
            MatchMadness mm = displayManager.currentScene.MatchMadnessData;
            cardset = false;
           //if (CardGrid.transform.childCount == mm.cardList.Count)
            //{
                //Debug.Log("Step 1: " + mm.cardList.Count);
                for (int i = 0; i < mm.cardList.Count; i++)
                {
                    ///Debug.Log("Step 1: " + mm.cardList.Count);
                    mmCard currentCard = mm.cardList[i];
                    //Debug.Log("Step 2: " + currentCard.Card);
                    Transform currentGridCard = CardGrid.transform.GetChild(i);
                    currentGridCard.GetComponent<UISprite>().spriteName = currentCard.Card;
                    if (currentCard.TimeStampHit!=null)
                    {
                        ShowBonus(currentGridCard, currentCard);
                        previousCard = currentCard;


                    }
                    else
                    {

                        NextCard(currentGridCard, currentCard);

                    }

                }
                initialLaunch = false;
            //}
        }
        
    }

    /// <summary>
    /// Activates the bonus for the current card
    /// </summary>
    /// <param name="currentGridCard"></param>
    /// <param name="currentCard"></param>
    private void ShowBonus(Transform currentGridCard, mmCard currentCard)
    {
        currentGridCard.GetComponent<UISprite>().color = Color.gray;
        currentGridCard.GetComponent<mmCardScript>().setCard(currentCard);

    }

    /// <summary>
    /// Sets the next active card in the grid
    /// </summary>
    /// <param name="currentGridCard"></param>
    /// <param name="currentCard"></param>
    private void NextCard(Transform currentGridCard, mmCard currentCard)
    {

        //Debug.Log("Should have reset" + currentCard.Card);
        currentGridCard.GetComponent<UISprite>().color = new Color32(220, 220, 220, 255);
        currentGridCard.GetComponent<mmCardScript>().resetCard();

        if (!cardset)
        {

            if (!initialLaunch)
            {
                multiplierBanner.ActivateBanner(previousCard);
                Invoke("DisableBannerHelper", 5);
                playSound(0, 5f);
            }

            currentHitCard.GetComponent<mmLargeCard>().setCard(currentCard,"Next Card");
            cardset = true;
        }
    }

    private void DisableBannerHelper()
    {
        multiplierBanner.DisableBanner();
        currentHitCard.SetActive(true);
    }
    public void ActivateBanner()
    {
        
        multiplierBanner.ActivateBanner(previousCard);
        currentHitCard.SetActive(false);
        Invoke("DisableBannerHelper", 5);
    }
}
