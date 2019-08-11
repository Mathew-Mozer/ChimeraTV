using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Firebase.Database;
using UnityEngine;

public class RestaurantMenuManager : MonoBehaviour {
    public List<GameObject> ItemTemplates = new List<GameObject>();
    public List<GameObject> RestaurantObjects = new List<GameObject>();
    public UITexture BackgroundTexture;
    public GameObject loadingLabel;
    public string backgroundFile;
    public delegate void BgDownloadComplete(string filename);
    // Use this for initialization
    void Start ()
    {
        DisplayManager.displayManager.currentManager = gameObject;
        DisplayManager.displayManager.setRestaurantManager(gameObject.transform.parent.gameObject);
        Debug.Log("Set Manager to:" + DisplayManager.displayManager.RestaurantManager.name);
        Debug.Log("Starting to build menu:" + DisplayManager.displayManager.RestaurantLayouts[DisplayManager.displayManager.currentScene.promoID].MenuItemObjects.Count);
        BuildChildren();
    }

    private void BuildChildren()
    {
        foreach (MenuItemObject menuItem in DisplayManager.displayManager.RestaurantLayouts[DisplayManager.displayManager.currentScene.promoID].MenuItemObjects)
        {
            loadingLabel.SetActive(false);
            switch (menuItem.key)
            {
                case "Background-Data":
                    backgroundFile = menuItem.image;
                    Debug.Log("Switching to:" + menuItem.image);
                    Texture2D tmpTexture2D =
                        DisplayManager.displayManager.GetTextureManager().DownloadBackgroundTexture(menuItem.image,gameObject);
                    Debug.Log("tmpTexture2d:" + tmpTexture2D);
                    BackgroundTexture.mainTexture = tmpTexture2D;
                    break;
                default:
                    GameObject tr;
                    //Debug.Log("Creating Menu Item:" + menuItem.key);
                    switch (menuItem.type)
                    {
                        case "PictureSlideshow":
                            tr = Instantiate(ItemTemplates[1]);
                            tr.GetComponent<InternalPictureSlideshow>().setProperties(menuItem);
                            //RestaurantObjects.Add(tr);
                            break;
                        case "Label":
                            tr = Instantiate(ItemTemplates[0]);
                            tr.GetComponent<RestMenuItem>().setProperties(menuItem);
                            //RestaurantObjects.Add(tr);
                            break;
                        default:
                            break;
                    }
                    
                    break;
            }

        }
    }

    public void UpdateMenuChild()
    {
        List<MenuItemObject> MenuItemObjects =
            DisplayManager.displayManager.RestaurantLayouts[DisplayManager.displayManager.currentScene.promoID]
                .MenuItemObjects;

        //.First(s => s.name == filename)
        BuildChildren();
    }
    // Update is called once per frame
    void Update () {

    }

    public void toManager(string msg)
    {
        Debug.Log("BKI received broadcast" + msg);
        switch (msg)
        {
            case "Downloaded-Background":
                BackgroundTexture.mainTexture= DisplayManager.displayManager.textureManager.LoadTexture(backgroundFile,gameObject);
                //LoadBackgroundFromFile();
                break;
        }    
    }

    /*
    private void LoadBackgroundFromFile()
    {
        Debug.Log("BKI: Loading From File ");
        Texture2D tex = new Texture2D(2, 2);
        byte[] fileData;
        fileData = File.ReadAllBytes(DisplayManager.displayManager.picturePath + "\\" + DisplayManager.displayManager.getFilename(backgroundFile));
        tex.LoadImage(fileData);
        BackgroundTexture.mainTexture = tex;
    }
    */
    void LoadFromFirebase()
    {
        #if UNITY_EDITOR
        string txt = @"{ text: 'Text Box', height: 142, width: 415, left: 235, top: 32,fontsize:100}";
        //tr.GetComponent<RestMenuItem>().setProperties(txt);
        #endif
    }

}
