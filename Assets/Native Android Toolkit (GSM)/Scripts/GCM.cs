using UnityEngine;
using System.Collections;

public class GCM : MonoBehaviour
{
	AndroidJavaClass cls_UnityPlayer;
	AndroidJavaObject obj_Activity;
	private string GCMID = "";
	private string message;
	private string type;
    public UILabel thing;
	
	// Use this for initialization
	void Start ()
	{

		//cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");			//Grabs the Android Unity Player
		//obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");	//Grabs current Android activity
        //obj_Activity.CallStatic("setListener", new object[] { "DisplayManager", "GCMReceiver" });
		/*
		 *In order to use GCM on this device you must register the device with the GCM server
		 *YourProjectIDHere is where you would put the Project ID that is given by the Google API Console
		 *You can learn more about Google Cloud Messaging by visiting the GCM website: http://developer.android.com/google/gcm/index.html
		 */
        //obj_Activity.CallStatic("registerDeviceWithGCM", new object[] { "398299964412" });
	}

	void OnGUI ()
	{
        
		
	}
		
	public void GCMReceiver (string gcmMessage)
	{
        thing.text = gcmMessage;
		string[] tmp = gcmMessage.Split (';');
	
		if (gcmMessage.Contains ("GCMRegistered")) {
			GCMID = obj_Activity.CallStatic<string> ("getRegID");	
		} else {
            thing.text = gcmMessage;
		}
		
		
		
	}
	
}
