using UnityEngine;
using System.Collections;

/*
 * This class needs to be broken up to make the countdown system modular.
 * 
 * 
 */ 

public class ODO : MonoBehaviour {
    public Odometer odometer;
    public UILabel TimeOF;
    public UILabel TimeRemaining;
    public GameObject fthdigit;
    public GameObject fourthdigit;
    bool autoStarted = false;
    bool sentWinner = false;
    bool blink;
    bool isFlipping=false;
    public int payout=100;
    public DisplayManager displayManager;
    public Vector3 origLoc;
    private HighHandManager hhManager;
    private int lenOfPayout;
    void Start()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        hhManager = GameObject.FindGameObjectWithTag("HighHandManager").GetComponent<HighHandManager>();
      
        if (odometer != null) {
            StartCoroutine("StartThing");
            
        }
    }

    private IEnumerator StartThing()
    {
         
        while (true)
        {
         
            odometer.SetValue(hhManager.payAmount);
           // Debug.Log("setting value to:" + thewww.payAmount);
            
            yield return new WaitForSeconds(8);
            isFlipping = true;
            odometer.SetValue(999);
            yield return new WaitForSeconds(.1f);
            isFlipping = false;
        }
    }

    void Update()
    {
        if (hhManager.payAmount > 999)
        {
            fthdigit.SetActive(true);
                Vector3 tmp = origLoc;
                tmp.x = -275f;
                tmp.y = -25f;
            odometer.gameObject.transform.localPosition = tmp;
        } else if (hhManager.payAmount < 99){
            fthdigit.SetActive(false);
            fourthdigit.SetActive(false);
            Vector3 tmp = origLoc;
            tmp.x = -145f;
            tmp.y = -25f;
            odometer.gameObject.transform.localPosition = tmp;
        }
        else
        {
            fthdigit.SetActive(false);
            fourthdigit.SetActive(true);
            Vector3 tmp = origLoc;
            tmp.x = -210f;
            tmp.y = -25f;
            odometer.gameObject.transform.localPosition = tmp;
        }
      
        if (!isFlipping)
        {

            odometer.SetValue(hhManager.payAmount);
        }
      
    }

    
    
}
