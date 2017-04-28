using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	public delegate void OnTriggerEnter(Collider2D other);

	public OnTriggerEnter onTriggerEnter;
	// Use this for initialization
	void Awake ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (onTriggerEnter != null)
		{
			onTriggerEnter(other);
		}
	}
}
