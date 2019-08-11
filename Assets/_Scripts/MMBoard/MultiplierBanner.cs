using UnityEngine;
using System.Collections;


/// <summary>
/// This controls the bonus multiplier banner
/// <Author>Stephen King</Author>
/// <Date>10/15/2016</Date>
/// <Version>10.15.2016.1</Version>
/// </summary>
public class MultiplierBanner : MonoBehaviour {

    [SerializeField]
    GameObject beam;
    [SerializeField]
    GameObject sparkles;
    [SerializeField]
    GameObject banner;
    [SerializeField]
    TweenAlpha tweenBackgroundAlpha;
    [SerializeField]
    UILabel pointMultiplierLabel;
    [SerializeField]
    mmLargeCard displayCard;


    /// <summary>
    /// Activate the bonus multiplier banner
    /// </summary>
    public void ActivateBanner(mmCard currentCard) {
        displayCard.setCard(currentCard,"Activate Banner");
        pointMultiplierLabel.text = currentCard.Value;
        sparkles.SetActive(false);
        tweenBackgroundAlpha.PlayForward();
        beam.SetActive(true);
        banner.SetActive(true);
    }

    /// <summary>
    /// Disable the bonus multiplier banner
    /// </summary>
    public void DisableBanner() {
        beam.SetActive(false);
        banner.SetActive(false);
        tweenBackgroundAlpha.PlayReverse();
        sparkles.SetActive(true);
    }

}
