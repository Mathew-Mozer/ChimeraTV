using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using UnityEngine.Assertions.Must;
using System.Text;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using Newtonsoft.Json.Linq;
using TMPro;

/// <summary>
/// <author>Mathew Mozer</author>
/// <date>9/9/2015</date>
/// <version>1.0</version>
/// 
/// This controls access to the ChimeraTV system and downloading of assets
/// required to run the system.
/// 
/// UPDATE
/// <author>Stephen King</author>
/// <date>6/15/2016</date>
/// <version>1.01</version>
/// 
/// I'm updating this to add comments and change the visual loading notification system. The layout
/// and graphics of the login screen have also changed as of this date.
/// </summary>

public class DisplayManager : MonoBehaviour
{

    //Constants
    const string LOADED_SPRITE = "ProgressArrowActive";

    //Initialization variables
    public string url;
    public string linkCode = "";
    public string GCMID;
    public float APIVersion;
    public int currentSceneID;
    public scene currentScene;
    public int currentSceneSequenceID = 0;

    internal void restartApp()
    {
        Application.Quit();
    }

    public UILabel loadinformation;
    public bool wifiStatus = true;
    //Server Object Variables
    //public DisplayInfo displayInfo;
    //public SkinElements skinElements;
    public DateTime currentTime;
    //If these variables are true then get information from the DB
    public bool updateSettings;
    public bool updateSkins;
    //UI Elements
    public GameObject NoPromo;
    public GameObject Offline;
    public UILabel boxID;
    //Loading Checkboxes
    public UISprite settingsLoaded;
    public UISprite skinsLoaded;
    public UISprite TexturesLoaded;
    public UISprite gcmIsRegistered;
    public BackgroundParticleManager bgParticleManager;
    //Status Bools
    public bool gcmRegistered;
    public bool hasErrors = false;
    private bool fullyloaded = false;
    public bool pauseLoop = false;
    public bool adminPause = false;
    private bool downloadAssetBundle = true;
    //float timer = 0f;
    float waitTimer = 1f;
    float sceneTimer = 0f;
    public UILabel dbug;
    public float defaultTimer = 5f;
    public DailyJackpot jodo;
    public DateTime lastUpdated;
    //scene specific variables
    //public highHand TheHand;
    public SceneClock sceneClock = new SceneClock();
    public List<PictureData> CurrentPictures = new List<PictureData>();
    //GCM Variables
    AndroidJavaClass cls_UnityPlayer;
    AndroidJavaObject obj_Activity;
    public int currentPrizeEventID = 0;
    private int currentSecond;
    public SceneSkin currentSceneSkin;
    public string macAddress = "";
    //Tracks completed assets
    bool[] dataTransfer = new bool[] { false, true, false, true };
    //For Json Conversion
    public string apiURL;
    //If lastupdated changes then reset scene
    public bool updateMatchMadness;
    public bool updateHighHand;
    public bool updateKickForCash;
    public bool updatePointsGT;
    //Will update scene if timestamp changes
    private string lastUpdatedMatchMadness = "";
    private string lastUpdatedHighHand = "";
    private string lastUpdatedKickForCash = "";
    private string lastUpdatedPointsGT = "";
    //Current Scene Vars
    private string lastUpdateScene = "";
    public bool updateSceneDataNow;
    public UILabel noPromoLabel;
    // Debug View Objects
    public GameObject debugWindow;
    public UILabel Timer;
    public UILabel errormessagelog;
    public UILabel errCntLog;
    public int wwwErrors;
    
    //
    public static DisplayManager displayManager;
    //
    double minusSeconds = 1;
    int updateTimer = 60;
    int synctime = 60;
    private bool startup;
    private bool downloadingStarted;
    private bool firstdownloaded;
    string timerSecond;
    public Texture2D noImageTexture;
    public TextMeshPro DisplayName;
    private string lastOnlineTimeStamp;
    public GameObject currentManager;
    public TextureManager textureManager;

    /// <summary>
    /// When app is awake this executes.
    /// </summary>

    void Awake()
    {
        textureManager = gameObject.GetComponent<TextureManager>();
        LoadMacAddress();
        UpdateErrorCount();
        CreateSingleton();
        //Make device never go to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        setAPIurl();
#if UNITY_ANDROID && !UNITY_EDITOR
                FileTools.directorySeperator="/";
#endif
        //Verifies Directory For Images is available
        if (!Directory.Exists(FileTools.PicturePath() + FileTools.directorySeperator))
        {
            Directory.CreateDirectory(FileTools.PicturePath() + FileTools.directorySeperator);
        }
    }

    public TextureManager GetTextureManager()
    {
        return textureManager;
    }
    private void SetupFireBaseListeners()
    {
        displayRef.ValueChanged += HandleServerValueChanged;
    }

    bool serverTime;
    private bool logErrors;

