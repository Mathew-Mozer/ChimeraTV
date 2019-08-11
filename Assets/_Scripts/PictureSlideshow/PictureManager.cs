using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// <author>Mathew Mozer</author>
/// <date>9/20/2015</date>
/// <version>1.0</version>
/// 
/// This class handles downloading and displaying images as pictures in unity. It also
/// breaks the textures down into segments for transition effects.
/// 
/// <author>Stephen King</author>
/// <date>6/29/2016</date>
/// <version>1.1</version>
/// 
/// Removed non-functional grid/transition system to replace with tweening.
/// 
/// </summary>

public class PictureManager : MonoBehaviour
{
    //public List<Texture2D> TextureList = new List<Texture2D>();
    //public List<string> UrlList = new List<string>();

    // Use this for initialization
    [SerializeField]
    float rotateTime;
    public UITexture textureObject;
    public UITexture textureObject2;

    //Swipe Left
    [SerializeField]
    int swipeLeftInFromX;
    [SerializeField]
    int swipeLeftInFromY;
    [SerializeField]
    int swipeLeftOutToX;
    [SerializeField]
    int swipeLeftOutToY;

    //Swipe Right
    [SerializeField]
    int swipeRightInFromX;
    [SerializeField]
    int swipeRightInFromY;
    [SerializeField]
    int swipeRightOutToX;
    [SerializeField]
    int swipeRightOutToY;

    //Swipe Up
    [SerializeField]
    int swipeUpInFromX;
    [SerializeField]
    int swipeUpInFromY;
    [SerializeField]
    int swipeUpOutToX;
    [SerializeField]
    int swipeUpOutToY;

    //Swipe Down
    [SerializeField]
    int swipeDownInFromX;
    [SerializeField]
    int swipeDownInFromY;
    [SerializeField]
    int swipeDownOutToX;
    [SerializeField]
    int swipeDownOutToY;

    string currentURL;
    public bool downloadingStarted;
    public bool PlaySlideshow;
    public GameObject pictureFrame;

    public bool isLocked; //{get;set;}
    [SerializeField]
    bool hasStarted;

    public bool sizeOverride;

    //Transition objects
    SwipeIn swipeIn = new SwipeIn();
    FadeIn fadeIn = new FadeIn();

    DisplayManager displayManager;
    private PictureViewer picViewerContainer;

    enum TransitionEffect
    {
        SWIPE_LEFT,
        SWIPE_RIGHT,
        SWIPE_DOWN,
        SWIPE_UP,
        FADE_IN
    }

    [SerializeField]
    TransitionEffect transitionEffect;

    int currentSlide = 0;

    //Currently depricated
    //public List<GameObject> AttractPoints = new List<GameObject>();
    //public GameObject prefabGrid;


    //
    void Awake()
    {
        DisplayVertical();
        DisplayFlipped();
        displayManager = DisplayManager.displayManager;
        textureObject2.transform.localPosition = new Vector3(999,999,0);
    }

