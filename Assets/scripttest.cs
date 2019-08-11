using UnityEngine;
using System.Collections;
using System;

public class scripttest : MonoBehaviour
{

    private AndroidJavaObject KioskMode = null;
    public AndroidJavaObject activityContext = null;
    public static scripttest UnityKioskMode;
    private bool startedService;



    // Use this for initialization
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        UnityKioskMode = this;
        setActivityContext();
#endif
    }

    public bool StartedService()
    {
        if (!startedService)
        {
            setActivityContext();
            //startKioskMode();
            startedService = true;
        }
        return true;
    }
    private void setActivityContext()
    {
        if (KioskMode == null)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
#endif
        }
    }

    public void toasty()
    {
        DisplayManager.displayManager.addtodebug("Step1");
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                KioskMode = pluginClass.CallStatic<AndroidJavaObject>("instance");
                KioskMode.Call("setContext", activityContext);
                activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    KioskMode.Call("showToast", "This is a test");
                    DisplayManager.displayManager.addtodebug("Should have shown toast");
                    startedService = true;
                }
                ));
            }
        }
#endif
    }

    internal void LaunchApp(string v)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        DisplayManager.displayManager.addtodebug("Should be launching: " + v);
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                DisplayManager.displayManager.addtodebug("launching:" + v);
                KioskMode.CallStatic("LaunchApp", v);
                
            }
        }
#endif
    }

    public void startKioskMode()
    {
        DisplayManager.displayManager.addtodebug("step 1");
        if (DisplayManager.displayManager.displayData != null)
        {
            DisplayManager.displayManager.addtodebug("step 2");
            if (DisplayManager.displayManager.displayData.kiosk)
            {
                DisplayManager.displayManager.addtodebug("trying to start service");
#if UNITY_ANDROID && !UNITY_EDITOR
                using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
                {
                    if (pluginClass != null)
                    {
                        //KioskMode = pluginClass.CallStatic<AndroidJavaObject>("instance");
                        //KioskMode.Call("setContext", activityContext);
                        //KioskMode.Call("startCharonService");
                    }
                 }
#endif
            }
            else
            {
                //DisplayManager.displayManager.addtodebug("kiosk must be false");
            }
        }

    }
    // Update is called once per frame


    internal void StopCharon()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                
                //KioskMode.Call("StopCharon","");
               // DisplayManager.displayManager.addtodebug("Stopping Charon");
            }
        }
#endif
    }

    internal bool ServiceRunning()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                return KioskMode.Call<Boolean>("ServiceRunning");
            }
        }
#endif
        return false;

    }

    internal bool DebugToasts()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                return KioskMode.Call<Boolean>("DebugToasts");
            }
        }
#endif
        return false;
    }
    internal void DebugToasts(bool toast)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("typhonpacific.com.unitykioskmode.KioskMode"))
        {
            if (pluginClass != null)
            {
                KioskMode.Call("DebugToasts",toast);
            }
        }
#endif
    }

}
