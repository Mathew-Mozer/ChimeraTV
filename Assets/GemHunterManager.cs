using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GemHunterManager : MonoBehaviour {
    public GameObject personPrefab;
    public GameObject playerGrid;
    public List<GemHunterPlayerObject> GHPlayers = new List<GemHunterPlayerObject>();
    public List<UISprite> GemsUsed = new List<UISprite>();
    public List<string> tempNames = new List<string> { "Elizabeth", "Charles", "Mary", "Martha", "John", "Harold", "Frank", "Eugene", "Angela", "Marie", "Jason", "Paula", "Andrew", "Susan", "Paula", "Carl", "Donna", "Beverly", "Katherine", "Shawn", "Mathew", "Brian", "Antonio", "Keith", "Lisa" };
    // Use this for initialization
    void Start () {
        GemHunterPlayer ghp = new GemHunterPlayer();
        GameObject newObject;
        foreach (string player in tempNames)
        {
            ghp = new GemHunterPlayer();
            ghp.PlayerName = player;
            int ttlGems = Random.Range(1, 20);
            for (int i = 0; i < ttlGems; i++)
            {
                ghp.GemsAchieved.Add(i);
            }
         newObject = NGUITools.AddChild(playerGrid, personPrefab);
        newObject.GetComponent<GemHunterPlayerObject>().setPlayer(ghp);
        playerGrid.GetComponent<UIGrid>().repositionNow = true;
        playerGrid.GetComponent<UIGrid>().Reposition();
        }
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
