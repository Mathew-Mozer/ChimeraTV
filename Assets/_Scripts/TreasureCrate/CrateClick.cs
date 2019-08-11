using UnityEngine;
using System.Collections;

public class CrateClick : MonoBehaviour
{
    Vector3 openlid = new Vector3(.98f, .63f, 0);
    Vector3 lidrotate = new Vector3(-16f, 16f, -16f);
    Vector3 lidmove = new Vector3(-10f, 12.87f, -7.08f);
    Vector3 closelid = new Vector3(0f, .63f, 0);
    Vector3 _endPosition;
    Vector3 _startPosition;
    public UILabel PayoutLabel;
    float timeTakenDuringLerp = 1f;
    float _timeStartedLerping;
    bool _isLerping;
    public GameObject lid;
    public Crate currentCrate;
    public int crateID;
    public TreasureCrateManager TCM;
    // Use this for initialization
    void Awake()
    {
       
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

        if (currentCrate.crateID != crateID)
        {
            
             switch (currentCrate.crateType)
                {
                    case 0:
                        PayoutLabel.text = "$" + currentCrate.value.ToString();
                        break;
                    case 1:
                        currentCrate.value = 0;
                        PayoutLabel.text = currentCrate.value + " Extra Picks!";
                        break;
                }
            
        }
        
      //  }
            //if crate isn't open check for changes in the crate in case it has been opened
            if (lid.transform.localPosition != openlid && !_isLerping)
            {
                
             
            }
            if (_isLerping)
            {

                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
                lid.transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                float step = 1.5f * Time.deltaTime;
                if (percentageComplete >= 1.0f)
                {
                    _isLerping = false;
                    currentCrate.isOpen = true;
                }
            }
        }
    //
    public void clickCrate()
    {
        if (TCM.picksEnabled)
        {
            if (lid.transform.localPosition != openlid)
            {
                currentCrate.isOpen = true;
                TCM.isMaster = true;
            }
        }
    }
    public void setCrate(Crate crate)
    {
        currentCrate = crate;
        if (crate.isOpen)
        {
            openCrateLid();
        }
    }

    private void openCrateLid()
    {
       _startPosition = lid.transform.localPosition;
            _timeStartedLerping = Time.time;
            _endPosition = openlid;
            _isLerping = true;
    }
}
