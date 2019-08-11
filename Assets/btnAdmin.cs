using UnityEngine;
using System.Collections;

public class btnAdmin : MonoBehaviour
{
    public DisplayManager dm;
    void Start()
    {
        if (!dm)
            dm = DisplayManager.displayManager;
    }


    public void MoveNext()
    {
        if (dm.displayData.debug)
        {
            dm.pauseLoop = true;
            dm.adminPause = true;
            dm.currentScene.duration = 0;
            Debug.Log("Next Scene");
            dm.nextScene(2);
        }


    }
    public void MovePrev()
    {
        dm.ToggleDebug();

    }
    public void PausePlayer()
    {
        if (dm.displayData.debug)
        {
            dm.adminPause = !dm.adminPause;
            if (dm.adminPause)
            {
                Debug.Log("Paused");
            }
            else
            {
                Debug.Log("Play");
            }
        }

    }

    public void StartCharon()
    {
        scripttest.UnityKioskMode.startKioskMode();
    }
    public void StopCharon()
    {
        scripttest.UnityKioskMode.StopCharon();
    }
    public void LaunchAppInstaller()
    {
        scripttest.UnityKioskMode.LaunchApp("com.droidlogic.appinstall");
        exitChimeraTV();
    }
    public void LaunchSettings()
    {
        scripttest.UnityKioskMode.LaunchApp("com.android.tv.settings");
        exitChimeraTV();
    }
    public void sendLogToSlack()
    {
        StartCoroutine(sendLogFile());
    }
    public void ClearLogTo()
    {
        dm.errormessagelog.text = "";
        dm.wwwErrors = 0;
    }
    internal void exitChimeraTV()
    {

        //togglePasswordBox();
#if UNITY_ANDROID && !UNITY_EDITOR
        scripttest.UnityKioskMode.StopCharon();
#endif

        Application.Quit();
    }

    public void payTimeTarget()
    {
        StartCoroutine(sendPayTimeTarget());
    }
    private IEnumerator sendPayTimeTarget()
    {
        Debug.Log("trying to send");
        dm = DisplayManager.displayManager;
        var form = new WWWForm();
        form.AddField("action", "endTimeTarget");
        form.AddField("timeTargetId", dm.currentScene.timeTargetData.id);
        form.AddField("endTime", "3"); 
        form.AddField("promotionId", dm.currentScene.promoID); 

        form.AddField("currentTime", dm.currentTime.ToString("yyyy/M/d H:mm:s"));
        dm.currentScene.timeTargetData.endTime = dm.currentTime.ToString();
        // Start a download of the given URL
        WWW www = new WWW(dm.url, form);
        // Wait for download to complete
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            dm.addtodebug(www.error);
        }
        else
        {
            Debug.Log(www.text);
            if (www.text.Contains("success"))
            {
            
            }
        }
    }
    private IEnumerator sendLogFile()
    {
        var form = new WWWForm();
        form.AddField("action", "LogFromBox");
        form.AddField("logdata", dm.errormessagelog.text);

        form.AddField("displayname", dm.displayData.DisplayName);
        form.AddField("macAddress", dm.macAddress);

        // Start a download of the given URL
        WWW www = new WWW(dm.url, form);
        // Wait for download to complete
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {

        }
        else
        {
            if (www.text.Contains("success"))
            {
                dm.errormessagelog.text = "";
                dm.wwwErrors = 0;
            }
        }

    }
}
