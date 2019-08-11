using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartSequence : MonoBehaviour {

    [SerializeField]
    List<TweenPosition> tweenPosition = new List<TweenPosition>();

    [SerializeField]
    List<TweenRotation> tweenRotation = new List<TweenRotation>();

    [SerializeField]
    Animator footballPlayerAnimation;

	// Use this for initialization
	void Start () {
        Invoke("InitiateTweening", 2);
       
    }
	
	void InitiateTweening() {
        //start position tweening
        foreach (TweenPosition tp in tweenPosition) {
            tp.PlayForward();
        }

        //start rotation tweening
        foreach (TweenRotation tr in tweenRotation) {
            tr.PlayForward();
        }

        footballPlayerAnimation.enabled = true;
    }
}
