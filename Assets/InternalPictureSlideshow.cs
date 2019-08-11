using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class InternalPictureSlideshow : MonoBehaviour
{
    public GameObject Texture1;
    public GameObject Texture2;
    public int SceneID;
    public PictureViewer pictureViewer;
    //public PictureViewer PictureViewerData;
    // Use this for initialization
    void Start () {
        
	}
    public void UpdateMenuChild()
    {
        Debug.Log("Picture Child Heard THAT!");
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
        gameObject.transform.localPosition = new Vector3(-967, 543);
    }

    public void setProperties(MenuItemObject MenuItem)
    {
        SceneID = MenuItem.promoid;
        Texture1.GetComponent<UITexture>().pivot = UIWidget.Pivot.TopLeft;
        //Texture1.GetComponent<RectTransform>().sizeDelta = new Vector2(height,width);
        Texture1.GetComponent<UITexture>().height = MenuItem.height*2;
        Texture1.GetComponent<UITexture>().width = MenuItem.width*2;
        Texture1.transform.localPosition = new Vector3(ConvertLeft(MenuItem.left), ConvertTop(MenuItem.top));
        Debug.Log("Loading Picture Data:" + MenuItem.promoid);
        StartCoroutine(getPictureSlideShowData(MenuItem.promoid));
    }
    private IEnumerator getPictureSlideShowData(int promoid)
    {
        while (true)
        {
            var form = new WWWForm();
            form.AddField("action", "GetPictureSlideShowSettings");
            form.AddField("promoid", promoid);
            //form.AddField("macAddress", macAddress);
            // Start a download of the given URL
            Debug.Log("url: " + DisplayManager.displayManager.url);
            WWW www = new WWW(DisplayManager.displayManager.url, form);
            // Wait for download to complete
            yield return www;

            // Checks for WWW error
            if (!string.IsNullOrEmpty(www.error))
            {
                //Puts error information into debug window
            }
            else
            {
                Debug.Log("txt:" + www.text);
                DeserializeSlideShowData(www.text);
            }
            Debug.Log("getting Picture Information");
            yield return new WaitForSeconds(30);
            

        }
        
    }

    private void DeserializeSlideShowData(string www)
    {
     
        try
        {

            pictureViewer = JsonConvert.DeserializeObject<PictureViewer>(www);
            foreach (PictureData pics in pictureViewer.PictureList)
            {
                DisplayManager.displayManager.GetTextureManager().LoadTexture(pics.FileName, DisplayManager.displayManager.currentManager);
                Debug.Log("CHK:" + pics.FileName + ": " + pics.Duration);
            }

            gameObject.GetComponent<PictureManager>().StartTheSlideShow(pictureViewer);
        }
        catch (JsonReaderException e)
        {
            Debug.Log("Error: " + e.Message);
            //addtodebug(e.Message);
            //addtodebug(www);

        }
    }
    private float ConvertLeft(int left)
    {
        return (left) * 2;
    }
    private float ConvertTop(int Top)
    {

        return -(Top) * 2;
    }
}
