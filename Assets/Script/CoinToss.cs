using UnityEngine;
using System.Collections;

public class CoinToss : MonoBehaviour {

    float lifeTime = 0;

	// Use this for initialization
	void Start () {
        Vector2 force = new Vector2(Random.Range(-55, 55), Random.Range(155, 188));
        GetComponent<Rigidbody2D>().AddForce(force);
	}
	
	// Update is called once per frame
	void Update () {

        lifeTime += Time.deltaTime;
        Color color = GetComponent<Renderer>().material.color;
        color.a = Mathf.Lerp(1, 0, lifeTime - 0.5f);
        GetComponent<Renderer>().material.color = color;

        if (color.a == 0) GameObject.Destroy(this.gameObject);
	}
}
