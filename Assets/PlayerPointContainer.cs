using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPointContainer : MonoBehaviour {

    public GameObject Points;
    public GameObject Name;
    public GameObject Prize;
    private int points;
    public List<UITexture> InstantWinners;
    public int Place;
    public string lastUpdated;
    public bool switchPrizes;
    public string[] prizes;
    private int currentPrize=0;
    private bool flip = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (lastUpdated != DisplayManager.displayManager.currentScene.lastUpdated)
        {
            if (DisplayManager.displayManager.currentScene.pointsGTData.playerListJson != null)
            {
                points = DisplayManager.displayManager.currentScene.pointsGTData.playerListJson[Place].Points;
                Points.GetComponent<UILabel>().text = String.Format("{0:n0}", points);
                Name.GetComponent<UILabel>().text = truncateName(DisplayManager.displayManager.currentScene.pointsGTData.playerListJson[Place].PlayerName.ToString());
                int curIW = 0;
                if (DisplayManager.displayManager.currentScene.pointsGTData.InstantWinners != null)
                {
                    foreach (pgtInstantWinner iw in DisplayManager.displayManager.currentScene.pointsGTData.InstantWinners)
                    {

                        if (points >= iw.PointAmount)
                        {
                            InstantWinners[curIW].gameObject.SetActive(true);
                            InstantWinners[curIW].color = DisplayManager.HexToColor(DisplayManager.displayManager.currentScene.pointsGTData.InstantWinners[curIW].IconColor);
                        }else{ }
                        curIW++;
                    }
                }
            }

            string[] thePrizes = DisplayManager.displayManager.currentScene.pointsGTData.PayoutList.Split(',');
            if (thePrizes.Length > Place)
            {
                prizes = thePrizes[Place].Split(';');
                if (prizes.Length == 0)
                {
                    prizes[0] = "";
                    switchPrizes = false;
                }
                else if(prizes.Length>1)
                {
                    switchPrizes = true;
                }
                else
                {
                    switchPrizes = false;
                }
                if (Prize.GetComponent<UILabel>().text.Equals("$0")){
                    Prize.GetComponent<UILabel>().text = prizes[0];
                }

                //String.Format("${0:n0}", int.Parse(thePrizes[Place].Substring(1, thePrizes[Place].Length-1)));
            }
            
        }
        if (switchPrizes)
        {
            Prize.GetComponentInParent<TweenScale>().enabled = true;
            
            if (Prize.transform.parent.gameObject.transform.localScale.y <.01)
            {
                if (flip)
                {
                    flip = false;
                    currentPrize++;
                    if (currentPrize == prizes.Length)
                        currentPrize = 0;
                    Prize.GetComponent<UILabel>().text = prizes[currentPrize];
                    Debug.Log("Scale is small: " + currentPrize + " - " + prizes[currentPrize]);
                }
            }
            else
            {
                flip = true;
            }
        }
        else
        {
            Prize.GetComponentInParent<TweenScale>().enabled = false;
            Prize.GetComponent<UILabel>().text = prizes[0];
        }
	}
    private string truncateName(string p)
    {
        string newstring = "";
        if (!p.Equals(""))
        {
            p = p.Replace('.', ' ');
            string[] tmp = p.Split(' ');

            switch (tmp.Count())
            {
                case 1:
                    newstring = FirstCharToUpper(p);
                    break;
                case 2:
                    newstring = FirstCharToUpper(tmp[0]) + " " + tmp[1].Substring(0, 1).ToUpper() + ".";
                    break;
                case 3:
                    if (tmp[2].Length < 1)
                    {
                        newstring = FirstCharToUpper(tmp[0]) + " " + tmp[1].Substring(0, 1).ToUpper() + ".";
                    }
                    else
                    {
                        newstring = FirstCharToUpper(tmp[0]) + " " + tmp[2].Substring(0, 1).ToUpper() + ".";
                    }
                    break;
                default:
                    break;
            }
        }
        return newstring;
    }
    public static string FirstCharToUpper(string input)
    {
        if (input.Length > 0)
        {
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }
        else
        {
            return "";
        }
    }
}
