using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
public class TreasureCrateManager : MonoBehaviour
{
    public TC_Session currentTC;
    public int picksLeft;
    public GameObject pickWindow;
    public int totalMoney;
    public GameObject payoutWindow;
    public bool picksEnabled = true;
    public GameObject treasureContainer;
    public UILabel lblPayAmount;
    public DisplayManager displayManager;
    public bool isMaster=false;
   
    IEnumerator Start()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<DisplayManager>();
        picksEnabled = true;
        while (!isMaster)
        {

            if (!isMaster)
            {
                StartCoroutine("LoadCrates");
            }
                yield return new WaitForSeconds(5);
            
        }
      
       
        
        
    }

    IEnumerator LoadCrates()
    {
        Debug.Log("loading Crates");
        var form = new WWWForm();

        form.AddField("activity", "CurrentTC");
        //form.AddField("casinoid", displayManager.displayInfo.casinoID);

        // Start a download of the given URL
        WWW www = new WWW(displayManager.url, form);

        // Wait for download to complete
        yield return www;
        if (www.error == null)
        {
            if (!www.text.Contains("norows"))
            {
                Debug.Log(www.text);
                XmlSerializer Xml_Serializer = new XmlSerializer(typeof(TC_Session));
                using (StringReader reader = new StringReader(www.text))
                {

                    currentTC = (TC_Session)Xml_Serializer.Deserialize(reader);
                }
            }
        }
        foreach (Transform child in treasureContainer.transform)
        {
            //CrateClick crate = child.gameObject.GetComponent<CrateClick>();
            //crate.setCrate(currentTC.crateHolder.crates.Find(r => r.crateID == crate.crateID);
        }
    }
    void Update()
    {
        lblPayAmount.text = "$" + totalMoney;
        if (picksEnabled)
        {
            pickWindow.transform.Find("pwLbl").GetComponent<UILabel>().text = picksLeft.ToString();
            if (picksLeft > 0)
            {
                picksEnabled = true;
            }
            else
            {
                picksEnabled = false;
            }
        }
        
    }
    //Called from Crate when clicked on.
    //Send information back to database.
    public void openCrate(int crateID,bool sendReport)
    {
        picksLeft--;
        pickWindow.transform.Find("pwLbl").GetComponent<UILabel>().text = picksLeft.ToString();
        Crate currentCrate = currentTC.crateHolder.crates.Find(r => r.crateID == crateID);
        
        
        switch (currentCrate.crateType)
        {
            case 0:
                totalMoney = totalMoney + currentCrate.value;
                break;
            case 1:
                picksLeft = picksLeft + currentCrate.value;
                break;
        }
        if (picksLeft == 0)
        {
            picksEnabled = false;
        }
    }
}
