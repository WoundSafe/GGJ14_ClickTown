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
    public int conJobs = 0;




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
        else if (Input.GetKeyDown(KeyCode.I))
        {
            displayStats = !displayStats;
        }

        if (displayInstruction && Time.time > 4) displayInstruction = false;

        

        displayCopper = (int)Mathf.Floor(copper);
        displaySilver = (int)Mathf.Floor(silver);
        displayGold = (int)Mathf.Floor(gold);
	}

    

    void OnGUI()
    {
        GUI.skin = skin;



        if (displayStats)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Player Stats");
            GUILayout.Label("Money");
            GUILayout.Label(System.String.Format("Gold: {0}   Silver: {1}   Copper: {2}",
                displayGold, displaySilver, displayCopper), GUILayout.Width(Screen.width));
            GUILayout.Label("Passive Income");
            GUILayout.Label(System.String.Format("Gold: {0}   Silver: {1}   Copper: {2}",
                passiveGold, passiveSilver, passiveCopper), GUILayout.Width(Screen.width));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Jobs Worked: " + workDone);
            GUILayout.Label("Cons Completed: " + conJobs);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100, 50), "Quit"))
            {
                Application.Quit();
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(System.String.Format("Gold: {0}   Silver: {1}   Copper: {2}",
                displayGold, displaySilver, displayCopper), GUILayout.Width(Screen.width));
            GUILayout.EndHorizontal();
        }

        if (displayInstruction)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                GUILayout.Label("Press with two fingers to toggle stat display");
            }
            else
            {
                GUILayout.Label("Press I to toggle player stats");
            }
        }
    }
}
