using System.Collections;
using System.Collections.Generic;
using qy.CrossPlatformInput;
using UnityEngine;

public class StandaloneInputHandle : MonoBehaviour
{
	private Vector2 axis = new Vector2();
	private Vector2 halfScreen;
	private Dictionary<KeyCode, string> keyMapping = new Dictionary<KeyCode, string>
	{
		{KeyCode.Space,CrossPlatformInput.JUMP},
	};

	void Start()
	{
		halfScreen = new Vector2(Screen.width/2,Screen.height/2);
	}
	// Update is called once per frame
	void Update () 
	{
		foreach (KeyCode key in keyMapping.Keys)
		{
			string button = keyMapping[key];
			if (Input.GetKeyDown(key))
			{
				CrossPlatformInputManager.SetButtonDown(button);
			}
			else if (Input.GetKeyUp(key))
			{
				CrossPlatformInputManager.SetButtonUp(button);
			}
		}

		if (Input.mousePosition.x<0 || Input.mousePosition.x>Screen.width||Input.mousePosition.y<0||Input.mousePosition.y>Screen.height)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				axis = Vector2.Lerp(axis, new Vector2(-1,0), 0.2f);
			}else if (Input.GetKey(KeyCode.RightArrow))
			{
				axis = Vector2.Lerp(axis, new Vector2(1, 0), 0.2f);
			}
			else
			{
				axis = Vector2.Lerp(axis, Vector2.zero, 0.2f);
			}
		}
		else
		{
			axis.x = (Input.mousePosition.x - halfScreen.x) / halfScreen.x;
			axis.y = (Input.mousePosition.y - halfScreen.y) / halfScreen.y;
			
			if (Input.GetMouseButtonDown(0))
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				CrossPlatformInputManager.SetButtonUp(CrossPlatformInput.JUMP);
			}
		}
		CrossPlatformInputManager.SetAxis(CrossPlatformInput.AXIS_HORIZONTAL, axis.x);


		
		
	}
}
