using UnityEngine;
using System.Collections;

public class WorkButtonControll : MonoBehaviour {

    public Sprite buttonUp;
    public Sprite buttoneDown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Clicked()
    {
        GetComponent<SpriteRenderer>().sprite = buttoneDown;
    }

    public void Released()
    {
        GetComponent<SpriteRenderer>().sprite = buttonUp;
    }
}
