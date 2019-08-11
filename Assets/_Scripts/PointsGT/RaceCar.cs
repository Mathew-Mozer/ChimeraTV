using UnityEngine;
using System.Collections;
using System;

public class RaceCar : MonoBehaviour {
	public bool _isLerping;
    float timeTakenDuringLerp = 5f;
    float _timeStartedLerping;
    Vector3 _endPosition;
    Vector3 _startPosition;
    public UILabel lblPlayername;
    public UISprite CarSprite;
    public GameObject[] keys;
    public float percentageComplete;
    public bool LargeSprite;
    // Use this for initialization
	void Start () {
        //CleanCache();
        CarSprite = gameObject.GetComponent<UISprite>();
        Vector2 spriteDims;
        if (LargeSprite)
        {
            spriteDims = RaceManager.raceManager.LargeSpriteSize[DisplayManager.displayManager.currentScene.pointsGTData.SpriteAtlas];
        }
        else
        {
            spriteDims = RaceManager.raceManager.SpriteSize[DisplayManager.displayManager.currentScene.pointsGTData.SpriteAtlas];
        }
        
        
        CarSprite.width = (int)spriteDims.x;
        CarSprite.height = (int)spriteDims.y;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (_isLerping)
        {

            float timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;
            gameObject.transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
            float step = 1.5f * Time.deltaTime;
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
	}

    public IEnumerator moveCar(Vector3 newLocation,float waitTime){

        yield return new WaitForSeconds(waitTime);
        _startPosition = gameObject.transform.localPosition;
        _endPosition = newLocation;
        _timeStartedLerping = Time.time;
        _isLerping = true;
    }
    public void startMove(Vector3 newLocation)
    {
        gameObject.AddComponent<TweenPosition>();
        TweenPosition tp = gameObject.GetComponent<TweenPosition>();

        tp.from = gameObject.transform.localPosition;
        tp.to = newLocation;
        tp.PlayForward();
        tp.duration = (float)RandomNumberBetween(4, 7);
    }
    public void setName(string name){
        lblPlayername.text = name;
    }
    public void setCar(string carID,Vector2 spriteDims)
    {
        
        //CarSprite.spriteName = carID;
       


    }
    public void ActivateKey(int keyid,bool Activate, string keyColor)
    {
        keys[keyid].SetActive(Activate);
        keys[keyid].GetComponent<UISprite>().color=HexToColor(keyColor);
    }

    internal void ActivateKey(int v1, bool v2)
    {
        ActivateKey(v1, v2, "FFFFFF");
    }
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
    private static readonly System.Random random = new System.Random();

    private static double RandomNumberBetween(double minValue, double maxValue)
    {
        var next = random.NextDouble();

        return minValue + (next * (maxValue - minValue));
    }
    public static void CleanCache()
    {
        if (Caching.ClearCache())
        {
            Debug.Log("Successfully cleaned the cache.");
        }
        else
        {
            Debug.Log("Cache is being used.");
        }
    }
}
