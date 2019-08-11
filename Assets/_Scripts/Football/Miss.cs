using UnityEngine;
using System.Collections;

/// <summary>
/// <author>Stephen King</author>
/// <date>8/5/2016</date>
/// <version>1.0</version>
/// 
/// This class is for all hit boxes that will reset the scene when the football misses the field goal
/// </summary>

public class Miss : MonoBehaviour {

    [SerializeField]
    TriggerEnd triggerEnd;
    
    /// <summary>
    /// Call the method that resets the scene.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other) {

        triggerEnd.ResetScene();
        }
    }
