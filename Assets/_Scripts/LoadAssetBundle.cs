using System;
using UnityEngine;
using System.Collections;

public class LoadAssetBundle : MonoBehaviour {
    public string BundleURL;
    public string AssetName;
    public int version;
    public UIAtlas ctvAtlas;
    
    void Start()
    {
        //loadAtlas("http://connect.typhonpacificstudios.com/tv/assetbundles/gac2.unity3d");
       
    }
    public void loadAtlas(string url){
        BundleURL = url;
        StartCoroutine(DownloadAndCache());
        //AssetName = "GACGO";
    }
    IEnumerator DownloadAndCache()
    {
        GameObject tmpObject = new GameObject();
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
                Instantiate(bundle.mainAsset);
            else
           tmpObject = (GameObject) Instantiate(bundle.LoadAsset(AssetName)) as GameObject;
            tmpObject.name = "Skin";
            tmpObject.tag = "Skin";
            DontDestroyOnLoad(tmpObject.transform);

            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
