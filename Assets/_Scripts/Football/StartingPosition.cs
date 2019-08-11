using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartingPosition : MonoBehaviour {

    static int I_LOW_THRESHOLD = 3;
    static int I_MEDIUM_THRESHOLD = 6;
    static int I_LARGE_THRESHOLD = 7;

    KickForCash di;

    [SerializeField]
    Camera mainCamera;

    int i_position;

    bool isHit;

    [SerializeField]
    UILabel kickerName;

    [SerializeField]
    UILabel ballTarget;

    [SerializeField]
    UILabel currentBall;

    [SerializeField]
    UILabel jackpotValue;
    [SerializeField]
    UILabel pickemTeam1Value;
    [SerializeField]
    UILabel pickemTeam2Value;
    [SerializeField]
    UILabel pickematsignValue;
    [SerializeField]
    UILabel pickemLabelValue;

    [SerializeField]
    GameObject football;

    [SerializeField]
    GameObject footballPlayer;

    float footballPosition;
    float playerPosition;

    TweenPosition footballArc;
    TweenPosition footballVelocity;

    Vector3 playerPostion1 = new Vector3(8.79f, -0.7f, 98f);
    Vector3 playerPosition2 = new Vector3(8.79f, -0.7f, 128f);
    Vector3 playerPosition3 = new Vector3(8.79f, -0.7f, 183f);
    /// <summary>
    /// On scene load
    /// </summary>
    void Awake() {

        //Make reference to display manager

        
                di = DisplayManager.displayManager.currentScene.kickForCashData;
        ChangeKickerStats();

        if (di.selectedBall == di.winningBall) {
            isHit = true;
        } else {
            isHit = false;
        }

        footballArc = football.GetComponent<TweenPosition>();
        footballVelocity = mainCamera.GetComponent<TweenPosition>();
        //Debug.Log(footballVelocity);

        SetKickPosition(di.fieldPosition);  

    }

    /// <summary>
    /// Set the kick position according to the number of recorded misses.
    /// </summary>
    /// <param name="i_kickPosition"></param>
    void SetKickPosition(int i_kickPosition) {
        if(i_kickPosition < I_LOW_THRESHOLD) {
            KickOff(0, isHit);
        }else if(i_kickPosition > I_LOW_THRESHOLD && i_kickPosition < I_LARGE_THRESHOLD) {
            KickOff(1, isHit);
        } else {
            KickOff(2, isHit);
        }
    }

    /// <summary>
    /// This sets the position of the football, the player, and tweening sets for each kick location
    /// </summary>
    /// <param name="i_position"></param>
    public void KickOff(int i_position, bool b_isHit) {

        //Debug.Log("Stuff: " + isHit);

        //If the player draws the correct number
        if (b_isHit) {
            switch (i_position) {
                case 0:
                    //footballArc.duration = 3;
                    footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 107f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 107f);
                    footballPlayer.transform.position = playerPostion1;
                    //    football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 107f);
                    break;

                case 1:
                    //footballArc.duration = 3.5f;
                    footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 137f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 137f);
                    footballPlayer.transform.position = playerPosition2;
                    // football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 137f);
                    break;
                case 2:
                    // footballArc.duration = 4f;
                    footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 191.7f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 191.7f);
                    footballPlayer.transform.position = playerPosition3;
                    //football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 192f);
                    break;
            }

            //if the player fails
        } else {

            switch (i_position) {
                case 0:
                    //footballArc.duration = 3;
                 //   footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 107f);
                    footballVelocity.to = new Vector3(-17f, 15.5f, -129f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 107f);
                    footballPlayer.transform.position = playerPostion1;
                    //    football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 107f);
                    break;

                case 1:
                    //footballArc.duration = 3.5f;
                 //   footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 137f);
                    footballVelocity.to = new Vector3(-17f, 15.5f, -129f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 137f);
                    footballPlayer.transform.position = playerPosition2;
                    // football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 137f);
                    break;
                case 2:
                    // footballArc.duration = 4f;
                   // footballVelocity.duration = 7f;
                    footballVelocity.from = new Vector3(4.7f, 2.1f, 191.7f);
                    footballVelocity.to = new Vector3(-17f, 15.5f, -129f);

                    //preset locations
                    footballVelocity.transform.position = new Vector3(4.7f, 2.1f, 191.7f);
                    footballPlayer.transform.position = playerPosition3;
                    //football.transform.position = new Vector3(5.094f, -2.254f, 21.468f);
                    Camera.main.transform.position = new Vector3(4.7f, 2.1f, 192f);
                    break;
            }
        }
    }

    /// <summary>
    /// Sets the UI to customer's values
    /// </summary>
    void ChangeKickerStats() {
       
        kickerName.text = di.playerName;
        ballTarget.text = di.winningBall.ToString();
        currentBall.text = di.selectedBall.ToString();
        jackpotValue.text = di.jackpotAmount.ToString();
        //Pick em Values
        pickematsignValue.text = di.peatsign;
        pickemLabelValue.text = di.peLabel;
        pickemTeam1Value.text = di.peTeam1;
        pickemTeam2Value.text = di.peTeam2;
    }
}
