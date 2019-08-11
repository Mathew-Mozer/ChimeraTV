using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

internal static class FileTools
{
    public static string picturepath;
    public static string DownloadBackgroundURL = "http://serpent.typhonconnect.com/dependencies/images/MenuObjects/Backgrounds/";

    public static string PicturePath()
    {
            picturepath = Application.persistentDataPath + directorySeperator + "pictures" + directorySeperator;
            if (!Directory.Exists(picturepath))
            {
                Directory.CreateDirectory(picturepath);
            }
            return picturepath;
    }
    public static string getFilename(string filePath)
    {
        string tmp = "";
        string[] fileparts;
        if (filePath.Contains('\\'))
        {
            fileparts = filePath.Split('\\');
        }
        else
        {
            fileparts = filePath.Split('/');
        }


        tmp = fileparts[fileparts.Length - 1];
        return tmp;
    }
    
    public static string directorySeperator = "\\";

    internal static string GetFileFromURL(string fileName)
    {
        string[] fileParts = fileName.Split('/');https://msdn.microsoft.com/en-us/library/system.io.path.getfilename(v=vs.110).aspx
        return fileParts.Last();
    }

    
    public static Texture2D OpenFileAsTexture(string filePath)
    {
        Texture2D tex;
        if (!DisplayManager.displayManager.textureManager.listTexture2D.TryGetValue(Path.GetFileName(filePath), out tex))
        {
            byte[] fileData;
            fileData = File.ReadAllBytes(PicturePath() + getFilename(filePath));
            tex=new Texture2D(2,2);
            tex.LoadImage(fileData);
            DisplayManager.displayManager.textureManager.listTexture2D.Add(Path.GetFileName(filePath), tex);
            return DisplayManager.displayManager.textureManager.listTexture2D[Path.GetFileName(filePath)];
        }
        return tex;
    }
}