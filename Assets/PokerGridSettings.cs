using UnityEngine;
using System.Collections;

public class PokerGridSettings : MonoBehaviour {
    public int GridRow;
    public int GridColumn;
    public int RowOffset;
    public int ColumnOffset;
    public Vector3 tmpLocation;
    // Use this for initialization
    void Start () {
        tmpLocation = new Vector3(GridRow * 300, GridColumn * 90, gameObject.transform.localPosition.z);

    }
	
	// Update is called once per frame
	void Update () {
        tmpLocation = new Vector3((GridColumn * 300)+RowOffset,(GridRow * -90)+ColumnOffset, gameObject.transform.localPosition.z);

        gameObject.transform.localPosition = tmpLocation;
    }
}
