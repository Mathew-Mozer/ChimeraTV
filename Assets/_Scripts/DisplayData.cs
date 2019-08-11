using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class DisplayData{

    public int propertyID;
    public string BundleAndroidUrl;
    public string BundleWindowsURL;
    public string BundleVer;
    public string AssetName;
    public string DefaultLogo;
    public int lockedScene;
    public string checkIn;
    public string lastUpdated;
    public string DisplayName="unnamed";
    public string AppVersion;
    public string skinLastUpdated;
    public DateTime TimeFromServer;
    public string apiurl;
    public int width;
    public int height;
    public bool fitw;
    public bool fith;
    public bool flip= false;
    public bool flipv = false;
    public bool debug;
    public bool isVertical;
    public bool kiosk=false;

    //Specific Scene Settings
    public List<scene> SceneList;
    
    //Unimplemented
    public List<PictureData> PictureList { get; internal set; }
}
