﻿using System;
using UnityEngine;
public class AndroidManager
{
    public static bool setWifiEnabled(bool enabled)
    {/*
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            try
            {
                using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
                {
                    return wifiManager.Call<bool>("setWifiEnabled", enabled);
                }
            }
            catch (Exception e)
            {
            }
        }
        return false;
        */
        return true;
    }
    public static bool isWifiEnabled()
    {
        /*
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            try
            {
                using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
                {
                    return wifiManager.Call<bool>("isWifiEnabled");
                }
            }
            catch (Exception e)
            {

            }
        }
        return false;
        */
        return true;
    }
}