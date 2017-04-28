using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qy.CrossPlatformInput;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField]
	private float m_MaxSpeed = 10f;
	[SerializeField]
	private float m_JumpForce = 600f;
	[SerializeField]
	private Balloon[] balloons;

	private Rigidbody2D rigidbody;
	private Animator animator;
	private SpriteRenderer sprite;

	private Rect displayRect = new Rect();
	// Use this for initialization
	void Start ()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
		updateDisplayRect();
		CircleCollider2D c;
		
	}

	void Update()
	{
		updateBoundary();
	}


	//落在地面上让人站住播放待机动画，碰到其他角色，障碍物或者地面的非可站立面都收到反方向的弹力
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			animator.SetBool("Ground", true);
			rigidbody.velocity = Vector2.zero;
		}
		else
		{
			Vector2 direction = Vector2.zero;

			foreach (ContactPoint2D contact in coll.contacts)
			{
				//direction += new Vector2(transform.position.x,transform.position.y) - contact.point;
				//Debug.Log(contact.relativeVelocity+"|"+contact.normal);
				direction += getReflex(-contact.relativeVelocity, contact.normal) * Vector3.Distance(Vector3.zero, contact.relativeVelocity);
			}
			//Debug.Log(coll.contacts.Length);
			rigidbody.AddForce(direction * 20);
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

	

	Vector2 getReflex(Vector2 I ,Vector2 N)
	{
		I = I.normalized;
		N = N.normalized;
		Vector2 R = I - 2 * Vector3.Dot(I , N) * N;
		return R;
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
		if (move > 1)
		{
			move = 1;
		}
		else if (move < -1)
		{
			move = -1;
		}
		rigidbody.velocity = new Vector2(move * m_MaxSpeed, rigidbody.velocity.y);
		//rigidbody.AddForce(new Vector2(move * 20,0));
		animator.SetFloat("Speed", Mathf.Abs(move));

		//Vector3 scale = transform.localScale;
		//scale.x = Mathf.Abs(scale.x) * (move > 0 ? 1 : -1);
		//transform.localScale = scale;
		sprite.flipX = move > 0 ? false : true;

		if (jump)
		{
			rigidbody.AddForce(new Vector2(0, m_JumpForce));
			animator.SetBool("Ground", false);
		}
	}

	/// <summary>
	/// 吹气
	/// </summary>
	public void blowing()
	{
		
	}
}
