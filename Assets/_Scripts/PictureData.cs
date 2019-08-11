using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
[Serializable]
/// <summary>
/// Summary description for PictureData
/// </summary>
public class PictureData 
{
    public string FileName="";
    public int Duration = 0;
    public string HashData="";

    public PictureData()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public bool Equals(PictureData other)
    {
        // Would still want to check for null etc. first.
        Debug.Log("Looking at:" + other.FileName + " is: " + this.FileName);
        return this.FileName.Equals(other.FileName);
    }
}