using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

    public bool displayStats = false;
    public bool displayInstruction = true;
    public float twoTouchStart = float.MaxValue;

    public int displayCopper;
    public int displaySilver;
    public int displayGold;

    public float copper = 0;
    public float silver = 0;
    public float gold = 0;
    public float passiveCopper = 0;
    public float passiveSilver = 0;
    public float passiveGold = 0;

    public int workDone = 0;
    public int placesOwned = 0;
    public int areasFullyExplored = 0;

    enum PayMode
    {
        Copper,
        Silver,
        Gold
    }


    public GUISkin skin;
	// Use this for initialization
	void Start () {
	
	}

    public void AddMoney(int gold, int silver, int copper)
    {
        this.gold += gold;
        this.silver += silver;
        this.copper += copper;
    }


    public void Work()
    {
        workDone++;
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began) displayStats = !displayStats;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            displayStats = !displayStats;
        }

        if (displayInstruction && Time.time > 4) displayInstruction = false;       

        //update passive
        copper += passiveCopper * Time.deltaTime;
        silver += passiveSilver * Time.deltaTime;
        gold += passiveGold * Time.deltaTime;

        displayCopper = (int)Mathf.Floor(copper);
        displaySilver = (int)Mathf.Floor(silver);
        displayGold = (int)Mathf.Floor(gold);
	}

    public bool CanPurchase(int goldCost, int silverCost, int copperCost)
    {
        bool result = false;

        long totalCost = copperCost + silverCost * 100 + goldCost * 10000;
        long totalWealth = displayCopper + displaySilver * 100 + displayGold * 10000;

        if (totalWealth >= totalCost)
        {
            result = true;

            totalWealth -= totalCost;

            gold -= displayGold;
            silver -= displaySilver;
            copper -= displayCopper;

            gold += totalWealth / 10000;
            totalWealth = totalWealth % 10000;
            silver += totalWealth / 100;
            totalWealth = totalWealth % 100;
            copper += totalWealth;

            placesOwned += 1;
        }

        return result;
    }

    public void AddPassive(float passiveGold, float passiveSilver, float passiveCopper)
    {
        this.passiveGold += passiveGold;
        this.passiveSilver += passiveSilver;
        this.passiveCopper += passiveCopper;
    }

    public long CalcTotalWealth()
    {
        return displayCopper + displaySilver * 100 + displayGold * 10000;
    }
    

    void OnGUI()
    {
        GUI.skin = skin;



        if (displayStats)
        {
            GUILayout.BeginVertical(skin.GetStyle("TitleBar"));
            GUILayout.Label("<size=50>Player Stats</size>");
            GUILayout.Label("<size=24>Money</size>");
            GUILayout.Label(System.String.Format("<size=24>Gold: {0}   Silver: {1}   Copper: {2}</size>",
                displayGold, displaySilver, displayCopper), GUILayout.Width(Screen.width));
            GUILayout.Label("<size=24>Passive Income</size>");
            GUILayout.Label(System.String.Format("<size=24>Gold: {0}   Silver: {1}   Copper: {2}</size>",
                passiveGold, passiveSilver, passiveCopper), GUILayout.Width(Screen.width));

            GUILayout.BeginHorizontal();
            GUILayout.Label("<size=24>Jobs Worked: " + workDone + "</size>");
            GUILayout.Label("<size=24>Places Owned: " + placesOwned + "</size>");
            GUILayout.Label("<size=24>Areas Fully Explored: " + areasFullyExplored + "</size>");
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            //do not show if on the web
            if (true)
            {
                if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100, 50), "<size=50>Quit</size>"))
                {
                    Application.Quit();
                }
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(System.String.Format("<size=40>Gold: {0}   Silver: {1}   Copper: {2}</size>",
                displayGold, displaySilver, displayCopper), skin.GetStyle("TitleBar"), GUILayout.Width(Screen.width));
            GUILayout.EndHorizontal();
        }

        if (displayInstruction)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                GUILayout.Label("<size=50>Two-Finger tap to toggle stat display</size>");
            }
            else
            {
                GUILayout.Label("<size=50>Press S to toggle player stats</size>");
            }
        }
    }
}
