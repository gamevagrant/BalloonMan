using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Custom")]
[TaskDescription("判断tag = findTag 的物体是否在警戒距离内")]
public class CanSeeObject : Conditional
{
	[BehaviorDesigner.Runtime.Tasks.Tooltip("寻找物体的tag")]
	public string findTag;
	[BehaviorDesigner.Runtime.Tasks.Tooltip("警戒距离")]
	public float distance;
	[BehaviorDesigner.Runtime.Tasks.Tooltip("在警戒距离内的物体")]
	public SharedGameObject findObject;

	private GameObject[] targets;

	public override void OnStart()
	{
		targets = GameObject.FindGameObjectsWithTag(findTag);
	}

	public override TaskStatus OnUpdate()
	{
		if (targets != null && targets.Length > 0)
		{
			foreach (GameObject target in targets)
			{
				//Debug.Log((target.transform.position - transform.position).magnitude);
				if ((target.transform.position - transform.position).magnitude < distance)
				{
					findObject.Value = target;
					return TaskStatus.Success;
				}
			}
		}
		return TaskStatus.Failure;
	}
}
