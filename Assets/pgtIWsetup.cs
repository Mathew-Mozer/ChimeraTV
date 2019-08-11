using UnityEngine;
using System.Collections;

public class pgtIWsetup : MonoBehaviour {

    public UISprite goKey;
    public UILabel goPrize;
    public UILabel goAmount;

    public void setupIWKey(pgtInstantWinner iw)
    {
        goPrize.text = iw.PrizeAmount.ToString();
        goAmount.text = iw.PointAmount.ToString();
        
        if(iw.IconColor.Length>4)
        goKey.color =  HexToColor(iw.IconColor);
        
    }
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
