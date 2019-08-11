using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class adjustODO : MonoBehaviour {
    public GameObject[] Digits = new GameObject[8];
    public int valLength=1;
    Odometer theODO;
    public float speed;
    float _timeStartedLerping;
    public float percentageComplete;
    float timeTakenDuringLerp = 5f;
    bool _isLerping = true;
    private Vector3 moveToLocation;
    private Vector3 startingPosition;
    public bool resetODO;
    public bool setValue;
    private int newValue;
    private bool round2;

    // Use this for initialization
    void Start () {
        startingPosition = new Vector3(-150, startingPosition.y - 450, startingPosition.z);
        theODO = gameObject.GetComponent<Odometer>();
    }
    public void setODO(int newValue)
    {
        resetODO = true;
        setValue = true;
        this.newValue = newValue;
       
    }
	// Update is called once per frame
	void Update () {
        if (resetODO)
        {
            //Debug.Log("Resetting ODO");
            theODO.SetValue(0);
            resetODO = false;
            Digits[7].SetActive(false);
            Digits[6].SetActive(false);
            Digits[5].SetActive(false);
            Digits[4].SetActive(false);
            Digits[3].SetActive(false);
            Digits[2].SetActive(false);
        }
  if (round2)
        {
            theODO.SetValue(newValue);
            round2 = false;
            _isLerping = true;
        }
        if (setValue)
        {
            if (newValue != 99999999)
            {
                valLength = newValue.ToString().Length;
            }
            for (int i = 0; i < valLength; i++)
            {
                Digits[i].SetActive(true);
            }
            //Debug.Log("setting the value now");
            round2 = true;
            
            _isLerping = true;
            setValue = false;
        }
      
        if (theODO.value == 1 || theODO.value == 10 || theODO.value == 900 || theODO.value == 9900 || theODO.value == 99900 || theODO.value == 999900 || theODO.value == 9999900 || theODO.value == 99999900) {

            if (theODO.value != 99999999)
            {
                //Debug.Log("setting val");
                valLength = theODO.value.ToString().Length + 1;
            }
            for (int i = 0; i < valLength; i++)
            {
                Digits[i].SetActive(true);
            }
            _isLerping = true;

        }





        if (_isLerping)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;
            moveToLocation = new Vector3(-(valLength * 70), -100, gameObject.transform.localPosition.z);
            theODO.transform.localPosition = Vector3.Lerp(startingPosition, moveToLocation, percentageComplete);
            float step = 1.5f * Time.deltaTime;
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
    }
}
