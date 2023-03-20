using UnityEngine;
using System.Collections;

public class FadeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Color test = GetComponent<Renderer>().material.color;
            test.a -= 0.1f;
            GetComponent<Renderer>().material.color = test;
        }
	
	}
}
