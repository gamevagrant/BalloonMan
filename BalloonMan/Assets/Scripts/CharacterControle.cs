using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qy.CrossPlatformInput;

public class CharacterControle : MonoBehaviour
{

	private Rigidbody2D rigidbody;
	private Animator animator;

	private Rect displayRect = new Rect();
	// Use this for initialization
	void Start ()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();

		updateDisplayRect();
	}
	
	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown(CrossPlatformInput.JUMP_LEFT))
		{
			jump(new Vector2(-100,600));
		}else if (CrossPlatformInputManager.GetButtonDown(CrossPlatformInput.JUMP_RIGHT))
		{
			jump(new Vector2(100, 600));
		}
		updateBoundary();
	}

	void updateDisplayRect()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0)) -  Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

		displayRect.width = a.x;
		displayRect.height = a.y;
	}

	void jump(Vector2 v)
	{
		rigidbody.AddForce(v);
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Abs(scale.x) * (v.x > 0 ? 1 : -1);
		transform.localScale = scale;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			animator.SetBool("Ground", true);
			rigidbody.velocity = Vector2.zero;
		}
		else
		{
			animator.SetBool("Ground", true);
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

	Vector2 getReflex(Vector2 I ,Vector2 N)
	{
		I = I.normalized;
		N = N.normalized;
		Vector2 R = I - 2 * Vector3.Dot(I , N) * N;
		return R;
	}

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
}
