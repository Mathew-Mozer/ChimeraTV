using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoxID : MonoBehaviour
{
    public UILabel theLabel;
    public UILabel debugIdentifierLabel;
    private DisplayManager displayManager;
    public List<UILabel> characters;
    public GameObject passwordBox;
    public UISprite BackSprite;
    public static BoxID boxId;
    public string inputKeys;
    public int timer;
    public bool timerStarted;
    // Use this for initialization
    void Start()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        originColor = BackSprite.color;
        BoxID.boxId = this;
    }

    // Update is called once per frame
    public string password = "";
    void Update()
    {
        theLabel.text = displayManager.linkCode + "\n" + displayManager.macAddress + "\n" + displayManager.url;
        debugIdentifierLabel.text = displayManager.linkCode + "\n" + displayManager.macAddress + "\n" + displayManager.url;
        showKeys();
        
        inputKeys += Input.inputString;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            EscapeKey();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Application.Quit();
            displayManager.ToggleDebug();
        }
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            displayManager.ToggleDebug();
            //Application.Quit();
            //AttemptToExit();
        }

        foreach (char c in inputKeys)
        {
            resetTimer();
            BackSprite.color = originColor;
            
            if (c == "\b"[0])
            {
                backSpaceKey();
            }else if (c == "\n"[0]|| c == "\r"[0])
            {
                EnterKey();
            }
            else
            {
                if (password.Length != 4)
                {
                    password += c;
                    showKeys();
                }
            }

        }
        inputKeys = "";
    }

    internal void backSpaceKey()
    {
        
        if (password.Length != 0)
            password = password.Substring(0, password.Length - 1);
    }

    internal void EnterKey()
    {
        
        AttemptToExit();
    }

    internal void EscapeKey()
    {

        Application.Quit();
    }

    private void resetTimer()
    {
        timer = 10;
    }

    public void togglePasswordBox()
    {
        resetTimer();
        password = "";
        showKeys();
        inputKeys = "";
        BackSprite.color = originColor;
        passwordBox.SetActive(!passwordBox.activeSelf);
        StartCoroutine(closePasswordBox());
    }

    private IEnumerator closePasswordBox()
    {
        if (!timerStarted)
        {
            while (timer > 0)
            {
                timerStarted = true;
                yield return new WaitForSeconds(1);
                timer--;
            }
            timerStarted = false;
            if (passwordBox.activeSelf)
            {
                togglePasswordBox();
            }
        }

    }
    private Color originColor;
    private void AttemptToExit()
    {
        switch (password)
        {
            case "0224":
                print("quit");
                scripttest.UnityKioskMode.StopCharon();
                Application.Quit();
                break;
            case "8675":
                Debug.Log("Service Status:" + scripttest.UnityKioskMode.ServiceRunning());
                togglePasswordBox();
                    scripttest.UnityKioskMode.StopCharon();
                break;
            case "5309":
                scripttest.UnityKioskMode.startKioskMode();
                togglePasswordBox();
                break;
            default:
                password = "";
                showKeys();
                BackSprite.color = Color.red;
                break;        
        }
        
        
         
    }

    private void showKeys()
    {
        characters[0].text = "";
        characters[1].text = "";
        characters[2].text = "";
        characters[3].text = "";
        for (int i = 0; i < password.Length; i++)
        {
            characters[i].text = "•";
        }

        switch (password.Length)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
