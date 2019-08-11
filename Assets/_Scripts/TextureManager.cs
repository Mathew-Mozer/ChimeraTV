using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public Dictionary<string, Texture2D> listTexture2D = new Dictionary<string, Texture2D>();
    public List<string> AllTextures = new List<string>();
    public Texture2D LoadingTexture;

    public Texture2D LoadTexture(string filename, GameObject responseGameObject)
    {
        string shortFile = FileTools.PicturePath() + FileTools.directorySeperator + FileTools.GetFileFromURL(filename);
        //Debug.Log("Checking file:" + shortFile);
        Texture2D tmpTexture2D = null;
        if (File.Exists(shortFile))
        {
            //Debug.Log("File is already downloaded: " + shortFile);
            //return loadTextureFile(tmppd, filePath, index, scenes.promoID);
            tmpTexture2D = FileTools.OpenFileAsTexture(shortFile);
        }
        else
        {
            //Debug.Log("Downloading File: " + filename);
            DisplayManager.displayManager.downloadImage(filename, responseGameObject);
            tmpTexture2D = LoadingTexture;
        }
        return tmpTexture2D;
    }
    public Texture2D DownloadBackgroundTexture(string filename, GameObject responseGameObject)
    {
        return LoadTexture(FileTools.DownloadBackgroundURL+filename, responseGameObject);
    }

    

    /*
    public IEnumerator OlddownloadImg(PictureData pd, int index, bool backgroundImg)
    {
        
        Texture2D img;
        yield return 0;
        
        WWW imgLink = new WWW(pd.FileName);
        yield return imgLink;
        if (imgLink.error == null)
        {

            img = imgLink.texture;


            if (img.width == 8 && img.height == 8)
            {
                //pd.Texture = ;
                img = (Texture2D)noImageTexture;
                pd.Texture = img;
                Debug.Log("something is wrong");
            }
            else
            {
                string fullPath = "";
                pd.Texture = ScaleTexture(img, 1280, 768);
                if (backgroundImg)
                {
                    fullPath = FileTools.PicturePath() + "backgrounds\\" + getFilename(pd.FileName);
                }
                else
                {
                    fullPath = FileTools.PicturePath() + sceneid + "\\" + getFilename(pd.FileName);
                }

                Debug.Log("BKI Saving to: " + fullPath);

                File.WriteAllBytes(fullPath, imgLink.bytes);

            }

            if (!backgroundImg)
            {
                if (index == -1)
                {
                    CurrentPictures.Add(pd);
                }
                else
                {
                    CurrentPictures[index] = pd;
                }
            }
            Debug.Log("BKI Sending Broadcast");
            currentManager.BroadcastMessage("toManager", "Downloaded Background");
        }
        else
        {
            Debug.Log("www error:" + imgLink.error);
            addtodebug("Downloading Image Error: " + imgLink.error);
        }
        
    }*/

}