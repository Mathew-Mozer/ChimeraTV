using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <author>Stephen King</author>
/// <date>07/16/2016</date>
/// <version>1.0</version>
/// 
/// This triggers the camera to move, carrying the football with it, and the elevation/rotation of the football to simulate a kick.
/// </summary>

public class TriggerFootball : MonoBehaviour {

    [SerializeField]
    TweenPosition footballPosition;
    [SerializeField]
    TweenPosition footballElevation;
    [SerializeField]
    TweenRotation footballRotation;

    /// <summary>
    /// Triggers kick effect
    /// </summary>
    /// <param name="other"></param>

    void OnCollisionEnter(Collision other) {

        //Debug.Log("I kicked the football!");

        footballPosition.PlayForward();
        footballElevation.PlayForward();
        footballRotation.PlayForward();
    }
}
