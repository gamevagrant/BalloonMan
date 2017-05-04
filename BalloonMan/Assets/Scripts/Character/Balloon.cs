using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	public delegate void OnBalloonBroken(Collision2D other);
	public OnBalloonBroken onBalloonBroken;
	public bool isBroken = false;

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
		if (data!=null && data.localRotation.eulerAngles.z != transform.localRotation.eulerAngles.z)
		{

			transform.localRotation = Quaternion.Lerp(transform.localRotation, data.localRotation, 5f * Time.deltaTime);

			transform.localPosition = Vector3.Lerp(transform.localPosition,data.localPosition, 0.5f);
		}
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (onBalloonBroken != null && coll.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			broken();
			onBalloonBroken(coll);
		}

	}

	void broken()
	{
		isBroken = true;
		transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
		{
			gameObject.SetActive(false);
		});
	}

	public void blow(float time = 0.5f)
	{
		isBroken = false;
		gameObject.SetActive(true);
		transform.DOScale(new Vector3(1, 1, 1), time);
	}

	public void blowing(float f)
	{
		isBroken = false;
		gameObject.SetActive(true);
		transform.localScale = Vector3.one*f;

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
