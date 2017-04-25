using System.Collections;
using System.Collections.Generic;
using qy.CrossPlatformInput;
using UnityEngine;


public class MobileInputHandle : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.position.x < Screen.width / 2)
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_LEFT);
			}
			else
			{
				CrossPlatformInputManager.SetButtonDown(CrossPlatformInput.JUMP_RIGHT);
			}
			Debug.Log(touch.position);
		}
	}
}

