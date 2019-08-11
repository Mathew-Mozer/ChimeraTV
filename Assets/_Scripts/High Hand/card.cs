using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class card : MonoBehaviour
{
    float rotSpeed = 60; // degrees per second
    bool rotate = false;
    public bool flip;
    public GameObject CoinPrefab;
    public bool Face = true;
    float tmpnum;
    public int cardvalue;
    Vector3 to;
    public GameObject flyingCard;
    Vector3 loc;
    TweenRotation tweenRotation;
    TweenScale tweenScale;
    GameObject parentObject;
    public int sequenceNumber;
    void Start()
    {
        tweenRotation = GetComponent<TweenRotation>();
        
        tweenRotation.duration = 3f;
        parentObject = GameObject.FindGameObjectWithTag("MainWindow");

    }


    public void flipCard(int Thecardvalue)
    {
        flip = true;

        tweenRotation.enabled = true;
        if (Face)
        {

            //Animate flying card into prizes box

            //LaunchFlyingCard();

            to = new Vector3(0, 180, 0);
            Face = false;
            cardvalue = Thecardvalue;
            
           
        }
        else
        {
            to = new Vector3(0, 0, 0);
            Face = true;
            cardvalue = 0;
            tweenRotation.PlayReverse();
        }

    }


    // Update is called once per frame


    void Update()
    {
        tmpnum = gameObject.transform.rotation.eulerAngles.y;
        if (tmpnum > 90)
        {
            gameObject.GetComponent<UISprite>().depth = 2;
            gameObject.transform.GetChild(0).GetComponent<UISprite>().depth = 3;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().enabled = false;
            setChildDepth(1);

        }
        if (tmpnum < 90 || tmpnum > 270)
        {

            if (gameObject.transform.GetChild(0).GetComponent<UISprite>())
            {
                gameObject.GetComponent<UISprite>().depth = 4;
                gameObject.transform.GetChild(0).GetComponent<UISprite>().depth = 0;
                setChildDepth(8);
                gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().enabled = true;
            }

        }

        /*
        tmpnum = gameObject.transform.rotation.eulerAngles.y;
        if (flip){
            if (Vector3.Distance(transform.eulerAngles, to) > 0.01f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = to;
                flip = false;
            } 
            if (tmpnum > 90)
         {
             gameObject.GetComponent<UISprite>().depth = 1;
                gameObject.transform.GetChild(0).GetComponent<UISprite>().depth = 3;
                //gameObject.transform.GetChild(0).localScale = new Vector3(-1, 0, 0);
            }
         if (tmpnum < 90 || tmpnum >270)
         {
             gameObject.GetComponent<UISprite>().depth =3;
                gameObject.transform.GetChild(0).GetComponent<UISprite>().depth = 1;
             
         }
        }
      
        
        */
    }

    private void setChildDepth(int v)
    {
        gameObject.transform.GetChild(2).GetComponent<UISprite>().depth = v;
        gameObject.transform.GetChild(3).GetComponent<UISprite>().depth = v;
        gameObject.transform.GetChild(4).GetComponent<UISprite>().depth = v;
        gameObject.transform.GetChild(5).GetComponent<UISprite>().depth = v;
        gameObject.transform.GetChild(6).GetComponent<UISprite>().depth = v;
    }
    /*
    IEnumerator LaunchCoin(int coinCount)
    {
        int i=0;
        while (i < coinCount/2)
        {
            i++;
            yield return new WaitForSeconds(.05f);
            Vector3 instPos = transform.position;
            instPos.Set(transform.position.x + Random.Range(-.069f, .069f), transform.position.y + Random.Range(-.069f, .069f), transform.position.z);
            Instantiate(CoinPrefab, instPos, Quaternion.Euler(new Vector3(90, 0, 0)));

        }
    }
    */
    internal void reset()
    {
        if (!Face)
        {
            flipCard(0);
        }
    }

    public void LaunchFlyingCard()
    {
        //This initiates the flying card
        GameObject clone;
        loc = new Vector3(250f, -40f, -238f);
        clone = NGUITools.AddChild(parentObject, flyingCard);
        //clone = Instantiate(flyingCard, loc, Quaternion.identity) as GameObject;
        clone.GetComponent<RectTransform>().anchoredPosition = loc;
        clone.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 0);
        clone.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        clone.transform.rotation = Quaternion.identity;

        //clone.transform.parent = parentObject.transform;

        //clone.GetComponent<TweenPosition>().from = gameObject.transform.localPosition;

        clone.transform.GetChild(0).GetComponent<FlyingCard>().StartTrail();
        clone.transform.GetChild(0).GetChild(0).GetComponent<mmLargeCard>().setCard(new mmCard(gameObject.GetComponent<mmLargeCard>().CardString), "clone");

    }


}
