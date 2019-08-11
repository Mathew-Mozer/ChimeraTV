using UnityEngine;
using System.Collections;

public static class AndroidTools {
    static AndroidJavaObject mWiFiManager;

    public static string ReturnMacAddress()
    {
        string macAddr = "";
#if UNITY_ANDROID && !UNITY_EDITOR
        
        if (mWiFiManager == null)
        {
            using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
            {
                mWiFiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
            }
        }
        macAddr = mWiFiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<string>("getMacAddress");
       
#endif
        return macAddr;
    }
}
