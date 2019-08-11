using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RestaurantMenuContainer : MonoBehaviour
{
    DatabaseReference connectedRef;
    float waitTimer = 1f;
    private int PromoID = 0;
    public List<MenuItemObject> MenuItemObjects = new List<MenuItemObject>();
    public bool refresh;

    public RestaurantMenuContainer()
    {
        
    }

    void Update()
    {
        if (refresh)
        {
            refresh = false;
            StartCoroutine(retrieveData());
        }
    }

    public void LoadData(int promoId)
    {
        PromoID = promoId;
        Debug.Log("Load Data: Checking for Child in: " + PromoID);
        Resources.UnloadUnusedAssets();
#if UNITY_ANDROID && !UNITY_EDITOR
        loadfromFireBase(PromoID);
#else
        loadFromJson();
#endif

    }

    private void loadFromJson()
    {
        /*
        MenuItemObject tmp = new MenuItemObject("30",42,235,"Vegan",492,"Label",88,"#244332");
        MenuItemObjects.Add(tmp);
        tmp = new MenuItemObject(160, 286, 200,200, "PictureSlideshow",776);
        MenuItemObjects.Add(tmp);
        tmp = new MenuItemObject(160, 286, 1, 4, "PictureSlideshow", 823);
        MenuItemObjects.Add(tmp);
        tmp = new MenuItemObject();
        tmp.key = "Background-Data";
        tmp.image = "bg3.jpg";
        MenuItemObjects.Add(tmp);
        tmp = new MenuItemObject("16", 22, 235, "Fresh grilled vegetable patty served with sliced avocado garnished with lettuch and tomato", 532, "Label", 704,"#ffffff");
        MenuItemObjects.Add(tmp);
        */
        waitTimer = 5f;
        StartCoroutine(retrieveData());
        //StartCoroutine(StartSyncLoop());
        
    }

    private IEnumerator StartSyncLoop()
    {
        
        while (true)
        {

            //Check for the link code. If its found Load the rest of the Data.
            
            yield return new WaitForSeconds(waitTimer);
            
        }
    }

    private IEnumerator retrieveData()
    {
        
        string url = "https://chimeratvhome.firebaseio.com/Promotions/" + PromoID + "/layout.json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        // Wait for download to complete
        yield return www.Send();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            MenuItemObjects.Clear();
            Dictionary<string,MenuItemObject> DictMenuObs = JsonConvert.DeserializeObject<Dictionary<string, MenuItemObject>>(www.downloadHandler.text);
            List<KeyValuePair<string, MenuItemObject>> list = DictMenuObs.ToList();

            // Loop over list.
            foreach (KeyValuePair<string, MenuItemObject> pair in list)
            {
                pair.Value.key = pair.Key;
                MenuItemObjects.Add(pair.Value);
            }
            DisplayManager.displayManager.RestaurantManager.BroadcastMessage("UpdateMenuChild");
            Debug.Log("Objects Found:" + MenuItemObjects.Count);
        }




        
        
    }

    private void loadfromFireBase(int promoID)
    {
        connectedRef = FirebaseDatabase.DefaultInstance.GetReference("Promotions/" + promoID + "/layout");
        connectedRef.ValueChanged += MenuLayoutChanged;

    }
    private void MenuLayoutChanged(object sender, ValueChangedEventArgs e)
    {
        MenuItemObjects.Clear();
        Debug.Log("Menu Layout Changed:" + e.Snapshot.ChildrenCount);
        foreach (DataSnapshot child in e.Snapshot.Children)
        {
            MenuItemObject tempObj = JsonConvert.DeserializeObject<MenuItemObject>(child.GetRawJsonValue());
            tempObj.key = child.Key;
        //    Debug.Log("Temp Key: " + tempObj.key);
            MenuItemObjects.Add(tempObj);
        }
        DisplayManager.displayManager.RestaurantManager.BroadcastMessage("UpdateMenuChild");

    }
    
}