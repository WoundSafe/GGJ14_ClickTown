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

    bool displayText = false;
    string textToShow = "NOT SET";
    public GUISkin skin;

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

    long CalcTotalCost()
    {
        return buyCostCopper + buyCostSilver * 100 + buyCostGold * 10000;
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
            textToShow = string.Format("<size=30>You need to work <b>{0}</b> jobs.</size>", workNeeded);
            GetComponent<SpriteRenderer>().sprite = disabled;
        }
        else if (parentPOI.parentTile.GetComponent<Renderer>().material.color.a < interestNeeded)
        {
            canUse = false;
            textToShow = string.Format("<size=30>This is not interesting enough.</size>");
            GetComponent<SpriteRenderer>().sprite = disabled;
        }
        else if (playerData.CalcTotalWealth() < CalcTotalCost())
        {
            canUse = false;
            textToShow = string.Format("<size=30>You need {0} Gold, {1} Silver, and {2} Copper.</size>", buyCostGold, buyCostSilver, buyCostCopper);
            GetComponent<SpriteRenderer>().sprite = disabled;
        }
        else if (!canUse)
        {
            canUse = true;
            GetComponent<SpriteRenderer>().sprite = buttonUp;
        }
    }

    IEnumerator TurnOffText()
    {
        yield return new WaitForSeconds(2);
        displayText = false;
    }

    public void Clicked()
    {
        if (!canUse)
        {
            displayText = true;
            StartCoroutine("TurnOffText");
            return;
        }
        GetComponent<SpriteRenderer>().sprite = buttoneDown;

        print(CalcTotalCost().ToString());
        print(playerData.CalcTotalWealth().ToString());

        if (playerData.CanPurchase(buyCostGold, buyCostSilver, buyCostCopper))
        {
            playerData.AddPassive(passiveGold, passiveSilver, passiveCopper);
            isPurchased = true;
            Color color = parentPOI.parentTile.GetComponent<Renderer>().material.color;
            color.a += 10.0f / 255.0f;
            if (color.a >= 1 && color.r == 1)
            {
                playerData.areasFullyExplored++;
                color.r = 0.999f;
            }
            parentPOI.parentTile.GetComponent<Renderer>().material.color = color;
        }
    }

    public void Release()
    {
        if (canUse) GetComponent<SpriteRenderer>().sprite = buttonUp;
        else GetComponent<SpriteRenderer>().sprite = disabled;
    }

    public void OnGUI()
    {
        if (displayText)
        {
            GUI.skin = skin;
            GUIContent content = new GUIContent(textToShow);
            Vector2 textSize = GUI.skin.GetStyle("TitleBar").CalcSize(content);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z));
            print("World Point: " + transform.position.ToString());
            print("SCreen Point: " + screenPos.ToString());
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, textSize.x, textSize.y), content, GUI.skin.GetStyle("TitleBar"));
        }

    }
}
