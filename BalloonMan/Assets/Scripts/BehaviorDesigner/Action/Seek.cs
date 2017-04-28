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
	}

	public override TaskStatus OnUpdate()
	{
		if (target!=null)
		{
			Vector3 v = target.Value.transform.position - transform.position;
			if (v.x > 1)
			{
				v.x = 1;
			}else if (v.x < -1)
			{
				v.x = -1;
			}
			bool isJump = false;
			if (v.y>3 && rigidbody.velocity.y < 2)
			{
				isJump = true;
			}
			character.move(v.x, isJump);
			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Failure;
		}

		
	}
}
