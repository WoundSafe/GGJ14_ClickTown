using UnityEngine;
using System.Collections;

public class BuyButtonControll : MonoBehaviour, IButton
{

    public Sprite buttonUp;
    public Sprite buttoneDown;
    public Sprite disabled;

    public POI_Data parentPOI;
    PlayerData playerData;

    bool canUse = false;

    public int workNeeded = 0;
    public float interestNeeded = 0;

    public int buyCostGold = 0;
    public int buyCostSilver = 0;
    public int buyCostCopper = 0;

    public float passiveGold = 0;
    public float passiveSilver = 0;
    public float passiveCopper = 0;

    bool isPurchased = false;



    // Use this for initialization
    void Start()
    {
        parentPOI = transform.parent.GetComponent<POI_Data>();
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    void OnEnable()
    {
        CheckUse();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUse();
    }

    void CheckUse()
    {
        if (isPurchased)
        {
            gameObject.SetActive(false);
            return;
        }

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        parentPOI = transform.parent.GetComponent<POI_Data>();

        if (parentPOI.parentTile == null)
        {
            canUse = false;
            Debug.LogWarning("CheckUse failed on parentTile check (expected once per button)");
            return;
        }
        else if (parentPOI == null)
        {
            canUse = false;
            Debug.LogWarning("CheckUse failed on parentPOI check");
            return;
        }
        else if (playerData == null)
        {
            canUse = false;
            Debug.LogWarning("Checkuse failed on playerData check");
            return;
        }

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
        else if (!canUse)
        {
            canUse = true;
            GetComponent<SpriteRenderer>().sprite = buttonUp;
        }
    }

    public void Clicked()
    {
        if (!canUse) return;
        GetComponent<SpriteRenderer>().sprite = buttoneDown;

        if (playerData.CanPurchase(buyCostGold, buyCostSilver, buyCostCopper))
        {
            playerData.AddPassive(passiveGold, passiveSilver, passiveCopper);
            isPurchased = true;
            Color color = parentPOI.parentTile.renderer.material.color;
            color.a += 10.0f / 255.0f;
            parentPOI.parentTile.renderer.material.color = color;
        }
    }

    public void Release()
    {
        if (canUse) GetComponent<SpriteRenderer>().sprite = buttonUp;
        else GetComponent<SpriteRenderer>().sprite = disabled;
    }
}
