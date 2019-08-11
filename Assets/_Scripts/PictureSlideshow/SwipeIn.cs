using UnityEngine;
using System.Collections;

/// <summary>
/// <author>Stephen King</author>
/// <date>6/29/2016</date>
/// <version>1.0</version>
/// 
/// This controls textures swiping in and out.
/// </summary>

public class SwipeIn {

    /// <summary>
    /// This controls swiping new content in from a given direction
    /// </summary>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    public void SwipeTexture(int tweenFromX, int tweenFromY, int tweenToX, int tweenToY, TweenPosition texture) {

       texture.from = new Vector3(tweenFromX, tweenFromY, 0);
        texture.to = new Vector3(tweenToX, tweenToY, 0);
      
        texture.PlayForward();

    }


}
