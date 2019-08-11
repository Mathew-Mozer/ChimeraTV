using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LoadImage : MonoBehaviour {
    public string url;
    public Texture2D img;
	// Use this for initialization
	void Start () {

	}

   /* public void LoadAtlas(Texture2D tmpTexture,float TTL)
    {
        Rect[] spriteArray = new Rect[2];
        List<UISpriteData> sprites = new List<UISpriteData>();
        UIAtlas go = gameObject.GetComponent<UIAtlas>();
        Material mat = new Material(Shader.Find(" Diffuse"));
        mat.mainTexture = tmpTexture;
        int newi = 1;
        for (int c = 0; c < 5; ++c)
        {
            for (int r = 0; r < 3; ++r)
            {
                UISpriteData curSpriteData = new UISpriteData();
                curSpriteData.x = 256 * c;
                curSpriteData.y = 240 * r;
                curSpriteData.width = 256;
                curSpriteData.height = 240;
                curSpriteData.name = "Sprite" + c + r;
                sprites.Add(curSpriteData);
                newi++;
            }
        }
        go.spriteList = sprites;
        go.spriteMaterial = mat;


        foreach (Transform child in transform)
        {
            int r = child.gameObject.GetComponent<SpriteSetting>().row;
            int c = child.gameObject.GetComponent<SpriteSetting>().column;
            child.gameObject.GetComponent<UISprite>().spriteName = "Sprite" + c + r;
        }

       StartCoroutine(destroyMe(TTL));
    } 

   IEnumerator destroyMe(float TTL)
    {
        int whichdestroy = Random.Range(1, 4);
       yield return new WaitForSeconds(TTL);
       foreach (Transform child in transform)
       {
      //     child.gameObject.GetComponent<SpriteSetting>().animateDestroy(whichdestroy);
       }
       yield return new WaitForSeconds(1f);
       DestroyObject(gameObject);
    } */
}
