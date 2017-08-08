using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Custom")]
[TaskDescription("追寻物体")]
public class Seek : Action
{

	public SharedGameObject target;

	private CharacterController2D character;
	private Rigidbody2D rigidbody;
	public override void OnStart()
	{
		character = gameObject.GetComponent<CharacterController2D>();
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		character.m_JumpForce = 200;
		
	}

	public override TaskStatus OnUpdate()
	{
		if (target!=null)
		{
			/*
			Vector3 targetPoint = target.Value.transform.position + new Vector3(0, 5);
			Vector3 v = targetPoint - transform.position;
			if (v.x > 1)
			{
				v.x = 0.2f;
			}else if (v.x < -1)
			{
				v.x = -0.2f;
			}
			bool isJump = false;
			if (v.y > 5 && rigidbody.velocity.y < 1)
			{
				isJump = true;
			}
			character.move(v.x, isJump);
			return TaskStatus.Running;
			 * */
			Vector3 targetPoint = target.Value.transform.position + new Vector3(0, 3);
			Vector3 dir = (targetPoint - transform.position).normalized;

			character.move(transform.position + dir * 2 * Time.deltaTime + new Vector3(0, Mathf.Cos(Time.time)) * 10 * Time.deltaTime);
			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Failure;
		}

		
	}
}
