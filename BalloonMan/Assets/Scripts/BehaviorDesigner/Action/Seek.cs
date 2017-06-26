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

	private Rigidbody2D rigidbody;
	private Vector3 point;
	public override void OnStart()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		point = transform.position;
	}

	public override TaskStatus OnUpdate()
	{
		if (target!=null)
		{
		
			Vector3 targetPoint = target.Value.transform.position + new Vector3(0, 2);
			Vector3 dir = (targetPoint - point).normalized;
			Vector2 v = new Vector2(0, Mathf.Sin(Time.time * 2)) * 0.2f;
			point = point + dir*2*Time.deltaTime;
			rigidbody.MovePosition(point + new Vector3(v.x, v.y));

			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Failure;
		}
	}

}
