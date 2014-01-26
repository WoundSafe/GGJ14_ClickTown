using UnityEngine;
using System.Collections;

public class WorkButtonControll : MonoBehaviour {

    public Sprite buttonUp;
    public Sprite buttoneDown;
    public GameObject copperCoin;
    public POI_Data parentPOI;

	// Use this for initialization
	void Start () {
        parentPOI = transform.parent.GetComponent<POI_Data>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Clicked()
    {
        GetComponent<SpriteRenderer>().sprite = buttoneDown;
        GameObject coin = GameObject.Instantiate(copperCoin) as GameObject;
        coin.transform.position = transform.position;
        parentPOI.WorkDone();
    }

    public void Released()
    {
        GetComponent<SpriteRenderer>().sprite = buttonUp;
    }
}
