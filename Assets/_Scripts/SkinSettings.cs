using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System.Reflection.Emit;
using System;
using TMPro;

//using UnityEditor;

public class SkinSettings : MonoBehaviour
{
    public string skinTag;
    public int skinTagId;
    public string skinName;
    public int skinId;
    private Odometer prizeODO;
    private OdometerDigit PrizeODODollarSign;

    IEnumerator SaveSkin()
    {
        SkinElement currentSkinElement = new SkinElement();
        currentSkinElement.id = skinTagId;
        
        currentSkinElement.xCoord = transform.localPosition.x.ToString();
        currentSkinElement.yCoord = transform.localPosition.y.ToString();
        currentSkinElement.backsprite = "";
        currentSkinElement.tagname = skinTag;
        currentSkinElement.foresprite = "";
        
        if (gameObject.GetComponent<Odometer>() != null)
        {
            prizeODO = gameObject.GetComponent<Odometer>();
            currentSkinElement.backcolor = ColorToHex(prizeODO.backBackgroundColor);
            currentSkinElement.forecolor = ColorToHex(prizeODO.backgroundColor);
            currentSkinElement.textcolor = ColorToHex(prizeODO.fontColor);
        }
        if (gameObject.GetComponent<OdometerDigit>() != null)
        {
            PrizeODODollarSign = gameObject.GetComponent<OdometerDigit>();
            currentSkinElement.backcolor = ColorToHex(PrizeODODollarSign.backBackgroundColor);
            currentSkinElement.forecolor = ColorToHex(PrizeODODollarSign.backgroundColor);
            currentSkinElement.textcolor = ColorToHex(PrizeODODollarSign.fontColor);
        }
        if (gameObject.GetComponent<UILabel>() != null)
        {
            UILabel currentLabel = gameObject.GetComponent<UILabel>();
            currentSkinElement.bordercolor = ColorToHex(currentLabel.effectColor);
            currentSkinElement.textcolor = ColorToHex(currentLabel.color);
        }
        if (gameObject.GetComponent<TextMeshPro>() != null)
        {
            TextMeshPro currentLabel = gameObject.GetComponent<TextMeshPro>();
            //currentSkinElement.bordercolor = ColorToHex(currentLabel.effectColor);
            currentLabel.faceColor = currentLabel.color;
        }
        
        if (gameObject.name == ("Camera"))
        {
            currentSkinElement.backcolor = ColorToHex(gameObject.GetComponent<Camera>().backgroundColor);
        }
        if (bg != null)
        {
            currentSkinElement.backcolor=ColorToHex(bg.color);
        }
        if (bordercolor != null)
        {
            currentSkinElement.bordercolor = ColorToHex(bordercolor.color);
        }
        if (gameObject.GetComponent<UILabel>() != null)
        {
           
        }
        if (txtcolor != null)
            currentSkinElement.textcolor = ColorToHex(txtcolor.color);
        
        if (bgsprite != null)
        {
            currentSkinElement.width = bgsprite.width.ToString();
            currentSkinElement.height = bgsprite.height.ToString();
        }

        var form = new WWWForm();
        form.AddField("action", "SaveSkinSettings");
        form.AddField("appVersion", Application.version);
        form.AddField("macAddress", DisplayManager.displayManager.macAddress);
        form.AddField("skinData", JsonUtility.ToJson(currentSkinElement));

        // Start a download of the given URL
        WWW www = new WWW(DisplayManager.displayManager.url, form);
        // Wait for download to complete
        yield return www;
        Debug.Log(www.text);
    }

    public UISprite fg;
    public UISprite bg;
    public UISprite bgsprite;
    public UISprite bordercolor;
    public UILabel txtcolor;
    public TextMeshPro txtColorTM;

    //HighHandTopLeftTitle,HighHandTopLeftContent,HighHandTopRightTitle,HighHandTopRightContent,HighHandPrize,HighHandScene,HighHandName,HighHandPrevHand
    void Awake()
    {
        Skinit();
    }

