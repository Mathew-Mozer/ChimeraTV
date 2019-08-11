using UnityEngine;
using System.Collections;

public class KickDelay : MonoBehaviour {

    [SerializeField]
    Animator footballAnimation;

    [SerializeField]
    float animationStartDelay;

	// Use this for initialization
	void Awake () {

        //Delay the start of the football animation by a set number
        Invoke("KickFootball", animationStartDelay);
	}

    /// <summary>
    /// Enables the football animation
    /// </summary>
    void KickFootball() {
        footballAnimation.enabled = true;
    }

}
