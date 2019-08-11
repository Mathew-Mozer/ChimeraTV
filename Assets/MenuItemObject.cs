using System.Diagnostics;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

public class MenuItemObject
{
    public int height;
    public int left;
    public int top;
    public int width;
    public string text;
    public string type;
    public string fontsize;
    internal string key;
    public string color;
    public string image;
    public int promoid;

    public MenuItemObject()
    {
        //UnityEngine.Debug.Log("Created Object Called:" + text);
    }

    public MenuItemObject(string fontsize, int height, int left, string text, int top, string type, int width,string color)
    {
        this.fontsize = fontsize;
        this.height = height;
        this.left = left;
        this.text = text;
        this.top = top;
        this.type = type;
        this.width = width;
        this.color = color;
    }
    public MenuItemObject(int height, int width, int top, int left, string type,int promoid)
    {
        this.height = height;
        this.left = left;
        this.top = top;
        this.type = type;
        this.width = width;
        this.promoid = promoid;

    }
}