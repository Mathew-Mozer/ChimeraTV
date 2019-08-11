using UnityEngine;
using System.Collections;

public class objWinner : MonoBehaviour
{
    public PeWinner theWinner;
    public bool falling = false;
    public GameObject Playername;
    public GameObject wintype;
    public GameObject winamount;
    public GameObject RightContainer;
    public GameObject LeftContainer;
    private Vector3 startPosition;
    private bool scrolling;
    private bool leftToRight;
    private Vector3 staticPlacement;
    // Use this for initialization
    void Start()
    {

    }
    public void setPlayer(PeWinner newWinner, bool leftDrop, bool falling, bool scrolling, bool leftToRight,Vector3 staticPlacement)
    {
        Vector3 startPoint = new Vector3(480,450,0);
        if (falling)
        {
            if (leftDrop)
            {
                startPoint = new Vector3(-480, 450, 0);
            }
        }
        if (scrolling)
        {
            startPoint = new Vector3(-1000, -275, 0);
            if (leftToRight)
                startPoint = new Vector3(1000, -275, 0);
        }
        if (!scrolling && !falling)
        {
            transform.position = staticPlacement;
        }
        gameObject.transform.localPosition = startPoint;
        Playername.GetComponent<UILabel>().text = newWinner.name;
        wintype.GetComponent<UILabel>().text = newWinner.ptype ;
        winamount.GetComponent<UILabel>().text = newWinner.prize;
        this.falling = falling;
        theWinner = newWinner;
        this.scrolling = scrolling;
        this.leftToRight = leftToRight;
        this.staticPlacement = staticPlacement;
        foreach (Transform child in LeftContainer.transform)
        {
            //Debug.Log(newWinner.LeftIcon + " = " + child.gameObject.name);
            if (child.name.Equals(newWinner.lefticon))
            {
                child.gameObject.SetActive(true);
            }else
            {
                child.gameObject.SetActive(false);
            }
            
        }
        foreach (Transform child in RightContainer.transform)
        {
            if (child.name.Equals(newWinner.righticon))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10);
        if (theWinner != null)
        {
            if (falling)
            {
                gameObject.GetComponent<Rigidbody>().drag = 20;
                //Debug.Log(theWinner.PlayerName+ " is falling");
            }else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            if (scrolling)
            {
                if (leftToRight)
                {
                       transform.position += Vector3.left * Time.deltaTime/2;
                }else
                {
                    transform.position += Vector3.right * Time.deltaTime/2;
                }
                
            }
           
        }
    }
}
