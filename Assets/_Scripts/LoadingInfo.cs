using UnityEngine;
using System.Collections;

public class LoadingInfo : MonoBehaviour
{
    public DisplayManager displaymanager;
    public UILabel VersionLabel;
    public UILabel APILabel;
    public UILabel LinkCodeLabel;
	// Use this for initialization
	void Start () {
        VersionLabel.text = "Version: "+Application.version;
        APILabel.text = "API Version: " + displaymanager.APIVersion;
    }
	
	// Update is called once per frame
	void Update () {
	    if (displaymanager != null)
	    {
	        string tmpLink = displaymanager.linkCode;
	        string tmpMac = displaymanager.macAddress;
	        LinkCodeLabel.text = tmpLink + "\n" + tmpMac;
	    }
	}
}
