using UnityEngine;
using System.Collections;

public class PrizeListItem : MonoBehaviour
{
    public UILabel prizeLabel;

    public void setPrizeLabel(string pl)
    {
        prizeLabel.text = pl;
    }
}
