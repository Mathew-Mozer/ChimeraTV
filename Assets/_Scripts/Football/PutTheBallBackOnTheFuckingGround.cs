using UnityEngine;
using System.Collections;

/// <summary>
/// Because NGUI treats start delay as always delay, this script removes the stall on reverse play.
/// </summary>

public class PutTheBallBackOnTheFuckingGround : MonoBehaviour {


    /// <summary>
    /// Do it
    /// </summary>
    public void ReverseBall() {
        gameObject.GetComponent<TweenPosition>().delay = 0f;
        gameObject.GetComponent<TweenPosition>().PlayReverse();
    }
}