    private void HandleServerValueChanged(object sender, ValueChangedEventArgs e)
    {
        serverTime = (bool)e.Snapshot.Child("ServerTime").Value;
        logErrors = (bool)e.Snapshot.Child("logErrors").Value;
        addtodebug("ServerTime Value Changed to:" + serverTime);
        addtodebug("logErrors Value Changed to:" + logErrors);
    }

    DatabaseReference connectedRef;
    DatabaseReference logRef;
    DatabaseReference connectedRef2;
    DatabaseReference serverConRef;
    DatabaseReference displayRef;
    private void FireBaseSetup()
    {
        
        if (FirebaseDisplayKey != null)
        {
            // Set up the Editor before calling into the realtime database.
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chimeratvhome.firebaseio.com/");
            // Get the root reference location of the database.
            connectedRef = FirebaseDatabase.DefaultInstance.GetReference("CurrentConnections/" + displayData.DisplayName + "(" + linkCode + ")");
            logRef = FirebaseDatabase.DefaultInstance.GetReference("DisplayLogs/" + linkCode);
            connectedRef2 = FirebaseDatabase.DefaultInstance.GetReference("Displays/" + FirebaseDisplayKey + "/Connected");
            displayRef = FirebaseDatabase.DefaultInstance.GetReference("Displays/" + FirebaseDisplayKey + "");
            serverConRef = FirebaseDatabase.DefaultInstance.GetReference(".info/connected");
            serverConRef.ValueChanged += HandlePresenceChanged;
            displayRef.Child("ctvfirebasetoken").SetValueAsync(fireBaseToken);
            displayRef.Child("ctvVersion").SetValueAsync(Application.version);
            if (!FirebaseStartup)
            {
                addToDisplayLog("Firebase Activated", "Startup");
                FirebaseStartup = true;
                SetupFireBaseListeners();
            }
            
        }
        else
        {
            
            getFireBaseDisplayKey();
        }
    }

    private void addToDisplayLog(string details, string cat)
    {
        if (logErrors)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        
        DisplayLog displayLog = new DisplayLog();
        displayLog.Details = details;
        displayLog.Category = cat;
        displayLog.Application = "ChimeraTV";
        //displayLog.timestamp = ServerValue.Timestamp;
        DatabaseReference currentCon = logRef.Push();
        JObject jo = JObject.FromObject(displayLog);
        jo.Add("Timestamp", JObject.Parse("{ '.sv': 'timestamp' }"));
        currentCon.SetRawJsonValueAsync(jo.ToString());
        addtodebug("addToDisplayLog Called:" + details);
        Debug.Log("addToDisplayLog Called:" + details);
#endif
        }
    }

    private string FirebaseDisplayKey;
    private void getFireBaseDisplayKey()
    {
        
        #if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            FirebaseDatabase.DefaultInstance.GetReference("Displays")
            .OrderByChild("LinkCode")
            .EqualTo(linkCode)
            .GetValueAsync().ContinueWith(task =>
            {
                
                if (task.IsFaulted)
                {
                
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                
                    DataSnapshot snapshot = task.Result;
                
                    Debug.Log(snapshot.GetRawJsonValue().ToString());
                    foreach (var childSnapshot in snapshot.Children)
                    {
                
                        FirebaseDisplayKey = childSnapshot.Key;
                        Debug.Log("linkcode: " + linkCode + "  -FirebaseKey:" + FirebaseDisplayKey);
                    }
                }
            });
        }
        catch (Exception tmp)
        {
            addtodebug("Exception:" + tmp);
        }
        
