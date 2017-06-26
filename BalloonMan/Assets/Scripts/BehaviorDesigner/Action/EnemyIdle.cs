using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("敌人待机")]
public class EnemyIdle : Action
{
	private Rigidbody2D rigidbody;
	private Vector2 position;
	public override void OnStart()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		Debug.Log("ssssssss");
		position = new Vector2(transform.position.x, transform.position.y);
	}

	public override TaskStatus OnUpdate()
	{
		Vector2 v = new Vector2(0, Mathf.Sin(Time.time * 2)) * 0.2f;

		rigidbody.MovePosition(position + v);
		return TaskStatus.Running;
	}
}
