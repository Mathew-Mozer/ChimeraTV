using UnityEngine;
using System.Collections;

public class AdjustDisplay : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        UpdateDims();
    }
	
	// Update is called once per frame
	void Update () {
        if (DisplayManager.displayManager != null)
        {


            if (DisplayManager.displayManager.updateDimensions)
            {
                UpdateDims();
                DisplayManager.displayManager.updateDimensions = false;
            }
        }
	}

    void UpdateDims()
    {
        gameObject.GetComponent<UIRoot>().fitWidth = DisplayManager.displayManager.displayData.fitw;
        gameObject.GetComponent<UIRoot>().fitHeight = DisplayManager.displayManager.displayData.fith;
        gameObject.GetComponent<UIRoot>().manualWidth = DisplayManager.displayManager.displayData.width;
        gameObject.GetComponent<UIRoot>().manualHeight = DisplayManager.displayManager.displayData.height;
        
        if (DisplayManager.displayManager.displayData.isVertical)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            //Screen.orientation = ScreenOrientation.Portrait;
        }
        else
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
           // Screen.orientation = ScreenOrientation.Landscape;
        }
        
        
    }

}
