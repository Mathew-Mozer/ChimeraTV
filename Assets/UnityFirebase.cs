using UnityEngine;
using System.Collections;

public class UnityFirebase : MonoBehaviour {

    public void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        DisplayManager.displayManager.addtodebug("Token Found:" + token.Token);
        DisplayManager.displayManager.setFireBaseToken(token.Token);
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        string msgData = "";
        if (e.Message.Data.ContainsKey("command"))
        {
            Debug.Log("found Command Key");
            DisplayManager.displayManager.addtodebug("Command Received:" + e.Message.Data["command"]);
            DisplayManager.displayManager.addtodebug("Command Received:" + e.Message.Data["command"]);
            switch (e.Message.Data["command"])
            {
                case "getSettings":
                    DisplayManager.displayManager.getSettings();
                    DisplayManager.displayManager.addtodebug("Getting Settings because FCM Said so");
                    break;
                case "stopCharon":
                    scripttest.UnityKioskMode.StopCharon();
                    break;
                case "RestartApp":
                    DisplayManager.displayManager.restartApp();
                    break;
                case "LaunchApp":
                    scripttest.UnityKioskMode.LaunchApp(e.Message.Data["packageName"]);
                    break;
                case "Quit":
                    Application.Quit();
                    break;
                case "AttemptReconnectFirebase":
                    DisplayManager.displayManager.markAsConnected();
                    break;
            }
        } else
        {
            Debug.Log("Couldn't find key");
        }
        
        if (e.Message.Data.ContainsKey("packageName"))
        {
            DisplayManager.displayManager.addtodebug("Package Received:" + e.Message.Data["packageName"]);
        }
        
        //UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
  
}
