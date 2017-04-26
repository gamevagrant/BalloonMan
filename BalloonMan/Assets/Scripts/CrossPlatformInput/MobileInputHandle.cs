using System.Collections;
using System.Collections.Generic;
using qy.CrossPlatformInput;
using UnityEngine;


public class MobileInputHandle : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		Input.gyro.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{

		CrossPlatformInputManager.SetAxis(CrossPlatformInput.AXIS_HORIZONTAL, Input.gyro.gravity.x);

		if (Input.touchCount > 0 )
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP);
			}else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				CrossPlatformInputManager.SetButtonUp(CrossPlatformInput.JUMP);
			}
		}
	}
}

