using UnityEngine;
using System.Collections;

public class PrizeList : MonoBehaviour
{
    public GameObject prizeListPrefab;
    public UIAtlas PrizeAtlas;

    public void setPrizeList(string PrizeList)
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        string[] prizeList = PrizeList.Split(',');
        foreach (string pl in prizeList)
        {
            GameObject gm = NGUITools.AddChild(gameObject, prizeListPrefab);
            gm.GetComponent<PrizeListItem>().setPrizeLabel(pl);
            gm.GetComponent<UISprite>().atlas = PrizeAtlas;
        }
        gameObject.GetComponent<UIGrid>().repositionNow=true;
    }
	
}
