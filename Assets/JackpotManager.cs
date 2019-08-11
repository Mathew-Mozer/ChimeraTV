using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class JackpotManager : MonoBehaviour
{
    public bool addTheValue = true;
    public Odometer currentODO;
    public int addNumber = 50;
    public float delay = .1f;
    private UIGrid digitGrid;
    public int digitCount;
    private Vector3 moveToLocation;
    private Vector3 startingPosition;
    public float speed;
    float _timeStartedLerping;
    public float percentageComplete;
    float timeTakenDuringLerp = 5f;
    bool _isLerping = true;
    private Vector3 logoStartingPosition;
    private Vector3 moveLogoToLocation;
    private GameObject odo;
    public GameObject logo;
    public GameObject winnerObjectPrefab;
    public GameObject NextPayoutBox;
    public GameObject TimerBox;
    public bool scrollIfNeeded;
    bool leftDrop;
    
    public UILabel lblTitle;
    int pr = 0;
    List<int> displayedIDs = new List<int>();
    public int OdoSetValue;
    public List<PlayerWinner> playerWinners = new List<PlayerWinner>();
    private bool enableBoard;
    private float repTime = 2f;
    public int ttl=0;
    private bool hasStarted = false;
    private bool namescrolling;
    private int sceneID;
    // Use this for initialization
    void Awake()
    {
        
    }
    void Start()
    {
        //currentODO = gameObject.GetComponent<Odometer>();+
        Application.runInBackground = true;
        digitGrid = GameObject.Find("DigitGrid").GetComponent<UIGrid>();

        //startingPosition = gameObject.transform.localPosition;
        startingPosition = new Vector3(0, startingPosition.y - 450, startingPosition.z);
        logoStartingPosition = new Vector3(0, 600, startingPosition.z);
        delay = .01f;
        odo = currentODO.gameObject;
        //InvokeRepeating("randomNameDrop", 1,4F);
        //InvokeRepeating("randomNameScroll", 1, 2F);
        //staticNamePlacement(false);
        enableBoard = true;

    }
    IEnumerator scrollPlayer()
    {
        //Debug.Log("start scrolling");
        //Scroll Players as long as ttle number of players doesn't equal 0
        if (!namescrolling)
        {
            while (ttl != 0)
            {
                //if total number of cards = amount of winners or PR  is greater than or equal to winners 
                if (displayedIDs.Count == DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count || pr >= DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count)
                {
                    //Debug.Log("Clear ID");
                    repTime = 5;
                    displayedIDs.Clear();
                    namescrolling = false;
                    pr = 0;
                }
                else
                {
                    namescrolling = true;
                    //Debug.Log("scrollName");
                    repTime = 2f;
                    NameScroll();
                }

                yield return new WaitForSeconds(repTime);
            }
        }
    }
    /*
    void randomNameDrop()
    {
        GameObject newObject = NGUITools.AddChild(gameObject, winnerObjectPrefab);
        newObject.GetComponent<objWinner>().setPlayer(DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist[Random.Range(0, DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count)], leftDrop, true, false, false, new Vector3(0, 0, 0));
        leftDrop = !leftDrop;
    }
    
    void staticNamePlacement(bool randomPlayer)
    {
        //vec
        GameObject newObject = NGUITools.AddChild(gameObject, winnerObjectPrefab);
        if (randomPlayer) {
            newObject.GetComponent<objWinner>().setPlayer(playerWinners[Random.Range(0, playerWinners.Count)], leftDrop, true, false, false,);
        }else
        {
            for(int i; i > playerWinners.Count; i++)
            {
                newObject.GetComponent<objWinner>().setPlayer(playerWinners[i], leftDrop, true, false, false,);
            }
            

        }
        leftDrop = !leftDrop;
    }*/
    void NameScroll()
    {

        ttl = 0;
        int theID = 0;
        bool found = true;
        if (DisplayManager.displayManager.currentScene.PrizeEventData != null)
        {
            
                ttl = DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count;
                if (ttl > 0)
                {
                    GameObject newObject = NGUITools.AddChild(gameObject, winnerObjectPrefab);

                    if (DisplayManager.displayManager.currentScene.PrizeEventData.RandomPrize == 1)
                    {
                        
                        while (found)
                        {
                            theID = Random.Range(0, DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count);
                            if (displayedIDs.Contains(theID))
                            {
                                found = true;

                            }
                            else
                            {
                                found = false;
                                displayedIDs.Add(theID);
                            }
                        }
                        newObject.GetComponent<objWinner>().setPlayer(DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist[theID], leftDrop, false, true, false, new Vector3(0, 0, 0));
                    }
                    else
                    {
                        //DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist[Random.Range(0, DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count)]
                        //Debug.Log("peeps:" + pr);
                        if (ttl != 0)
                        {
                            displayedIDs.Add(theID);
                            newObject.GetComponent<objWinner>().setPlayer(DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist[pr], leftDrop, false, true, false, new Vector3(0, 0, 0));
                            pr++;
                        }
                        
                    }
                   
                
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Set at startup
        if (enableBoard)
        {
            NextPayoutBox.SetActive(Convert.ToBoolean(DisplayManager.displayManager.currentScene.PrizeEventData.NextTimeVisible));
            TimerBox.SetActive(Convert.ToBoolean(DisplayManager.displayManager.currentScene.PrizeEventData.clockRemainingVisible));
            //if Lbl changes or jackpot changes then reset board
            if (lblTitle.text != DisplayManager.displayManager.currentScene.PrizeEventData.Title || addNumber != DisplayManager.displayManager.currentScene.PrizeEventData.JackPotAmount)
            {
                
                currentODO.gameObject.GetComponent<adjustODO>().resetODO = true;
                currentODO.SetValue(0);
                lblTitle.text = DisplayManager.displayManager.currentScene.PrizeEventData.Title;
                addNumber = DisplayManager.displayManager.currentScene.PrizeEventData.JackPotAmount;
                //Debug.Log("Add Value: True");
                addTheValue = true;
            }
            //if box is checked or value needs to be added to the odo via code
            if (addTheValue)
            {
                currentODO.gameObject.GetComponent<adjustODO>().resetODO = true;
                currentODO.SetValue(0);
                if (DisplayManager.displayManager.currentScene.PrizeEventData.IncrementNumber == 1)
                {
                    //Rolling ODO
                    currentODO.IncrementSmooth(addNumber, delay);
                }else
                {
                    //Static Value
                    currentODO.gameObject.GetComponent<adjustODO>().setODO(addNumber);
                }
                
                addTheValue = false;
            }
        }else
        {
            lblTitle.text = "No Active Events";
           
        }
        if (ttl == 0&& hasStarted)
        {
            if (DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count > 0)
            {
                
                ttl = DisplayManager.displayManager.currentScene.PrizeEventData.winnerlist.Count;
                StartCoroutine("scrollPlayer");
            }

        }
        
        moveLogoToLocation = new Vector3(0, 210, logo.transform.localPosition.z);
        //changing of the logoposition and start Scrolling players
        if (_isLerping)
            {
            hasStarted = true;
                float timeSinceStarted = Time.time - _timeStartedLerping;
                percentageComplete = timeSinceStarted / timeTakenDuringLerp;
                //odo.transform.localPosition = Vector3.Lerp(startingPosition, moveToLocation, percentageComplete);
                logo.transform.localPosition = Vector3.Lerp(logoStartingPosition, moveLogoToLocation, percentageComplete);
                float step = 1.5f * Time.deltaTime;
                if (percentageComplete >= 1.0f)
                {
                if (ttl > 0)
                {
                    StartCoroutine("scrollPlayer");
                }
                _isLerping = false;
                }
            }
        
    }
    
}
