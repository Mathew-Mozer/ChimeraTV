using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

public class RestMenuItem : MonoBehaviour
{
    public UILabel MainLabel;
    public MenuItemObject MenuItem;
    public GameObject MainGameObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateMenuChild()
    {
        //Debug.Log("Child Heard THAT!");
        Destroy(gameObject);
    }
    public void setKey(DatabaseReference db, string fbKey)
    {
        //Debug.Log("Setup Value Change" + fbKey);
        db.Child(fbKey).ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        //Debug.Log("Handle Value Changed");
        if (args.DatabaseError != null)
        {
            //Debug.LogError(args.DatabaseError.Message);
            return;
        }
        switch (args.Snapshot.Child("type").Value.ToString())
        {
            case "Label":
                
                break;
        }
        MenuItem = JsonConvert.DeserializeObject<MenuItemObject>(args.Snapshot.GetRawJsonValue());
        setProperties(MenuItem);

    }

    internal void setProperties(string Json)
    {
        MenuItem = JsonConvert.DeserializeObject<MenuItemObject>(Json);
        setProperties(MenuItem);
    }

    internal void setProperties(MenuItemObject menuItem)
    {
        MenuItem = menuItem;
        //Debug.Log("Should have created:" + menuItem.text);
        if (MainLabel)
        {
            MainLabel.pivot = UIWidget.Pivot.TopLeft;
        MainLabel.text = MenuItem.text;
        MainLabel.gameObject.transform.localPosition = new Vector3(ConvertLeft(MenuItem.left), ConvertTop(MenuItem.top));
            if (MenuItem.color != null)
            {
                MainLabel.color = DisplayManager.HexToColor(MenuItem.color);
            }
            else
            {
                //Debug.Log("Color is null for:" + menuItem.text);
            }
            try
            {
                MainLabel.fontSize = int.Parse(MenuItem.fontsize) * 2;
            }
            catch (Exception c)
            {
                Debug.Log("Exception" + MenuItem.key);
            }
        
        }
        else
        {
            //Debug.Log("Main Label is Null");
        }


    }
    private float ConvertLeft(int left)
    {
        return (left)*2;
    }
    private float ConvertTop(int Top)
    {
        
        return -(Top) * 2;
    }
}
