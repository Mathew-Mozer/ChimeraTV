using UnityEngine;
using System.Collections;


/// <summary>
/// <author>Stephen King</author>
/// <date>6/15/2016</date>
/// <version>1.0</version>
/// 
/// This class handles network testing for the login screen. It is not immediately extensible because
/// the request was that it would only be available on the login screen.
/// </summary>
public class NetworkTest : MonoBehaviour {

    //Status messages
    const string NETWORK_DOWN = "[1AFFC6FF]Network Status:[-] [FF0000]DOWN - There is a problem with your network[-]";
    const string NETWORK_UP = "[1AFFC6FF]Network Status:[-] [00FF00]OK[-]";
    const string NETWORK_TEST = "[1AFFC6FF]Network Status:[-] [FFFFFF]TESTING...[-]";

    //Timers
    const int NETWORK_TIMEOUT = 10;
    const int DELAY_BEFORE_TEST = 3;

    //monobehavior links
    public string s_client;
    public UILabel networkStatusLabel;
    public int attemptCooldownTime;


    //net objects
    //System.Net.Http webClient;
    System.IO.Stream ioStream;

    void Start() {

        InvokeRepeating("TestNetwork", DELAY_BEFORE_TEST, attemptCooldownTime);
    }

    /// <summary>
    /// This performs a network test by attempting to access a network resource
    /// on a high availablility network.
    /// </summary>
    /// <returns></returns>

    void TestNetwork() {
        networkStatusLabel.text = NETWORK_TEST;
        //Try to connect to a reliable network resource (like google). If this connection cannot be made,
        //then there is a 99.9999% chance that the customer's network is down.
        try {
            //Build network objects
            //webClient = new System.Net.WebClient();
            //ioStream = webClient.OpenRead(s_client);

            //Set network message
            DisplayNetworkStatus(true);

        } catch (System.Exception e) {
            
            //Log the error
            Debug.Log("Network error:" + e);

            //set network message
            DisplayNetworkStatus(false);
        }
      
    }

    /// <summary>
    /// This displays the status of the network on the box
    /// </summary>
    void DisplayNetworkStatus(bool status) {
        if (status) {
            networkStatusLabel.text = NETWORK_UP;
        } else if (!status) {
            networkStatusLabel.text = NETWORK_DOWN;
        }
        
    }

}