    private void DisplayVertical()
    {
        if (DisplayManager.displayManager.displayData.isVertical)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            textureObject.width = DisplayManager.displayManager.displayData.height;
            textureObject.height = DisplayManager.displayManager.displayData.width;
            textureObject2.width = DisplayManager.displayManager.displayData.height;
            textureObject2.height = DisplayManager.displayManager.displayData.width;
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            textureObject.width = DisplayManager.displayManager.displayData.width;
            textureObject.height = DisplayManager.displayManager.displayData.height;
            textureObject2.width = DisplayManager.displayManager.displayData.width;
            textureObject2.height = DisplayManager.displayManager.displayData.height;
        }
    }

    void Start()
    {
        displayManager = DisplayManager.displayManager;
        displayManager.currentManager = gameObject;
        //TextureList.AddRange(displayManager.displayInfo.PictureList);
        picViewerContainer = displayManager.currentScene.PictureViewerData;
        DisplayVertical();
        DisplayFlipped();
        StartCoroutine(StartSlideShow());
        /// <summary>
        /// As long as there is a texture in the texture list, run the slide show.
        /// </summary>
        /// <returns></returns>
    }

    public void StartTheSlideShow(PictureViewer internaPictureViewer)
    {
        picViewerContainer = internaPictureViewer;
        Debug.Log("Picture Count: "+picViewerContainer.PictureList.Count);
        StartCoroutine(StartSlideShow());
       
    }

    private void DisplayFlipped()
    {
        if (DisplayManager.displayManager.displayData.flipv)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    void Update()
    {
        DisplayVertical();
        DisplayFlipped();
    }

    public void toManager()
    {
        Debug.Log("Its been called");
    }
    //picViewerContainer = picViewerContainer;
    IEnumerator StartSlideShow()
    {
            
            if (!displayManager)
            {
            
            }
            else
            {

                if (!hasStarted)
                {
                    Debug.Log("started" + hasStarted);
                    while (picViewerContainer.PictureList.Count > 0)
                    {
                        //GameObject newImage = (GameObject)Instantiate(prefabGrid, new Vector3(0, 0, 0),transform.rotation);
                        //newImage.transform.SetParent(pictureFrame.transform,false);
                        //newImage.GetComponent<LoadImage>().LoadAtlas(TextureList[currentTexture], rotateTime + 2f);

                        CycleImage();

                        yield return new WaitForSeconds(picViewerContainer.PictureList[currentSlide].Duration);

                        currentSlide++;


                        //Wrap back around to the first texture
                        if (currentSlide > picViewerContainer.PictureList.Count - 1)
                        {
                            currentSlide = 0;
                        }

                    }
                }
                else
                {
                Debug.Log("started should be false: " + hasStarted);
            }
            
            }
       

      

        

    }

    /// <summary>
    /// This scales the texture so that it fits the texture object it will be displayed on.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targetWidth"></param>
    /// <param name="targetHeight"></param>
    /// <returns></returns>
    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }
        result.Apply();
        return result;
    }

    /// <summary>
    /// This rotates out the current image for a new one
    /// </summary>
    /// <param name="i_imageIndex"></param>
    public void CycleImage()
    {
        while (picViewerContainer.PictureList[currentSlide].Duration <= 0)
        {
            currentSlide++;
            if (currentSlide >picViewerContainer.PictureList.Count-1)
            {
                currentSlide = 0;
            }
        }
        //Debug.Log("slide changed to: " + currentSlide);
        //= displayManager.CurrentPictures.FindIndex(s => s.FileName.Equals(fileName))
        if (!hasStarted)
        {
            hasStarted = true;
            
            //If the scene is starting for the first time, randomly transition in a texture
            ExecuteInTransition();
            }
        else
        {
            //Standard operating procedure
            ExecuteOutTransition();
        }
    }

    /// <summary>
    /// This preps slide two with the current image so it can slide in from a direction
    /// </summary>

    public void PrepImage()
    {
        //Destroy(textureObject.mainTexture);
        textureObject2.mainTexture =
            displayManager.textureManager.LoadTexture(picViewerContainer.PictureList[currentSlide].FileName, gameObject);
        //FileTools.OpenFileAsTexture(picViewerContainer.PictureList[currentSlide].FileName);
    }
    
    /// <summary>
    /// This uses a random transition effect on each texture change.
    /// </summary>
    /* void ExecuteRandomInTransition() {

         //switch(Random.Range(0, System.Enum.GetNames(typeof(TransitionEffect)).Length)) {
         switch (Random.Range(0, 2)) {
             case 0:
                 textureObject2.mainTexture = TextureList[currentSlide];
                 swipeIn.SwipeTexture(swipeLeftInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>());
                 transitionEffect = TransitionEffect.SWIPE_LEFT;
                 break;

             case 1:
                 Debug.Log("Fading In");
                 transitionEffect = TransitionEffect.FADE_IN;
                 fadeIn.FadeTextureIn(textureObject.GetComponent<TweenAlpha>());
                 break;
         }
     } */

    /// <summary>
    /// This transitions a texture out before loading a new one
    /// </summary>
    /*  void ExecuteRandomOutTransition() {
          //switch (Random.Range(0, System.Enum.GetNames(typeof(TransitionEffect)).Length)) {
          switch (Random.Range(0, 2)) {
              case 0:

                  if (transitionEffect == TransitionEffect.FADE_IN) {
                      ResetTexturePosition();
                  }
                  Debug.Log("Swiping");
                  swipeIn.SwipeTexture(0, 0, swipeLeftOutToX, swipeLeftOutToY, textureObject.GetComponent<TweenPosition>()); //swipe out
                  PrepImage();
                  swipeIn.SwipeTexture(swipeLeftInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>()); //swipe in
                  break;

              case 1:

                  fadeIn.FadeTextureOut(textureObject.GetComponent<TweenAlpha>());
                  break;
          }
      } */

    /// <summary>
    /// This resets the position of the main texture
    /// </summary>
    public void ResetTexturePosition()
    {
        textureObject.GetComponent<TweenPosition>().ResetToBeginning();
        textureObject2.GetComponent<TweenPosition>().ResetToBeginning();
    }
    
    /// <summary>
    /// This resets the texture's alpha property
    /// </summary>
    public void ResetTextureAlpha()
    {
        TweenAlpha ta = textureObject.GetComponent<TweenAlpha>();
        if (ta.value == 0)
        {
            textureObject.mainTexture = textureObject2.mainTexture;
            
        }
        
        fadeIn.FadeTextureIn(textureObject.GetComponent<TweenAlpha>());
    }

    /// <summary>
    /// This controlls pre-selected in transitions to start the scene
    /// </summary>
    void ExecuteInTransition()
    {
        textureObject.mainTexture =
            DisplayManager.displayManager.textureManager.LoadTexture(
                picViewerContainer.PictureList[currentSlide].FileName,gameObject);
        /*
        switch ((int)transitionEffect)
        {
            case 0:
                textureObject2.mainTexture = getTexture(picViewerContainer.PictureList[currentSlide].FileName);
                swipeIn.SwipeTexture(swipeLeftInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>());
                break;

            case 1:
                textureObject2.mainTexture = getTexture(picViewerContainer.PictureList[currentSlide].FileName);
                swipeIn.SwipeTexture(swipeRightInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>());
                break;

            case 2:
                textureObject2.mainTexture = getTexture(picViewerContainer.PictureList[currentSlide].FileName);
                swipeIn.SwipeTexture(swipeDownInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>());
                break;

            case 3:
                textureObject2.mainTexture = getTexture(picViewerContainer.PictureList[currentSlide].FileName);
                swipeIn.SwipeTexture(swipeUpInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>());
                break;

            case 4:
            */
        transitionEffect = TransitionEffect.FADE_IN;
                fadeIn.FadeTextureIn(textureObject.GetComponent<TweenAlpha>());
        /*
        break;
        }*/
    }
    
    /// <summary>
    /// This controls the pre-selected transition effect. 
    /// </summary>
    void ExecuteOutTransition()
    {
       
        //textureObject.mainTexture = FileTools.OpenFileAsTexture(picViewerContainer.PictureList[currentSlide].FileName);
        switch ((int)transitionEffect)
        {

            case 0:
                swipeIn.SwipeTexture(0, 0, swipeLeftOutToX, swipeLeftOutToY, textureObject.GetComponent<TweenPosition>()); //swipe out
                PrepImage();
                swipeIn.SwipeTexture(swipeLeftInFromX, swipeLeftInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>()); //swipe in
                break;

            case 1:
                swipeIn.SwipeTexture(0, 0, swipeRightOutToX, swipeRightOutToY, textureObject.GetComponent<TweenPosition>()); //swipe out
                PrepImage();
                swipeIn.SwipeTexture(swipeRightInFromX, swipeRightInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>()); //swipe in
                break;

            case 2:
                swipeIn.SwipeTexture(0, 0, swipeDownOutToX, swipeDownOutToY, textureObject.GetComponent<TweenPosition>()); //swipe out
                PrepImage();
                swipeIn.SwipeTexture(swipeDownInFromX, swipeDownInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>()); //swipe in
                break;

            case 3:
                swipeIn.SwipeTexture(0, 0, swipeUpOutToX, swipeUpOutToY, textureObject.GetComponent<TweenPosition>()); //swipe out
                PrepImage();
                swipeIn.SwipeTexture(swipeUpInFromX, swipeUpInFromY, 0, 0, textureObject2.GetComponent<TweenPosition>()); //swipe in
                break;

            case 4:
                //Debug.Log("Out");
                PrepImage();
                fadeIn.FadeTextureOut(textureObject.GetComponent<TweenAlpha>());
                break;
        }
    }
}
