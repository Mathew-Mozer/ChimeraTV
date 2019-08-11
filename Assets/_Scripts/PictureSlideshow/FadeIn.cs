using UnityEngine;
using System.Collections;

/// <summary>
/// <author>Stephen King</author>
/// <date>6/29/2016</date>
/// <version>1.0</version>
/// 
/// This allows textures to fade in/out in the picture slideshow
/// </summary>

public class FadeIn {

    /// <summary>
    /// This fades the texture out.
    /// </summary>
    /// <param name="tweenAlpha"></param>
    public void FadeTextureOut(TweenAlpha tweenAlpha) {

        tweenAlpha.PlayForward();
    }

    /// <summary>
    /// This plays the tween forward (revealing the texture)
    /// </summary>
    /// <param name="tweenAlpha"></param>
    public void FadeTextureIn(TweenAlpha tweenAlpha) {
        tweenAlpha.PlayReverse();
    }
}
