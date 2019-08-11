using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class DisplayManagerold : MonoBehaviour
{
    public DisplayInfo displayInfo;
    public string linkCode = "";
    public scene currentScene;
    public int currentSceneSequenceID = 0;
    public GameObject tablesign;
    public List<string> sceneEffectTags = new List<string>();
    public int loadPercent;
    public UILabel loadinformation;
    public string url;
    public highHand TheHand = ScriptableObject.CreateInstance("highHand") as highHand;
    public UIToggle settingsLoaded;
    public UIToggle skinsLoaded;
    public UIToggle TexturesLoaded;
    public UIToggle gcmIsRegistered;
    public bool hasErrors = false;
    private bool fullyloaded = false;
    private bool pauseLoop = false;
    //public bool refreshData;
    private int refreshInt = 1;
    //public Skin skins;
    public SkinElements skinElements;
    //GCM Vars
    AndroidJavaClass cls_UnityPlayer;
    AndroidJavaObject obj_Activity;
    public UILabel gcmstatus;
    private bool gcmRegistered;
    private bool manualUpdateSkin;
    private bool manualUpdateSettings;
    public bool updateHH;
    public string GCMID;
    void Awake()
    {
        //GCM Initializers
         #if UNITY_ANDROID && !UNITY_EDITOR
        cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");			//Grabs the Android Unity Player
        obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");	//Grabs current Android activity
        obj_Activity.CallStatic("setListener", new object[] { "DisplayManager", "GCMReceiver" });
        obj_Activity.CallStatic("registerDeviceWithGCM", new object[] { "398299964412" });
        #endif
        //Done GCM
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        url = "http://connect.typhonpacificstudios.com/tv/" + Application.version.Replace(".", "") + "/";
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    IEnumerator Start()
    {
        linkCode = PlayerPrefs.GetString("linkCode").Trim();
        tablesign = GameObject.FindGameObjectWithTag("TableSign");
        while (true)
        {
            if (linkCode.Equals(""))
            {
                //loadinformation.text = "Getting Link Code";
                StartCoroutine(getLinkCode());
            }
            else
            {
               
                if (!gcmRegistered||!manualUpdateSettings)
                {
                    StartCoroutine(LoadSettings());
                }

                if (settingsLoaded != null)
                    {
                        if (settingsLoaded.value)
                        {
                            if (!gcmRegistered || !manualUpdateSkin)
                            {
                                StartCoroutine(getSkins());
                            }
                        }
                    }
                
                
            }
            if (settingsLoaded != null && skinsLoaded != null)
            {
                if (settingsLoaded.value && skinsLoaded.value)
                {
                    if (!TexturesLoaded.value)
                    {
                        
                        print("RefreshInt:10");
                        StartCoroutine(DownloadAndCacheSkinAtlas());
                        
                    }
                }
            }
            
                yield return new WaitForSeconds(refreshInt);
        }

    }

    IEnumerator LoadSettings()
    {
        
        //addLoadInfo("Loading Settings");
        if (!fullyloaded)
            CheckLoaded();
        
        var form = new WWWForm();
        form.AddField("activity", "getSettings");
        form.AddField("linkcode", linkCode);
        form.AddField("gcmid", GCMID);
        form.AddField("appVersion", Application.version);

        // Start a download of the given URL
        WWW www = new WWW(url, form);
        tablesign = GameObject.FindGameObjectWithTag("TableSign");
        // Wait for download to complete
        yield return www;
        if (www.error != null)
        {
            hasErrors = true;
            addLoadInfo("WWW download had an error:" + www.error);
        }
        else
        {
            finishLoading();
            if (www.text.Contains("Display Not Found"))
            {
                addLoadInfo("Display Not Found");
            }
            else
            {
                Scene tmpscene = SceneManager.GetActiveScene();
                if (settingsLoaded != null)
                    settingsLoaded.value = true;
                XmlSerializer Xml_Serializer = new XmlSerializer(typeof(DisplayInfo));
                using (StringReader reader = new StringReader(www.text))
                {
                    displayInfo = (DisplayInfo)Xml_Serializer.Deserialize(reader);
                }
                if (!gcmRegistered || !manualUpdateSkin)
                {
                    StartCoroutine(getSkins());
                    
                }
                StartCoroutine(GetHighHand());
                if (fullyloaded)
                { //Debug.Log("Step 1");
                    manualUpdateSettings = true;
                    refreshInt = 10;
                    if (displayInfo.lockedScene == 0)
                    {
                        //Debug.Log("Step 2");
                        if (pauseLoop)
                        {

                            pauseLoop = false;
                            currentScene.duration = 0;
                        }

                        if (currentScene.duration == 0)
                        {


                            // Debug.Log("Step 3");
                            StartCoroutine(loadscene());
                        }
                    }

                    else
                    {
                        pauseLoop = true;
                        //Debug.Log("Went Here");

                        if (tmpscene.buildIndex != displayInfo.lockedScene)
                            Application.LoadLevel(displayInfo.lockedScene);
                    }
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Effect"))
                {
                    go.SetActive(false);
                    if (go.GetComponent<Effect>().EffectID == currentScene.EffectID)
                    {
                        go.SetActive(true);
                    }
                }
                if (displayInfo.DisplayMode == 2)
                {
                    //0tablesign.SetActive(true);
                    //tablesign.GetComponent<TableSign>().setTableSign(displayInfo.tableWagers);
                }
                else
                {
                    //if (tablesign != null) {
                    //  tablesign.SetActive(false);
                    //}
                }
            }
        }
    }
    private void finishLoading()
    {
    }
    private bool CheckLoaded()
    {
        if (settingsLoaded.value)
        {
            if (skinsLoaded.value)
            {
                if (TexturesLoaded.value)
                {
                    if (!hasErrors)
                    {
                        fullyloaded = true;
                    }
                }
            }
        }


        return fullyloaded;
    }
    IEnumerator DownloadAndCacheSkinAtlas()
    {
        GameObject tmpObject = new GameObject();
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;
        string URL = displayInfo.BundleWindowsURL;
        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            URL = displayInfo.BundleWindowsURL;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            URL = displayInfo.BundleAndroidUrl;
        }


        using (WWW www = new WWW(URL))
        {
            yield return www;
            if (www.error != null)
            {
                hasErrors = true;
                addLoadInfo("WWW download had an error:" + www.error);
                throw new Exception(www.error);

            }
            AssetBundle bundle = www.assetBundle;
            addLoadInfo("Skin Downloaded");
            if (displayInfo.AssetName == "")
                Instantiate(bundle.mainAsset);
            else
                tmpObject = (GameObject)Instantiate(bundle.LoadAsset(displayInfo.AssetName)) as GameObject;
            tmpObject.name = "Skin";
            tmpObject.tag = "Skin";
            DontDestroyOnLoad(tmpObject.transform);
            if (TexturesLoaded != null)
                TexturesLoaded.value = true;
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator GetHighHand()
    {
        var form = new WWWForm();
        form.AddField("activity", "CurrentAdvHH");
        form.AddField("casinoid", displayInfo.casinoID);
        // Start a download of the given URL
        WWW www = new WWW(url, form);
        // Wait for download to complete
        yield return www;
        if (www.error == null)
        {
            if (!www.text.Contains("norows"))
            {

                XmlSerializer Xml_Serializer = new XmlSerializer(typeof(highHand));
                using (StringReader reader = new StringReader(www.text))
                {

                    TheHand = (highHand)Xml_Serializer.Deserialize(reader);
                }
                updateHH = true;
            }
            else
            {
                //TheHand = ScriptableObject.CreateInstance("highHand") as highHand;

            }
        }
    }
    IEnumerator getSkins()
    {
        // addLoadInfo("Getting Skins");
        var form = new WWWForm();
        form.AddField("activity", "getSkins");
        form.AddField("linkcode", linkCode);
        // Start a download of the given URL
        WWW www = new WWW(url, form);

        // Wait for download to complete
        yield return www;
        if (www.error == null)
        {
            if (skinsLoaded != null)
                skinsLoaded.value = true;
            XmlSerializer Xml_Serializer = new XmlSerializer(typeof(SkinElements));
            using (StringReader reader = new StringReader(www.text))
            {

                skinElements = (SkinElements)Xml_Serializer.Deserialize(reader);
                manualUpdateSkin = true;
            }
        }
        else
        {
            hasErrors = true;
            addLoadInfo(www.error);
        }
    }
    IEnumerator loadscene()
    {
        while (!pauseLoop)
        {
            currentScene = displayInfo.scenes[currentSceneSequenceID];
            Application.LoadLevel(currentScene.sceneID);
            yield return new WaitForSeconds(currentScene.duration);
            currentSceneSequenceID++;
            //Debug.Log("current Scene Sequence ID:" + currentSceneSequenceID);
            if (currentSceneSequenceID > displayInfo.scenes.Count() - 1)
            {
                currentSceneSequenceID = 0;
            }
        }

    }

    IEnumerator getLinkCode()
    {
        var form = new WWWForm();
        form.AddField("activity", "getLinkCode");

        // Start a download of the given URL
        WWW www = new WWW(url, form);

        // Wait for download to complete
        yield return www;

        if (www.error != null)
        {
            Debug.Log("error:" + www.error);
            hasErrors = true;
            addLoadInfo("error: " + www.error);
        }
        else
        {
            linkCode = www.text;
            Debug.Log("linkcode: " + www.text);
            PlayerPrefs.SetString("linkCode", linkCode);
            addLoadInfo("Saved Link Code");
        }
    }
    public void addLoadInfo(string txtinfo)
    {
        loadinformation.text = loadinformation.text + txtinfo + "\n";
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("quit");
            Application.Quit();
        }
        /*   if (displayInfo.DisplayMode == 2)
           {
               tablesign.SetActive(true);
               tablesign.GetComponent<TableSign>().setTableSign(displayInfo.tableWagers);
           }
           else
           {
              if (tablesign != null) {
                   tablesign.SetActive(false);
               }
       }*/


    }
    //used for GCM
    public void GCMReceiver(string gcmMessage)
    {
        string[] tmp = gcmMessage.Split (';');
	
        if (gcmMessage.Contains ("GCMRegistered")) {
            GCMID = obj_Activity.CallStatic<string> ("getRegID");
            gcmIsRegistered.value = true;
            gcmRegistered = true;
            print("saved GCM");
        } else {
            //addLoadInfo(gcmMessage);
            print("TXT:" + gcmMessage);
            //string[] type = tmp[4].Split('=');
            string[] msg = tmp[2].Split('=');

            switch (msg[1])
            {
                case "getSkin":
                      StartCoroutine(getSkins());
                    break;
                case "getSettings":
                    StartCoroutine(LoadSettings());
                    break;
                case "getHighHand":
                    StartCoroutine(GetHighHand());
                    break;
            }
            
        }
    }
}
