using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BalloonsController : MonoBehaviour
{
	[SerializeField]
	private int maxCount = 3;//初始气球数量
	[SerializeField]
	private GameObject BalloonPrefab;
	[SerializeField]
	private Balloon Parachute;//降落伞

	private int currentCount;//当前的气球数量

	public bool hasBalloon = true;//是否还有气球

	private List<Balloon> balloons;
	private Rigidbody2D rigidbody;
	private Vector2 upForce = new Vector2(0,1000);
	private float progress=0;//充气的进度 （0，1）

	private bool inParachute//是否打开了降落伞
	{
		get { return Parachute.gameObject.activeSelf; }
	}

	private Dictionary<int, List<Balloon.BalloonData>> balloonDatas = new Dictionary<int, List<Balloon.BalloonData>>
	{
		{1,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),0 )}},
		{2,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),22 ),new Balloon.BalloonData(new Vector2(0,0.5f),-22 )}},
		{3,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),32 ),new Balloon.BalloonData(new Vector2(0,0.5f),0 ),new Balloon.BalloonData(new Vector2(0,0.5f),-32 )}},
	};

	void Awake()
	{
		BalloonPrefab.SetActive(false);
		Parachute.gameObject.SetActive(false);
		rigidbody = GetComponentInParent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start ()
	{
		Parachute.onBalloonBroken = onParachuteBroken;

		balloons = new List<Balloon>();
		for (int i = 0; i < maxCount; i++)
		{
			Balloon.BalloonData balloonData = balloonDatas[maxCount][i];

			GameObject go = GameObject.Instantiate(BalloonPrefab);
			go.transform.parent = transform;
			go.transform.localPosition = balloonData.localPosition;
			go.transform.localScale = Vector3.one;
			go.transform.localRotation = balloonData.localRotation;
			go.SetActive(true);

			Balloon balloon = go.GetComponent<Balloon>();
			balloon.onBalloonBroken = onBalloonBroken;
			balloon.setData(balloonData);
			balloons.Add(balloon);
			
		}
		currentCount = maxCount;
	}

	
	void FixedUpdate () 
	{
		if (rigidbody)
		{
			if (currentCount > 0)
			{
				rigidbody.AddForce(upForce * Time.deltaTime);
			}
			else if (inParachute)
			{
				rigidbody.AddForce(upForce * 1.4f * Time.deltaTime);
			}
			
		}
	}

	void onBalloonBroken(Collision2D coll)
	{
		currentCount--;
		if (currentCount==0)
		{
			Invoke("onParachute",0.1f);
			hasBalloon = false;
		}
		resetBalloons();
		Debug.Log(transform.childCount);
	}

	void resetBalloons()
	{
		if (balloonDatas.ContainsKey(currentCount))
		{
			int index = 0;
			for (int i = 0; i < balloons.Count; i++)
			{
				if (!balloons[i].isBroken)
				{
					Debug.Log(i + "|" + currentCount + "|" + index);
					balloons[i].setData(balloonDatas[currentCount][index]);
					index++;
				}
			}
		}
		
	}


	void onParachute()
	{
		Parachute.gameObject.SetActive(true);
		Parachute.gameObject.transform.localScale = Vector3.zero;
		Parachute.gameObject.transform.DOScale(Vector3.one, 1);
	}

	void onParachuteBroken(Collision2D coll)
	{

	}
	/*
	public void blow(float time)
	{
		Parachute.gameObject.SetActive(false);
		//StartCoroutine(blowing(time));
	}*/

	public void blowing(float f)
	{
		if (Parachute.gameObject.activeSelf)
		{
			Parachute.gameObject.SetActive(false);
		}
		progress += f;
		float step = 1.0f/balloons.Count;
		int index = (int)(progress / step);
		float time = progress % step/step;
		if (index< balloons.Count)
		{
			currentCount = index+1;
			balloons[index].blowing(time);
			resetBalloons();
		}
		if (progress >= 1)
		{

			hasBalloon = true;
			progress = 0;
		}
		
	}

}
