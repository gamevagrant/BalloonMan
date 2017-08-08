using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	public Transform[] backGround;//背景结合 离摄像机越远，排的越远，第一个为前景

	private float smoothing = 0.1f;
	private float moveScale = 0.1f;
	private GameObject player;
	private Vector3 previousPlayerPos;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag("Player");
		previousPlayerPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 offset = (player.transform.position - previousPlayerPos) * moveScale;
		offset.y = 0;
		offset.z = 0;
		if (Mathf.Abs(offset.x) > 3)
		{
			previousPlayerPos = player.transform.position;
			return;
		}
		for (int i = 0; i < backGround.Length; i++)
		{
			Transform target = backGround[i];
			Vector3 targetPos;
			if (i == 0)
			{
				targetPos = target.position + offset * (backGround.Length - i);
			}
			else
			{
				targetPos = target.position - offset * (backGround.Length - i);
			}

			target.position = Vector3.Lerp(target.position, targetPos, smoothing);

		}
		

		previousPlayerPos = player.transform.position;
	}
}
