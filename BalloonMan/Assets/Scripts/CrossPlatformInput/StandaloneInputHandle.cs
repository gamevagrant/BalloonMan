using System.Collections;
using System.Collections.Generic;
using qy.CrossPlatformInput;
using UnityEngine;

public class StandaloneInputHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0) )
		{
			if (Input.mousePosition.x<Screen.width/2)
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_LEFT);
			}
			else
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_RIGHT);
			}
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_LEFT);
		}else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_RIGHT);
		}
		
	}
}
