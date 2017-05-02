using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsController : MonoBehaviour
{
	public int maxCount = 3;//初始气球数量
	public GameObject BalloonPrefab;

	public readonly int currentCount;//当前的气球数量

	private List<Balloon> balloons;

	private Dictionary<int, List<Balloon.BalloonData>> balloonDatas = new Dictionary<int, List<Balloon.BalloonData>>
	{
		{1,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),0 )}},
		{2,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),32 ),new Balloon.BalloonData(new Vector2(0,0.5f),-32 )}},
		{3,new List<Balloon.BalloonData>{new Balloon.BalloonData(new Vector2(0,0.5f),32 ),new Balloon.BalloonData(new Vector2(0,0.5f),0 ),new Balloon.BalloonData(new Vector2(0,0.5f),-32 )}},
	};

	void Awake()
	{
		BalloonPrefab.SetActive(false);
	}
	// Use this for initialization
	void Start () 
	{
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

			Balloon balloon = go.GetComponentInChildren<Balloon>();
			balloon.onTriggerEnter = onTriggerEnter;
			balloon.setData(balloonData);
			balloons.Add(balloon);
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround();
	}

	void onTriggerEnter(Collider2D other)
	{
		
	}
}
