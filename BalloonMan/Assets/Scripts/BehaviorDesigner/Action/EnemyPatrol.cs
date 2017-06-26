using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
[TaskDescription("向一个方向巡逻")]
public class EnemyPatrol : Action {
	
	[BehaviorDesigner.Runtime.Tasks.Tooltip("巡逻的方向")]
	public Vector2 moveDir = new Vector2(-1,0);

	private Rigidbody2D rigidbody;
	private Vector2 point;
	public override void OnStart()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		point = position;
	}

	public override TaskStatus OnUpdate()
	{
		Vector2 v = new Vector2(0, Mathf.Sin(Time.time * 2)) * 0.5f;
		point = point + moveDir * 2 * Time.deltaTime;
		rigidbody.MovePosition(point + v);
		return TaskStatus.Running;
	}

	private Vector2 position
	{
		get
		{
			return new Vector2(transform.position.x,transform.position.y);
		}
		
	}
}
