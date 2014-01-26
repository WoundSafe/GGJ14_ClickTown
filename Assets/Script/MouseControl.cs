using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{

	bool dragMode = false;
    bool sendRelease = false;
    WorkButtonControll buttonTarget;
	Vector3 lastMouse;
	Vector3 originalCameraPosition;
	Vector3 worldTouchPoistion;
	Vector3 worldToCamera;

    float fps_counter = 0;
    float deltaTime = 0;
    int fps = 0;

    public GUISkin skin;
	// Use this for initialization
	void Start () {
		lastMouse = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () 
	{

        fps_counter++;
        deltaTime += Time.deltaTime;
        if (deltaTime >= 2)
        {
            deltaTime -= 2;
            fps = (int)(fps_counter / 2);
            fps_counter = 0;
        }

		if (Input.GetMouseButtonDown (0) && Input.touchCount != 2) 
		{
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit;

            if (rayHit = Physics2D.GetRayIntersection(mouseRay))
            {
                if (rayHit.transform.tag == "WorkButton")
                {
                    buttonTarget = rayHit.transform.GetComponent<WorkButtonControll>();
                    buttonTarget.Clicked();
                    sendRelease = true;
                }
                else if (rayHit.transform.tag == "POI")
                {
                    rayHit.transform.GetComponent<POI_Data>().ToggleShowActions();
                }
                else
                {
                    dragMode = true;
                    lastMouse = Input.mousePosition;
                    worldTouchPoistion = Camera.main.ScreenToWorldPoint(lastMouse);
                }
            }
            else
            {
                dragMode = true;
                lastMouse = Input.mousePosition;
                worldTouchPoistion = Camera.main.ScreenToWorldPoint(lastMouse);
            }

			
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			dragMode = false;
            if (sendRelease)
            {
                sendRelease = false;
                buttonTarget.Released();
            }
		}

		if (dragMode) 
		{
			//Vector3 deltaMouse = Input.mousePosition - lastMouse;
			//deltaMouse.z = 0;
			//deltaMouse.Normalize();
			//deltaMouse *= -0.1f;
			//transform.position = originalCameraPosition + deltaMouse;
			Vector3 newWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newWorld.z = 0;
            Vector3 moveIt = worldTouchPoistion - newWorld;
            moveIt = moveIt + transform.position;
            moveIt.z = -10;
            transform.position = moveIt;
		}



        if (Input.touchCount != 2)
        {
            lastMouse = Input.mousePosition;
            worldTouchPoistion = Camera.main.ScreenToWorldPoint(lastMouse);
            worldTouchPoistion.z = 0;
        }
	}

    void OnGUI()
    {
        GUI.skin = skin;
        //GUILayout.Label("Camera Postion: " + transform.position.ToString());
        //GUILayout.Label("lastMouse: " + lastMouse.ToString());
        //GUILayout.Label("worldTouchPosition: " + worldTouchPoistion.ToString());
        GUI.Label(new Rect(Screen.width / 2, 50, 100, 50), "FPS: " + fps);
    }

}
