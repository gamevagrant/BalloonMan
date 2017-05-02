using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	public delegate void OnTriggerEnter(Collider2D other);
	public OnTriggerEnter onTriggerEnter;

	private BalloonData data;
	private CircleCollider2D collider;

	// Use this for initialization
	void Awake ()
	{
		collider = GetComponent<CircleCollider2D>();
	}

	void Start()
	{

	}
	void Update()
	{
		if (data!=null && data.localRotation.eulerAngles.z != transform.parent.localRotation.eulerAngles.z)
		{

			transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, data.localRotation, 5f * Time.deltaTime);

			transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition,data.localPosition, 0.5f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (onTriggerEnter != null)
		{
			onTriggerEnter(other);
		}
		//Debug.Log(other.name);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == "Player" && onTriggerEnter != null)
		{
			onTriggerEnter(coll.collider);
			//broken();
		}
		Vector3 force = (new Vector2(transform.position.x, transform.position.y) - coll.contacts[0].point).normalized*2*coll.relativeVelocity.sqrMagnitude;
		collider.attachedRigidbody.AddForce(force);

		broken();
		Debug.Log(coll.collider.name);
	}

	void broken()
	{
		transform.parent.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
		{
			gameObject.SetActive(false);
		});
	}

	public void blow(float time = 0.5f)
	{
		transform.parent.DOScale(new Vector3(1, 1, 1), time);
	}

	public void setData(BalloonData data)
	{
		this.data = data;
	}

	public class BalloonData
	{
		public Vector3 localPosition;
		public Quaternion localRotation;

		public BalloonData(Vector2 position, int rotate)
		{
			this.localPosition = position;
			Quaternion q = Quaternion.identity;
			q.eulerAngles = new Vector3(0,0,rotate);
			this.localRotation = q;
		}
	}
}
