using UnityEngine;
using System.Collections;

public class KeyboardButton : MonoBehaviour
{

    public KeyCode keyCode;
	// Use this for initialization
	void Start () {
	
	}

    public void clickbutton()
    {
        
        switch (keyCode)
        {
            case KeyCode.Alpha0:
                BoxID.boxId.inputKeys += 0;
                break;
            case KeyCode.Alpha1:
                BoxID.boxId.inputKeys += 1;
                break;
            case KeyCode.Alpha2:
                BoxID.boxId.inputKeys += 2;
                break;
            case KeyCode.Alpha3:
                BoxID.boxId.inputKeys += 3;
                break;
            case KeyCode.Alpha4:
                BoxID.boxId.inputKeys += 4;
                break;
            case KeyCode.Alpha5:
                BoxID.boxId.inputKeys += 5;
                break;
            case KeyCode.Alpha6:
                BoxID.boxId.inputKeys += 6;
                break;
            case KeyCode.Alpha7:
                BoxID.boxId.inputKeys += 7;
                break;
            case KeyCode.Alpha8:
                BoxID.boxId.inputKeys += 8;
                break;
            case KeyCode.Alpha9:
                BoxID.boxId.inputKeys += 9;
                break;


            case KeyCode.Backspace:
                BoxID.boxId.backSpaceKey();
                break;
            case KeyCode.Escape:
                BoxID.boxId.EscapeKey();
                break;
            case KeyCode.Menu:
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                BoxID.boxId.EnterKey();
                break;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