#endif
        
    }

    void HandlePresenceChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            addtodebug("line 267" + args.DatabaseError.Message);
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        DatabaseReference currentCon = connectedRef.Push();
        connectedRef.OnDisconnect().RemoveValue();
        connectedRef2.OnDisconnect().SetValue(false);
        markAsConnected();
        displayRef.Child("/LastConnected").OnDisconnect().SetValue(Firebase.Database.ServerValue.Timestamp);

    }

    public void markAsConnected()
    {
        if (connectedRef != null && connectedRef2 != null)
        {
            connectedRef.SetValueAsync(true);
            connectedRef2.SetValueAsync(true);
        }
        else
        {
            FireBaseSetup();
        }

    }
    public IEnumerator downloadImg(string FileName,GameObject responseGameObject)
    {
        yield return 0;
        WWW imgLink = new WWW(FileName);
        yield return imgLink;
        if (imgLink.error == null)
        {
            string fullPath = "";
            fullPath = FileTools.PicturePath() + FileTools.getFilename(FileName);
//            Debug.Log("BKI Saving to: " + fullPath);
            File.WriteAllBytes(fullPath, imgLink.bytes);
            BroadcastMessageToManager("toManager", "Downloaded-Background");
            if (RestaurantManager)
            {
            //    displayManager.RestaurantManager.BroadcastMessage("UpdateMenuChild");
            }
        }
        else
        {
            Debug.Log("www error:" + imgLink.error);
        }
    }
    private void GetPictureData()
    {

        List<string> newTextureList = new List<string>();
        List<string> removeList;
        //DON'T FORGET TO IMPLEMENT A CLEANUP METHOD
        foreach (scene scenes in displayData.SceneList)
        {
            if (scenes.PictureViewerData != null)
            {
                //folder cleanup
                /*
                foreach (string file in System.IO.Directory.GetFiles(FileTools.PicturePath() + FileTools.directorySeperator))
                {
                    int index = scenes.PictureViewerData.PictureList.FindIndex(s => getFilename(s.FileName).Equals(getFilename(file)));
                    if (index >= 0)
                    {
                        //Debug.Log("file Found: " + getFilename(file));
                    }
                    else
                    {
                        //Debug.Log("delete file because its not in the list" + getFilename(file));
                        File.Delete(picturePath + directorySeperator + scenes.promoID + directorySeperator + getFilename(file));
                    }
                }
                */
                // Add Files To New List. Will Verify Later
                //Debug.Log("Path:" + FileTools.PicturePath());
                foreach (PictureData pictures in scenes.PictureViewerData.PictureList)
                {
                    //Debug.Log("Adding File To Texture List: " + FileTools.GetFileFromURL(pictures.FileName));
                    //newTextureList.Add(pictures.FileName);
                    textureManager.LoadTexture(pictures.FileName, gameObject);
//                    textureManager.DownloadBackgroundTexture(pictures.FileName,gameObject);
                }
                //removeList = newTextureList.Except(textureManager.AllTextures).ToList();
                /*
                foreach (string picture in removeList)
                {
                    //Debug.Log("Deleting Image:" + picture);
                }
                */
                //textureManager.AllTextures = newTextureList;
            }

        }
    }
    private void setAPIurl()
    {

        url = "";// PlayerPrefs.GetString("APIUrl").Trim();
        if (url.Equals(""))
            url = apiURL + APIVersion.ToString().Replace(".", "") + "/";

    }

    /// <summary>
    /// When app starts up this executes.
    /// </summary>
    void Start()
    {
        defaultTimer = 5f;
        currentTime = System.DateTime.Now;
        InvokeRepeating("minusSec", 0, 1f);
        StartCoroutine(StartSyncLoop());
        waitTimer = defaultTimer;
    }
    /// <summary>
    /// Creates and allows one instance of this object.
    /// </summary>
    private void CreateSingleton()
    {
        if (displayManager == null)
        {
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
            displayManager = this;
        }
        else if (displayManager != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets the mac address depending on the platform.
    /// </summary>
    private void LoadMacAddress()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            macAddress = GetWindowsMac();
            if (macAddress.Equals(""))
            {
                macAddress = "00000000000000E0";
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            macAddress = AndroidTools.ReturnMacAddress();
            //Debug.Log("here is the Android mac:" + macAddress);
        }
    }
    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Gets the mac address if platform is Windows.
    /// </summary>
    /// <returns>Mac Address</returns>
    private string GetWindowsMac()
    {
        var macAdress = "";
        foreach (NetworkInterface nics in NetworkInterface.GetAllNetworkInterfaces())
        {
            macAdress = nics.GetPhysicalAddress().ToString();

        }
        return macAdress;
    }

    /// <summary>
    /// Retrieves settings of the display from the API
    /// </summary>
    /// <returns> Settings </returns>
    private IEnumerator StartSyncLoop()
    {
        linkCode = PlayerPrefs.GetString("linkCode").Trim();
        if (linkCode.Length < 6)
        {
            linkCode = "";
        }
        //addtodebug("StartupLinkcode:" + linkCode);
        while (true)
        {
            //Check for the link code. If its found Load the rest of the Data.

            if (linkCode.Equals(""))
            {

                StartCoroutine(getLinkCode());

            }
            else
            {
                
                
                CheckForWifi();
                getSettings();
                FireBaseSetup();
            }
            yield return new WaitForSeconds(waitTimer);

        }

    }

    public void getSettings()
    {
        StartCoroutine(LoadSettingsJson());

    }

    public void DownloadAssetBundle()
    {
        if (!CheckLoaded() && displayData.propertyID != 0)
            StartCoroutine(DownloadAndCacheSkinAtlas());
    }
    private void CheckForWifi()
    {
        
#if UNITY_ANDROID && !UNITY_EDITOR

        if (!AndroidManager.isWifiEnabled())
        {
            if (allowWifiRestart())
            {
                addtodebug("Wifi wasn't on at startup");
                StartCoroutine(ResetWifi());
            }
        }
    
#endif
    }

    /// <summary>
    /// Setter for LinkCode Variable
    /// </summary>
    /// <param name="linkcode"></param>
    public void setLinkCode(string linkcode)
    {
        linkCode = linkcode;
    }

    

    /// <summary>
    /// Retrieves all data for display
    /// </summary>
    /// <returns> Json file from API </returns>
    /// 
    private IEnumerator LoadSettingsJson()
    {
        
        var form = new WWWForm();
        form.AddField("action", "GetSettings");
        form.AddField("appVersion", Application.version);
        //form.AddField("macAddress", macAddress);
        form.AddField("linkcode", linkCode);

        // Start a download of the given URL
        WWW www = new WWW(url, form);
        // Wait for download to complete
        yield return www;

        // Checks for WWW error
        if (!string.IsNullOrEmpty(www.error))
        {
            addtodebug("found error");
            //Puts error information into debug window
            HandleWWWErrors(www);
            setDeviceOnlineStatus(false);
            tryOfflineLoading();
        }
        else
        {

            setDeviceOnlineStatus(true);
            wwwData = www.text;
            //Set time used for timer from server response header

            
            
            //Turn Data from API into Object
            DeserializeData(www.text);

            //Activates loading sprite
            SetSettingsLoadedSprite();


            //Update Scene Specific Data (check timestamps)

            if (displayData != null)
            {
                startInitialSceneSetup();
                if (wwwErrorList.Count > 0)
                {
                    wwwErrorList.Clear();
                    addToDisplayLog("Reconnected from WWW", "WWW");
                }
            }

        }
    }

    private bool isYearBad()
    {
        if (System.DateTime.Now.Year < 2018)
        {
            return true;

        }else
        {
            return false;
        }
        
    }

    private void setDeviceOnlineStatus(bool v)
    {
        deviceOnline = v;
        Offline.SetActive(!v);

    }

    private void startInitialSceneSetup()
    {
        
        DownloadAssetBundle();
        if (displayData.flip)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;

        }

        UpdateCurrentSceneData();
        setDisplayURL();
        GetPictureData();
        getRestaurantMenuData();
        //CleanupPictureDirectories();
        if (displayData.propertyID == 0 || displayData.SceneList.Count == 0)
        {
            ActivateNoPromoWindow();

        }
        else
        {

            DeactivateNoPromoWindow();

            // where we left off                   

            if (CheckLoaded())
            {

                InitialSceneStartup();

            }
        }
    }

    private void tryOfflineLoading()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (File.Exists(Application.persistentDataPath + FileTools.directorySeperator + "settings.json"))
            {
                StreamReader reader = new StreamReader(Application.persistentDataPath + FileTools.directorySeperator + "settings.json");
                string file = reader.ReadToEnd();
                DeserializeData(file);
                SetSettingsLoadedSprite();
                startInitialSceneSetup();
            }
        }
    }

    private void CleanupPictureDirectories()
    {
        Debug.Log("cleanup directories");
        foreach (string directory in System.IO.Directory.GetDirectories(FileTools.PicturePath() + FileTools.directorySeperator))
        {
            int index = displayData.SceneList.FindIndex(s => s.promoID == int.Parse(FileTools.getFilename(directory)));
            if (index >= 0)
            {
                //Debug.Log("folder Found: " + getFilename(directory));
            }
            else
            {
                //Debug.Log("delete folder because scene doesn't exist anymore:" + getFilename(directory));
                //Directory.Delete(FileTools.PicturePath()() + FileTools.directorySeperator + FileTools.getFilename(directory) + FileTools.directorySeperator, true);
                //File.Delete(FileTools.FileTools.PicturePath()() + "\\" + scenes.promoID + "\\" + getFilename(file));
            }
        }
    }

    private void setDisplayURL()
    {

        if (displayData.propertyID == 0)
        {
            url = apiURL + APIVersion.ToString().Replace(".", "") + "/";

        }
        else
        {
            if (!displayData.apiurl.Equals(apiURL))
            {
                url = displayData.apiurl + APIVersion.ToString().Replace(".", "") + "/";
                downloadAssetBundle = true;
                UpdateCurrentSceneData();

            }
            else
            {
                url = displayData.apiurl + APIVersion.ToString().Replace(".", "") + "/";
            }
        }
        PlayerPrefs.SetString("APIUrl", url);
    }

    private void PauseLoop()
    {
        pauseLoop = true;
        Scene tmpscene = SceneManager.GetActiveScene();
        if (tmpscene.buildIndex != displayData.lockedScene)
        {
            currentScene = new scene();
            currentScene.setActive();
            currentScene.sceneID = displayData.lockedScene;
            SceneManager.LoadScene(displayData.lockedScene);
            updateMatchMadness = true;
        }
    }

    private void UnpauseLoop()
    {
        pauseLoop = false;
        currentScene.duration = 0;
        addtodebug("unpauseloop");
        nextScene(1);
    }

    private void InitialSceneStartup()
    {
        if (!startup)
        {
            startup = true;

            nextScene(1);
        }
    }

    private void UnlockDisplay()
    {

        prevLockedSceneID = displayData.lockedScene;
        adminPause = false;
    }
    private void DeactivateNoPromoWindow()
    {
        if (NoPromo != null)
        {
            NoPromo.SetActive(false);
        }
    }
    private void ActivateNoPromoWindow()
    {
        bool allowToLog = false;
        startup = false;
        currentSceneSequenceID = 0;
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
        if (!NoPromo.activeSelf)
        {
            NoPromo.SetActive(true);



            if (displayManager.displayData.propertyID == 0)
            {
                noPromoLabel.text = "This Display is unassigned. Please assign it to a property";

                addToDisplayLog("Unassigned Display", "Startup");
                allowToLog = false;
            }
            else
            {
                noPromoLabel.text = "This Display does not have any assigned promotions";

                addToDisplayLog("No promotions assigned", "Startup");
                allowToLog = false;
            }
        }
        boxID.text = displayData.DisplayName + " - " + linkCode + "\n" + macAddress + "\n" + url;
        ;
        if (dbug)
        {
            //dbug.text = "No Promotions Available";
            //addtodebug("No Promotions Available");
        }
    }

    private void UpdateCurrentSceneData()
    {
        if (currentScene != null && displayData.SceneList != null && currentScene.sceneID != 0)
        {
            scene foundScene = displayData.SceneList.Find(s => s.promoID.Equals(currentScene.promoID));
            if (foundScene != null)
            {
                if (foundScene.lastUpdated != currentScene.lastUpdated || foundScene.promotionStatus != currentScene.promotionStatus)
                {

                    currentScene = foundScene;
                    currentScene.setActive();
                    updateSceneDataNow = true;
                    UpdateSceneTimer();
                    saveSettingsFile();

                }
            }
        }
    }

    /// <summary>
    /// Converts downloaded data to DisplayData Object
    /// </summary>
    /// <param name="www"></param>
    private string wwwData;
    private void DeserializeData(string www)
    {
        try
        {
            displayData = JsonConvert.DeserializeObject<DisplayData>(www);
            if (displayData.DisplayName.Equals("") || displayData == null)
            {
                displayData.DisplayName = "Unnamed - " + macAddress;
            }
            if (deviceOnline)
            {
                if (!lastUpdatedDisplay.Equals(System.DateTime.Now.Hour.ToString()))
                {
                    saveSettingsFile();
                }
                if (currentScene.promoID != displayData.lockedScene && displayData.lockedScene!=0)
                {
                    addtodebug(currentScene.promoID + " " + displayData.lockedScene);
                    Debug.Log(currentScene.promoID + " " + displayData.lockedScene);
                    nextScene(1);
                }
            }
            lastUpdated = System.DateTime.Now;
            if (serverTime)
            {
                currentTime = displayData.TimeFromServer;
            }
            else
            {
                if (!isYearBad())
                {
                    currentTime = System.DateTime.Now;
                
                }
                else
                {
                    currentTime = displayData.TimeFromServer;
                
                }
            }


        }
        catch (JsonReaderException e)
        {
            //addtodebug(e.Message);
            //addtodebug(www);

        }
    }

    private void saveSettingsFile()
    {
        lastUpdatedDisplay = System.DateTime.Now.Hour.ToString();
        saveToFile(wwwData, "settings.json");
        //addtodebug("saving: " + lastUpdatedDisplay + " != " + System.DateTime.Now.Hour);
    }

    private void saveToFile(string www, string v)
    {
        //addtodebug("saved settings file");
        File.WriteAllBytes(Application.persistentDataPath + FileTools.directorySeperator + v, Encoding.ASCII.GetBytes(www));
    }

    /// <summary>
    /// Adds errors to debug screen and resets wifi(later)
    /// </summary>
    /// <param name="www"></param>
    public List<string> wwwErrorList = new List<string>();
    private void HandleWWWErrors(WWW www)
    {
        //addtodebug("LoadSettings:" + www.error);
        if (!wwwErrorList.Contains(www.error))
        {
            wwwErrorList.Add(www.error);
          //  Debug.Log("adding To List:" + www.error);
            addToDisplayLog(www.error, "WWW");
        }
        else
        {
            //Debug.Log("Not! Adding To List:" + www.error);
        }
        addtoWWWerrorCount();

        if (allowWifiRestart())
        {

            if (AndroidManager.isWifiEnabled())
            {
                StartCoroutine(ResetWifi());
            }
        }
    }

    private bool allowWifiRestart()
    {

        TimeSpan lapsedTime = System.DateTime.Now - lastUpdated;
        if (lapsedTime.Minutes > 0)
        {
            return true;
        }
        return false;
    }


    IEnumerator ResetWifi()
    {
        lastUpdated = System.DateTime.Now;
        //addtodebug("[00ff00]Turning off WiFi[-]");
        AndroidManager.setWifiEnabled(false);
        yield return new WaitForSeconds(5);
        AndroidManager.setWifiEnabled(true);
        //addtodebug("[00ff00]Turning On Wifi[-]");

    }



    /// <summary>
    /// Adds a negative second?
    /// </summary>
    void minusSec()
    {
        currentTime = currentTime.AddSeconds(minusSeconds);

        //currentTime = 
    }


    void Update()
    {
        
        //Quit when back button is pressed
        //Standard change scene code. Will not change scene if done manually until unpaused.
        //Debug.Log("SceneTimer:" + sceneTimer);
        if (!adminPause && startup && !pauseLoop)
        {
            //sceneTimer -= Time.fixedDeltaTime;

            string[] scenetimes = System.DateTime.Now.ToString("h:mm:ss").Split(':');
            if (scenetimes[2] != scenetimerSecond)
            {
                scenetimerSecond = scenetimes[2];
                sceneTimer -= 1;

            }
            if (sceneTimer <= 0)
            {
                //Debug.Log("change scene with Timer");

                nextScene(0);
            }

            Timer.text = sceneTimer.ToString();
        }
        else
        {
            if (displayData != null)
            {
                //displayInfo = new DisplayInfo();
                /*
                if (displayInfo.lockedScene != 0)
                {

                }
                else
                {
                    if (adminPause)
                    {
                        Timer.text = "||";
                    }
                    else
                    {
                        Timer.text = "L";
                    }

                }
                */
            }

        }
    }



    /// <summary>
    /// Gets the link code
    /// </summary>
    /// <returns></returns>
    IEnumerator getLinkCode()
    {
        //Debug.Log("Getting Link Code." + url);
        var form = new WWWForm();
        form.AddField("action", "GetLinkCode");
        form.AddField("macAddress", macAddress);

        // Start a download of the given URL
        WWW www = new WWW(url, form);

        // Wait for download to complete
        yield return www;

        if (www.error != null)
        {

            addtodebug("LinkCode Error:" + www.error);
            hasErrors = true;
        }
        else
        {
            linkCode = www.text.Trim();
            if (!linkCode.Equals(""))
            {
                PlayerPrefs.SetString("linkCode", linkCode);
                addtodebug("link code is saved:" + linkCode);
            }
            else
            {
                addtodebug("link code is blank");
            }

        }

    }

    private void addtoWWWerrorCount()
    {
        wwwErrors++;
        UpdateErrorCount();

    }

    private void UpdateErrorCount()
    {
        errCntLog.text = "[ff0000]WWW Errors:[-]" + wwwErrors + "\n";
    }

    /// <summary>
    /// I assume this loads settings
    /// </summary>
    /// <returns></returns>


    private void UpdateSceneTimer()
    {
        switch (currentScene.sceneID)
        {
            case 7:
                sceneTimer = currentScene.sceneDuration;
                sceneClock.active = currentScene.active;
                sceneClock.TimerType = currentScene.highHandData.sessionTimer;
                sceneClock.isHrOdd = currentScene.highHandData.isOdd;
                sceneClock.SecondsToHorn = currentScene.highHandData.secondstohorn;
                break;
            case 8:
                sceneTimer = currentScene.sceneDuration;
                sceneClock.active = currentScene.active;
                sceneClock.TimerType = currentScene.PrizeEventData.TimerType;
                sceneClock.isHrOdd = currentScene.PrizeEventData.isOddHr;
                sceneClock.SecondsToHorn = currentScene.PrizeEventData.secondsToHorn;
                break;
            case 9:
                sceneTimer = currentScene.sceneDuration;
                sceneClock.active = currentScene.active;
                sceneClock.TimerType = currentScene.highHandData.sessionTimer;
                sceneClock.isHrOdd = currentScene.highHandData.isOdd;
                sceneClock.SecondsToHorn = currentScene.highHandData.secondstohorn;
                break;
            case 2://High Hand Version 1
                sceneTimer = currentScene.sceneDuration;
                sceneClock.active = currentScene.active;
                sceneClock.TimerType = currentScene.highHandData.sessionTimer;
                sceneClock.isHrOdd = currentScene.highHandData.isOdd;
                sceneClock.SecondsToHorn = currentScene.highHandData.secondstohorn;
                break;
            case 4://PointsGT
                sceneTimer = currentScene.sceneDuration * 17;
                break;
            case 5://Picture Slideshow
                int dur = 0;
                foreach (PictureData picture in currentScene.PictureViewerData.PictureList)
                {
                    dur += picture.Duration;
                }
                sceneTimer = dur * currentScene.sceneDuration;
                break;
            case 11://KickForCash
                sceneTimer = currentScene.sceneDuration * 18;
                break;
            case 17://RedCarpet
                sceneTimer = (currentScene.sceneDuration * currentScene.DisplayListData.ListData.Count)*.75f+7;
                break;
            default:
                sceneTimer = currentScene.sceneDuration;
                break;

        }
    }

    private void SetSettingsLoadedSprite()
    {
        if (settingsLoaded != null)
        {
            settingsLoaded.spriteName = LOADED_SPRITE;
            dataTransfer[0] = true;
        }
    }

    private void setTimeFromServer(Dictionary<string, string> responseHeaders)
    {
        if (responseHeaders != null)
        {
            if (responseHeaders.Count > 0)
            {
                if (serverTime)
                {
                    currentTime = DateTime.Parse(responseHeaders["DATE"].ToString());
                }
                else
                {
                    currentTime = System.DateTime.Now;
                }


            }
        }

    }

    static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }
        result.Apply();
        return result;
    }
    
    private int prevLockedSceneID;
    private string scenetimerSecond;
    public DisplayData displayData;
    public bool updateDimensions;
    private string fireBaseToken;
    private string lastUpdatedDisplay = "";
    private bool deviceOnline = false;
    public GameObject RestaurantManager;
    public Dictionary<int,RestaurantMenuContainer> RestaurantLayouts = new Dictionary<int, RestaurantMenuContainer>();
    private object promoID;

    public bool FirebaseStartup { get; private set; }

    private void getRestaurantMenuData()
    {

        foreach (scene scenes in displayData.SceneList)
        {
//            Debug.Log("Dictionary Count:" + RestaurantLayouts.Count);
  //          Debug.Log("Scene Check: " + scenes.sceneID + " PromoID:" + scenes.promoID + " scene Type: " + scenes.sceneType);
            RestaurantMenuContainer value;
            switch (scenes.sceneID)
            {
                case 16:
                    
                    if (!RestaurantLayouts.TryGetValue(scenes.promoID,out value))
                    {
                        RestaurantMenuContainer rmc = gameObject.AddComponent<RestaurantMenuContainer>();
                        rmc.LoadData(scenes.promoID);
                        RestaurantLayouts.Add(scenes.promoID, rmc);
                        //Debug.Log("Added Layout");
                    }
                    else
                    {
                        //Debug.Log("Found key already count:" + RestaurantLayouts.Count);
                    }
                    break;
                default:
                    break;
                    

            }
            
        }
    }

    public void nextScene(int v)
    {
        //addtodebug("next scene");
        //StartCoroutine(loadscene());
        loadNextScene();

    }

    private void loadNextScene()
    {
        if (displayData.lockedScene == currentScene.promoID)
        {
            
            switch (currentScene.sceneID)
            {
                case 16:
                    
                    break;
                default:
                    
                    ChangeScene();
                    break;

            }

        }
        else
        {
            
            if (displayData.SceneList.Count == 1 && displayData.SceneList[0].sceneID == 16)
            {
                if (currentScene.sceneID != 16)
                {
                    ChangeScene();
            
                }
                else
                {
            
                }
                
            }
            else {
                ChangeScene();
            }
        }
        
    }

    private void ChangeScene()
    {
        if (displayData.SceneList.Count > 0)
        {
            if (currentSceneSequenceID > displayData.SceneList.Count - 1)
            {
                currentSceneSequenceID = 0;
            }
            currentScene = displayData.SceneList[currentSceneSequenceID];
            currentScene.setActive();
            
            SceneManager.LoadScene(currentScene.sceneID);
            bgParticleManager.ActivateBackgroundParticle(currentScene.EffectID);
            currentSceneSequenceID++;
            //Load specific details about the scene configuration
            UpdateSceneTimer();
            if (connectedRef2 != null)
            {
                connectedRef2.SetValueAsync(true);
            }



            if (currentSceneSequenceID > displayData.SceneList.Count - 1)
            {
                currentSceneSequenceID = 0;
            }
        }
    }

    public void PrevScene()
    {
        //StartCoroutine(loadPrevScene());
        loadPreviousScene();

    }

    private void loadPreviousScene()
    {
        if (displayData.SceneList.Count > 0)
        {
            currentScene = displayData.SceneList[currentSceneSequenceID];
            SceneManager.LoadScene(currentScene.sceneID);
            bgParticleManager.ActivateBackgroundParticle(currentScene.EffectID);
            currentSceneSequenceID++;
            //Load specific details about the scene configuration
            UpdateSceneTimer();

            if (currentSceneSequenceID > displayData.SceneList.Count - 1)
            {
                currentSceneSequenceID = 0;
            }
        }
    }

    IEnumerator DownloadAndCacheSkinAtlas()
    {
        string BundleVer = "0";
        string URL = "";
        string AssetName = "";
        bool jSon = false;
        


        if (displayData != null && !displayData.BundleAndroidUrl.Equals(""))
        {

            BundleVer = displayData.BundleVer;
            AssetName = displayData.AssetName;

            if (downloadAssetBundle)
            {
                Debug.Log("downloading new asset bundle");
                waitTimer = defaultTimer;
                GameObject tmpObject = new GameObject();
                // Wait for the Caching system to be ready
                while (!Caching.ready)
                    yield return null;
                // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
                //Debug.Log("downloading skin at:" + URL);
                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    URL = displayData.BundleWindowsURL;
                }
                else if (Application.platform == RuntimePlatform.Android)
                {

                    URL = displayData.BundleAndroidUrl;
                }

                using (WWW www = WWW.LoadFromCacheOrDownload(URL, int.Parse(BundleVer)))
                {
                    yield return www;
                    if (www.error != null)
                    {
                        hasErrors = true;
                        
                        throw new Exception(www.error);


                    }
                    AssetBundle bundle = www.assetBundle;

                    if (AssetName == "")
                        Instantiate(bundle.mainAsset);
                    else
                        tmpObject = (GameObject)Instantiate(bundle.LoadAsset(AssetName)) as GameObject;
                    tmpObject.name = "Skin";
                    tmpObject.tag = "Skin";
                    downloadAssetBundle = false;
                    DontDestroyOnLoad(tmpObject.transform);
                    if (TexturesLoaded != null)
                    {
                        TexturesLoaded.spriteName = LOADED_SPRITE;
                        dataTransfer[2] = true;
                    }
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle.Unload(false);

                } // memory is freed from the web stream (www.Dispose() gets called implicitly)
            }
        }
    }

    /// <summary>
    /// This checks to see if all the resources have been downloaded and applied
    /// successfully.
    /// </summary>
    /// <returns></returns>
    private bool CheckLoaded()
    {
        //Debug.Log("checkloaded");
        //bool[] allbools = new bool[]{settingsLoaded.value, skinsLoaded.value, TexturesLoaded.value };
        fullyloaded = dataTransfer.All(x => x);
        return fullyloaded;
    }

    /// <summary>
    /// Unfinished GCM integration
    /// </summary>
    /// <param name="gcmMessage"></param>
    public void GCMReceiver(string gcmMessage)
    {
        Debug.Log("gcm: " + gcmMessage);
        addtodebug("gcm: " + gcmMessage);
        string[] tmp = gcmMessage.Split(';');

        if (gcmMessage.Contains("GCMRegistered"))
        {
            GCMID = obj_Activity.CallStatic<string>("getRegID");
            //gcmIsRegistered.spriteName = LOADED_SPRITE;
            //dataTransfer[3] = true;
            //gcmRegistered = true;
            Debug.Log("Saved GCM");
            print("saved GCM");
        }
        else
        {
            //addLoadInfo(gcmMessage);
            print("TXT:" + gcmMessage);
            //string[] type = tmp[4].Split('=');
            string[] msg = tmp[2].Split('=');

            switch (msg[1])
            {
                case "StartSyncLoop":
                    StartCoroutine(LoadSettingsJson());
                    break;
                case "getHighHand":
                    //StartCoroutine(GetHighHand());
                    break;
            }

        }
    }

    /// <summary>
    /// A debugger?
    /// </summary>
    /// <param name="msg"></param>
    public void addtodebug(string msg)
    {
        //if (!errormessagelog.text.Contains(msg))
        errormessagelog.text += "[ffffff]" + currentTime + ": " + msg + "[-]\n";
        Debug.Log(msg);
    }

    internal void ToggleDebug()
    {
        debugWindow.SetActive(!debugWindow.activeSelf);
        Debug.Log("clicked!");
    }

    internal void setFireBaseToken(string token)
    {
        fireBaseToken = token;
        StartCoroutine(updateFireBaseToken());

    }

    private IEnumerator updateFireBaseToken()
    {
        var form = new WWWForm();
        form.AddField("action", "UpdateFireBaseToken");
        form.AddField("macAddress", macAddress);
        form.AddField("FireBaseToken", fireBaseToken);

        // Start a download of the given URL
        WWW www = new WWW(url, form);
        // Wait for download to complete
        yield return www;
        //addtodebug("Updated Token: " + www.text);
    }
    public static Color HexToColor(string hex)
    {
        if (hex.Contains("#"))
        {
            hex = hex.Remove(0,1);
        }
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    internal void setRestaurantManager(GameObject gameObject)
    {
        RestaurantManager = gameObject;
        

    }

    internal void downloadImage(string filename,GameObject responseGameObject)
    {
        StartCoroutine(downloadImg(filename,responseGameObject));
    }

    internal void BroadcastMessageToManager(string Method,string Message)
    {
        currentManager.BroadcastMessage(Method,Message);
    }
}