    public void Skinit()
    {
        SkinElement SE = DisplayManager.displayManager.currentScene.sceneSkin.skinData.Find(se => (se.tagname.Equals(skinTag)));
        if (SE)
        {



            this.skinTagId = SE.id;
            this.skinId = DisplayManager.displayManager.currentScene.sceneSkin.skinID;
            this.skinName = DisplayManager.displayManager.currentScene.sceneSkin.skinName;
            if (SE != null)
            {
                if (fg != null)
                    fg.color = HexToColor(SE.forecolor);

                if (gameObject.GetComponent<Odometer>() != null)
                {
                    prizeODO = gameObject.GetComponent<Odometer>();
                    prizeODO.backBackgroundColor = HexToColor(SE.backcolor);
                    prizeODO.backgroundColor = HexToColor(SE.forecolor);
                    prizeODO.fontColor = HexToColor(SE.textcolor);
                }
                if (gameObject.GetComponent<OdometerDigit>() != null)
                {
                    PrizeODODollarSign = gameObject.GetComponent<OdometerDigit>();
                    PrizeODODollarSign.backBackgroundColor = HexToColor(SE.backcolor);
                    PrizeODODollarSign.backgroundColor = HexToColor(SE.forecolor);
                    PrizeODODollarSign.fontColor = HexToColor(SE.textcolor);
                }
                if (gameObject.GetComponent<UILabel>() != null)
                {
                    UILabel currentLabel = gameObject.GetComponent<UILabel>();
                    currentLabel.effectColor = HexToColor(SE.bordercolor);
                    currentLabel.color = HexToColor(SE.textcolor);
                }
                if (gameObject.GetComponent<TextMeshPro>() != null)
                {
                    TextMeshPro currentLabel = gameObject.GetComponent<TextMeshPro>();
                    currentLabel.color = HexToColor(SE.textcolor);
                }
                if (gameObject.name == ("Camera"))
                {
                    gameObject.GetComponent<Camera>().backgroundColor = HexToColor(SE.backcolor);
                }
                if (bg != null)
                {
                    bg.color = HexToColor(SE.backcolor);
                }
                if (bordercolor != null)
                {
                    bordercolor.color = HexToColor(SE.bordercolor);
                }
                if (gameObject.GetComponent<UILabel>() != null)
                {
                    if (bordercolor == null)
                    {
                        txtcolor.effectStyle = UILabel.Effect.Outline;
                        txtcolor.effectColor = HexToColor(SE.bordercolor);
                    }
                }
                if (txtcolor != null)
                    txtcolor.color = HexToColor(SE.textcolor);
                if (txtColorTM)
                {
                    txtColorTM.color = HexToColor(SE.textcolor);
                    
                }
                if (bgsprite != null)
                {
                    if (skinTag.Equals("Logo"))
                    {
                        bgsprite.atlas = GameObject.FindGameObjectWithTag("Skin").GetComponent<UIAtlas>();
                    }
                    
                    if (SE.backsprite.Equals("DefaultLogo"))
                    {
                        bgsprite.spriteName = DisplayManager.displayManager.displayData.DefaultLogo;

                    }
                    else
                    {
                        bgsprite.spriteName = SE.backsprite;
                    }
                    int number;
                    bool result = Int32.TryParse(SE.width, out number);
                    if (result)
                    {
                        bgsprite.width = int.Parse(SE.width);
                    }
                    else
                    {
                        Debug.Log("Couldn't parse bgsprite width:" + SE.tagname);
                    }
                    result = Int32.TryParse(SE.height, out number);
                    if (result)
                    {
                        bgsprite.height = int.Parse(SE.height);
                    }
                    else
                    {
                        Debug.Log("Couldn't parse bgsprite height:" + SE.tagname);
                    }


                }
                if (!string.IsNullOrEmpty(SE.xCoord) && !string.IsNullOrEmpty(SE.yCoord))
                {

                    Vector3 temp = new Vector3(float.Parse(SE.xCoord), float.Parse(SE.yCoord), 0);
                    transform.localPosition = temp;

                }
            }
            else
            {
                //Debug.Log("Can't Find:" +skinTag);
            }
        }
    }

    public static Color HexToColor(string hex)
    {
        if (hex.Equals(""))
        {
            return new Color32(0, 0, 0, 255);
        }
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        
        
        
    }

    string ColorToHex(Color myColor)
    {
       return myColor.r.ToString("X2") + myColor.g.ToString("X2") + myColor.b.ToString("X2");
    }
    [InspectorButton("OnButtonClicked")]
    public bool clickMe;

    private void OnButtonClicked()
    {
        Debug.Log("Saved Skin");
        StartCoroutine(SaveSkin());
    }
}

