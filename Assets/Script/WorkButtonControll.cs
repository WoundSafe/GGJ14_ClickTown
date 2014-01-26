using UnityEngine;
using System.Collections;

public class WorkButtonControll : MonoBehaviour {

    public Sprite buttonUp;
    public Sprite buttoneDown;
    public Sprite disabled;

    public GameObject coinObject;
    public POI_Data parentPOI;
    PlayerData playerData;

    bool canUse = true;

    public int workNeeded = 0;
    public float interestNeeded = 0;

    public int workCopper = 0;
    public int workSilver = 0;
    public int workGold = 0;

	// Use this for initialization
	void Start () {
        parentPOI = transform.parent.GetComponent<POI_Data>();
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
	}

    void OnEnable()
    {
        CheckUse();
    }
	
	// Update is called once per frame
	void Update () {
        CheckUse();
	}

    void CheckUse()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        parentPOI = transform.parent.GetComponent<POI_Data>();

        if (parentPOI.parentTile == null) return;

        if (playerData.workDone < workNeeded)
        {
            canUse = false;
            GetComponent<SpriteRenderer>().sprite = disabled;
        }
        else if (parentPOI.parentTile.renderer.material.color.a < interestNeeded)
        {
            canUse = false;
            GetComponent<SpriteRenderer>().sprite = disabled;
        }
        else
        {
            canUse = true;
            GetComponent<SpriteRenderer>().sprite = buttonUp;
        }
    }

    public void Clicked()
    {
        if (!canUse) return;

        GetComponent<SpriteRenderer>().sprite = buttoneDown;
        GameObject coin = GameObject.Instantiate(coinObject) as GameObject;
        coin.transform.position = transform.position;
        parentPOI.WorkDone();
        playerData.AddMoney(workGold, workSilver, workCopper);
        playerData.Work();
    }

    public void Released()
    {
        if(canUse) GetComponent<SpriteRenderer>().sprite = buttonUp;
        else GetComponent<SpriteRenderer>().sprite = disabled;
    }
}
