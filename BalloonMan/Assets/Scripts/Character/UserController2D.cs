using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qy.CrossPlatformInput;

[RequireComponent(typeof(CharacterController2D))]
public class UserController2D : MonoBehaviour
{

	private CharacterController2D character;
	// Use this for initialization
	void Awake ()
	{
		character = gameObject.GetComponent<CharacterController2D>();
	}
	
	void Update()
	{
		float h = CrossPlatformInputManager.GetAxis(CrossPlatformInput.AXIS_HORIZONTAL);
		bool j = CrossPlatformInputManager.GetButtonDown(CrossPlatformInput.JUMP);
		if (Mathf.Abs(h) < 0.1f)
		{
			h = 0;
		}

		character.move(h,j);
	}
}
