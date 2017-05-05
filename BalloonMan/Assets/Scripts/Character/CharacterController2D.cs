using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qy.CrossPlatformInput;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField]
	private float m_MaxSpeed = 10f;
	[SerializeField]
	private float m_Acceleration = 10000f;
	[SerializeField]
	public float m_JumpForce = 600f;
	[SerializeField]
	private BalloonsController balloonsController;
	[SerializeField]
	private LayerMask m_WhatIsGround;//地面的层

	private Rigidbody2D rigidbody;
	private Animator animator;
	private SpriteRenderer sprite;
	private Transform groundCheck; //判断是否落地的监测点
	private bool isGrounded;//是否在地面上
	private const float groundedRadius = .2f;//检测落地点的半径

	private Rect displayRect = new Rect();

	void Awake()
	{
		groundCheck = transform.Find("GroundCheck");
	}


	void Start ()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
		updateDisplayRect();
		
	}

	void Update()
	{
		updateBoundary();
	}

	void FixedUpdate()
	{
		isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				blowing();
			}
		}
		animator.SetBool("Ground", isGrounded);

		// Set the vertical animation
		//animator.SetFloat("vSpeed", rigidbody.velocity.y);
		animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
		
	}

	//落在地面上让人站住播放待机动画，碰到其他角色，障碍物或者地面的非可站立面都收到反方向的弹力
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.layer!=LayerMask.NameToLayer("Character"))
		{
			if (!isGrounded)
			{
				Debug.Log(coll.relativeVelocity);
				rigidbody.velocity = Vector2.zero;
				rigidbody.AddForce(coll.relativeVelocity * 30);
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			animator.SetBool("Ground", false);
		}
	}


	void updateDisplayRect()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

		displayRect.width = a.x;
		displayRect.height = a.y;
	}


	//计算左右穿越
	void updateBoundary()
	{
		if (gameObject.transform.position.x>displayRect.width/2)
		{
			Vector3 point = gameObject.transform.position;
			point.x = -displayRect.width/2;
			gameObject.transform.position = point;
		}
		else if(gameObject.transform.position.x<-displayRect.width/2)
		{
			Vector3 point = gameObject.transform.position;
			point.x = displayRect.width / 2;
			gameObject.transform.position = point;
		}
	}

	/// <summary>
	/// 移动，move范围在-1到1之间
	/// </summary>
	/// <param name="move"></param>
	/// <param name="jump"></param>
	public void move(float move, bool jump)
	{
		if (balloonsController && !balloonsController.hasBalloon )
		{
			rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
			animator.SetFloat("Speed", 0);
			return;
		}
		if (move > 1)
		{
			move = 1;
		}
		else if (move < -1)
		{
			move = -1;
		}
		/*
		if (rigidbody.velocity.x * move < 0 )
		{
			//rigidbody.velocity = new Vector2(move * m_MaxSpeed , rigidbody.velocity.y);
		}*/
		if (Mathf.Abs(rigidbody.velocity.x) < m_MaxSpeed * Mathf.Abs(move))
		{
			float dir = move < 0 ? -1 : 1;
			rigidbody.AddForce(new Vector2( m_Acceleration,0)*Time.deltaTime * dir);
		}
		

		if (move > 0)
		{
			sprite.flipX = false;
		}else if (move < 0)
		{
			sprite.flipX = true;
		}

		if (jump)
		{
			rigidbody.AddForce(new Vector2(0, m_JumpForce));
			animator.SetBool("Ground", false);
		}
	}

	public void move(Vector3 position)
	{
		if (balloonsController && !balloonsController.hasBalloon)
		{
			rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
			animator.SetFloat("Speed", 0);
			return;
		}
		rigidbody.MovePosition(position);
	}

	/// <summary>
	/// 吹气
	/// </summary>
	public void blowing()
	{
		if (balloonsController && !balloonsController.hasBalloon)
		{
			balloonsController.blowing(1f / 2 * Time.fixedDeltaTime);
		}
	}

	public void attack()
	{
		
	}
}
