using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GemHunterPlayerObject : MonoBehaviour {
    public List<int> GemsAchieved = new List<int>();
    public GemHunterPlayer currentPlayers;
    public UILabel playername;
    public GameObject GemList;
    int currentGem=0;
    bool blast = false;
    

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    
    internal void setPlayer(GemHunterPlayer ghp)
    {
        playername.text = ghp.PlayerName;
        GemsAchieved = ghp.GemsAchieved;
        blastGems();
    }

    private void blastGems()
    {
        StartCoroutine("blastNow");

    }
    IEnumerator blastNow ()
    {
        while (currentGem < GemsAchieved.Count - 1)
        {

            yield return new WaitForSeconds(Random.RandomRange(1.0f,7.0f));
            string tmp = "diamond_variant_" + Random.Range(1, 11).ToString() + "_" + Random.Range(1, 12).ToString() + "@1x";
            GemList.transform.GetChild(currentGem).gameObject.GetComponent<UISprite>().spriteName = tmp;
            GemList.transform.GetChild(currentGem).gameObject.GetComponent<UISprite>().MakePixelPerfect();
            currentGem++;
        }
    }
}
