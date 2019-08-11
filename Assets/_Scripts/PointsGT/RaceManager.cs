using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public List<GameObject> Cars = new List<GameObject>();
    //public int[] Points = new int[]{1,2,3,4,5,6,7,8,9,10};
    //public int DaysInSession=7;
    //public int DayOfSession=1;
    public float multiplier1;
    public float multiplier2;
    private float maxunits = 11;
    public bool refreshRacers;
    public int maxPoints;
    public TextMeshPro lblvalue1;
    public TextMeshPro lblvalue2;
    public TextMeshPro lblvalue3;
    public TextMeshPro lblvalue2Title;
    public TextMeshPro lblvalue3Title;
    public TextMeshPro lblLastUpdate;
    public TextMeshPro lbltitle;
    public TextMeshPro lblDay;
    public PrizeList currentPL;
    public float TimePerSession;
    private DisplayManager displayManager;
    public List<UILabel> lbls = new List<UILabel>();
    public int currentPGTsession = 0;
    public AudioClip[] audioClips;
    AudioSource audio;
    public bool playAudio;
    public List<GameObject> DisableForWin;
    public GameObject CongratsContainer;
    public TextMeshPro FirstPlaceLabel;
    public TextMeshPro SecondPlaceLabel;
    public TextMeshPro ThirdPlaceLabel;
    public GameObject[] winningCars;
    public GameObject[] InstantWinnerLabels;
    public GameObject goInstantWinners;
    public GameObject AtlasGameObject;
    public ParticleManager fireworkPM;
    public List<UIAtlas> SpriteAtlases = new List<UIAtlas>();
    public List<GameObject> BackgroundTextures = new List<GameObject>();
    public List<Vector2> SpriteSize;
    public List<Vector2> LargeSpriteSize;
    public UIAtlas MainPgTaAtlas;
    public GameObject ChipDropper;
    public GameObject CarRace;
    public GameObject TimeTillStart;
    public static RaceManager raceManager;
    //    public RaceCar leadcar;
    private bool triggerFinish = false;

    // Use this for initialization
    void Awake()
    {
        raceManager = this;
    }
    void Start()
    {
        
        CongratsContainer.SetActive(false);
        audio = GetComponent<AudioSource>();
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        foreach (GameObject backgrounds in BackgroundTextures)
        {
            backgrounds.SetActive(false);
            ChipDropper.SetActive(false);
            CarRace.SetActive(false);
        }
        //BackgroundTextures[displayManager.currentScene.pointsGTData.SpriteAtlas].SetActive(true);
        switch (displayManager.currentScene.pointsGTData.SpriteAtlas)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                CarRace.SetActive(true);
                Debug.Log("activating background:" + displayManager.currentScene.pointsGTData.SpriteAtlas);
                BackgroundTextures[displayManager.currentScene.pointsGTData.SpriteAtlas].SetActive(true);
                MainPgTaAtlas.replacement = SpriteAtlases[displayManager.currentScene.pointsGTData.SpriteAtlas];
                break;
            case 4:
                BackgroundTextures[displayManager.currentScene.pointsGTData.SpriteAtlas].SetActive(true);
                ChipDropper.SetActive(true);
                ChipDropper.GetComponent<ChipDropper>().StartTheDrop();
                break;
            case 5:
                BackgroundTextures[displayManager.currentScene.pointsGTData.SpriteAtlas].SetActive(true);
                ChipDropper.SetActive(true);
                //ChipDropper.GetComponent<ChipDropper>().StartTheDrop();
                break;
            case 6:
                ChipDropper.SetActive(true);
                BackgroundTextures[displayManager.currentScene.pointsGTData.SpriteAtlas].SetActive(true);
                break;
        }
        

        //TimePerSession = displayManager.currentScene.duration / displayManager.displayInfo.pgtSessions.Count;
        TimePerSession = 0;
        if (TimePerSession == 0)
        {
            TimePerSession = 30;
        }

        StartCoroutine("StartTimer");

    }

    // Update is called once per frame
    void Update()
    {
        if (playAudio)
        {
            playAudio = false;
            //playSound(0);
        }
        if (audio.isPlaying)
        {

        }

    }
    void playSound(int clipID)
    {
       // audio.PlayOneShot(audioClips[clipID], .7f);
    }
    public void disableItemsforWin(bool tof)
    {

        foreach (GameObject gO in DisableForWin)
        {
            gO.SetActive(!tof);
        }
        CongratsContainer.SetActive(tof);
        foreach (GameObject particle in fireworkPM.particles)
        {
            particle.SetActive(tof);
        }
        if (tof)
        {
            fireworkPM.LaunchFireworks();
        }



    }
    IEnumerator finishRace()
    {
        yield return new WaitForSeconds(7.5f);
        //playSound(1);
        disableItemsforWin(true);
    }
    private void UpdateRaceCarsJson()
    {


        Vector3 tmpVector3;
        PGTSession theSession = displayManager.currentScene.pointsGTData;
        int plc = 0;
        multiplier2 = (multiplier1 / maxPoints);
        if (theSession.playerListJson != null)
        {
            //theSession.playerListJson.RandomizeTrack();
            theSession.playerListJson = theSession.sortlist(theSession.playerListJson);
            winningCars[0].GetComponent<RaceCar>()
                .setName(truncateName(theSession.playerListJson[0].PlayerName));
            winningCars[1].GetComponent<RaceCar>()
                .setName(truncateName(theSession.playerListJson[1].PlayerName));
            winningCars[2].GetComponent<RaceCar>()
                .setName(truncateName(theSession.playerListJson[2].PlayerName));

            //playSound(0);
            RaceCar currentCar;
            PGTSession currentSession;
            foreach (GameObject car in Cars)
            {
                pgtPlayer tmpCar = theSession.playerListJson[plc];
                //pgtPlayer tmpCar = theSession.playerlist.getCarByTrackLocation(plc);
                if (plc < 10)
                {

                    tmpVector3 = car.transform.localPosition;
                    //tmpVector3.y = (multiplier2*   Points[plc])*DayOfSession;
                    currentSession = displayManager.currentScene.pointsGTData;
                    currentCar = car.GetComponent<RaceCar>();
                    if (tmpCar.Points != 0)
                    {
                        tmpVector3.y = ((multiplier2 * tmpCar.Points) *
                                        displayManager.currentScene.pointsGTData.DayOfSession);

                        //StartCoroutine(car.GetComponent<RaceCar>().moveCar(tmpVector3, 2f));
                        currentCar.startMove(tmpVector3);
                    }
                    currentCar.setName(truncateName(tmpCar.PlayerName));
                    int curIW = 0;
                    if (displayManager.currentScene.pointsGTData.InstantWinners != null)
                    {
                        foreach (pgtInstantWinner iw in displayManager.currentScene.pointsGTData.InstantWinners)
                        {

                            if (tmpCar.Points >= iw.PointAmount)
                            {
                                currentCar.ActivateKey(curIW, true,
                                    displayManager.currentScene.pointsGTData.InstantWinners[curIW].IconColor);
                            }
                            curIW++;
                        }
                    }
                    /*
                    if (tmpCar.Points > currentSession.playerlist.InstantWinners[0].PointAmount)
                    {
                        Debug.Log("First Key");
                        
                    }
                    if (tmpCar.Points > currentSession.playerlist.InstantWinners[1].PointAmount)
                        {
                            Debug.Log("Second Key");
                            currentCar.ActivateKey(1, true, currentSession.playerlist.InstantWinners[1].IconColor);
                        }
                        
                    if (tmpCar.Points > currentSession.playerlist.InstantWinners[2].PointAmount)
                        {
                            Debug.Log("Third Key");
                            currentCar.ActivateKey(2, true, currentSession.playerlist.InstantWinners[2].IconColor);
                        }
                      */
                    lbls[plc].text = tmpCar.Points.ToString();
                }
                else
                {
                    car.GetComponent<UILabel>().text = "(" + tmpCar.Points + ") " + truncateName(tmpCar.PlayerName);
                }

                plc++;
            }
        }
    }
    private string truncateName(string p)
    {
        string newstring = "";
        if (!p.Equals(""))
        {
            p = p.Replace('.', ' ');
            string[] tmp = p.Split(' ');

            switch (tmp.Count())
            {
                case 1:
                    newstring = FirstCharToUpper(p);
                    break;
                case 2:
                    newstring = FirstCharToUpper(tmp[0]) + " " + tmp[1].Substring(0, 1).ToUpper() + ".";
                    break;
                case 3:
                    if (tmp[2].Length < 1)
                    {
                        newstring = FirstCharToUpper(tmp[0]) + " " + tmp[1].Substring(0, 1).ToUpper() + ".";
                    }
                    else
                    {
                        newstring = FirstCharToUpper(tmp[0]) + " " + tmp[2].Substring(0, 1).ToUpper() + ".";
                    }
                    break;
                default:
                    break;
            }
        }
        return newstring;
    }
    public static string FirstCharToUpper(string input)
    {
        if (input.Length > 0)
        {
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }
        else
        {
            return "";
        }
    }
    private IEnumerator StartTimer()
    {

        while (true)
        {
            displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();

            LoadSessionJson();
            UpdateRaceCarsJson();
            yield return new WaitForSeconds(TimePerSession);
            resetCars();
        }
    }

    private void LoadSessionJson()
    {
        disableItemsforWin(false);
        maxPoints = 0;
        if (displayManager.currentScene.pointsGTData.finished)
        {
            maxunits = 930;
        }
        else
        {
            //maxunits = 11.50f;
            maxunits = 850;
        }

        if (displayManager.currentScene.pointsGTData.PayoutList.Length > 1)
        {
            currentPL.setPrizeList(displayManager.currentScene.pointsGTData.PayoutList);

        }
        multiplier1 = (float)maxunits / (float)displayManager.currentScene.pointsGTData.DaysInSession;
        if (displayManager.currentScene.pointsGTData.playerlist != null)
        {
            if (displayManager.currentScene.pointsGTData.playerListJson != null)
            {
                foreach (pgtPlayer pl in displayManager.currentScene.pointsGTData.playerListJson)
                {
                    if (pl.Points > maxPoints)
                        maxPoints = pl.Points;
                }
            }
        }
        lblvalue1.text = displayManager.currentScene.pointsGTData.Value1;
        lblvalue2.text = displayManager.currentScene.pointsGTData.Value2;
        lblvalue3.text = displayManager.currentScene.pointsGTData.Value3;
        lblvalue2Title.text = displayManager.currentScene.pointsGTData.Value2Title;
        lblvalue3Title.text = displayManager.currentScene.pointsGTData.Value3Title;
        if (lblvalue2Title.text.Equals(""))
        {
            lblvalue2Title.text = "Rules";
        }
        if (lblvalue3Title.text.Equals(""))
        {
            lblvalue3Title.text = "Payouts";
        }
        if (displayManager.currentScene.pointsGTData.playerlist != null)
        {

            if (displayManager.currentScene.pointsGTData.InstantWinners != null)
            {
                if (displayManager.currentScene.pointsGTData.InstantWinners.Count > 0)
                {
                    goInstantWinners.SetActive(true);
                    InstantWinnerLabels[0].SetActive(false);
                    InstantWinnerLabels[1].SetActive(false);
                    InstantWinnerLabels[2].SetActive(false);
                    switch (displayManager.currentScene.pointsGTData.InstantWinners.Count)
                    {
                        case 1:
                            InstantWinnerLabels[0].SetActive(true);
                            InstantWinnerLabels[0].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[0]);
                            break;
                        case 2:
                            InstantWinnerLabels[0].SetActive(true);
                            InstantWinnerLabels[1].SetActive(true);
                            InstantWinnerLabels[0].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[0]);
                            InstantWinnerLabels[1].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[1]);
                            break;
                        case 3:
                            InstantWinnerLabels[0].SetActive(true);
                            InstantWinnerLabels[1].SetActive(true);
                            InstantWinnerLabels[2].SetActive(true);
                            InstantWinnerLabels[0].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[0]);
                            InstantWinnerLabels[1].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[1]);
                            InstantWinnerLabels[2].GetComponent<pgtIWsetup>()
                                .setupIWKey(displayManager.currentScene.pointsGTData.InstantWinners[2]);

                            break;
                    }
                }
                else
                {
                    goInstantWinners.SetActive(false);
                }
            }
        }
        lblLastUpdate.text = displayManager.currentScene.lastUpdated;
        lbltitle.text = displayManager.currentScene.pointsGTData.title;
        lblDay.text = displayManager.currentScene.pointsGTData.DayOfSession.ToString() + " of " + displayManager.currentScene.pointsGTData.DaysInSession.ToString();

        if (displayManager.currentScene.pointsGTData.finished)
        {
           //Debug.Log("Race has finished");
            StartCoroutine(finishRace());
        }
        else
        {
            //Debug.Log("Race hasn't finished" + displayManager.currentScene.pointsGTData.finished);
        }
        

    }

    private void resetCars()
    {
        /*
        Vector3 tmpVector3;

        int plc = 0;
        foreach (GameObject car in Cars)
        {

            if (plc < 10)
            {

                tmpVector3 = car.transform.localPosition;
                tmpVector3.y = 0;
                car.transform.localPosition = tmpVector3;
                car.GetComponent<RaceCar>().ActivateKey(0, false);
                car.GetComponent<RaceCar>().ActivateKey(1, false);
                car.GetComponent<RaceCar>().ActivateKey(2, false);
            }
            plc++;
        }
        */
        switch (displayManager.currentScene.pointsGTData.SpriteAtlas)
        {
            case 4:
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
        
    }
}
