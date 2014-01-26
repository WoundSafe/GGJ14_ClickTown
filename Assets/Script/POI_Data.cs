using UnityEngine;
using System.Collections;

public class POI_Data : MonoBehaviour {

    public SpriteRenderer parentTile;
    int interest = 0;
    int kindness = 0;

	// Use this for initialization
	void Start () {

        Vector2 position2D;
        position2D.x = transform.position.x;
        position2D.y = transform.position.y;
        Collider2D[] others = Physics2D.OverlapPointAll(position2D);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].tag == "Tile")
            {
                parentTile = others[i].GetComponent<SpriteRenderer>();
            }
        }
	
	}

    // Update is called once per frame
    void Update()
    {
	
	}

    public void WorkDone()
    {
        interest++;

        Color color = parentTile.renderer.material.color;
        color.a = interest / 255.0f;
        parentTile.renderer.material.color = color;
    }

    public void DisplayActions()
    {

    }
}
