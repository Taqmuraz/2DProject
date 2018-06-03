using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour {
	public Rigidbody2D body { get; private set; }
	public SpriteRenderer rend { get; private set; }
	public Transform trans { get; private set; }
	public CapsuleCollider2D coll { get; private set; }
	public MDMachine momentDoMachine { get; private set; }
	public MotionMachine motionMachine { get; private set; }
	public AnimMachine animMachine { get; private set; }

	public static Character player { get; private set; }

	[SerializeField]
	private string characterName;

	public const float moveSpeed = 3f;

	public Vector2 position
	{
		get
		{
			return (Vector2)trans.position;
		}
		set
		{
			trans.position = (Vector3)value;
		}
	}

	public Vector2 velocity
	{
		get {
			return body.velocity;
		}
		set {
			if (onGround) {
				if (Mathf.Abs(value.x) > 0) {
					rend.flipX = value.x < 0;
				}
				body.velocity = new Vector2(value.x * moveSpeed, body.velocity.y);
			}
		}
	}

	public bool isMoving
	{
		get {
			return Mathf.Abs(velocity.x) > 0.2f;
		}
	}

	public bool onGround
	{
		get {
			float radius = coll.size.x;
			Vector2 point = position + Vector2.up * radius / 2;
			return Physics2D.OverlapCircleAll (point, radius).FirstOrDefault((Collider2D c) => !c.isTrigger && c != coll);
		}
	}

	private void Start () {
		player = this;
		body = GetComponent<Rigidbody2D> ();
		coll = GetComponent<CapsuleCollider2D> ();
		trans = transform;
		rend = GetComponentInChildren<SpriteRenderer> ();
		momentDoMachine = MDMachine.Humanic_Player (this);
		motionMachine = MotionMachine.Humanic (this);
		animMachine = new AnimMachine (characterName, this);
	}

	private void Update () {
		momentDoMachine.MDMUpdate ();
		motionMachine.MMUpdate ();
		animMachine.Update ();
	}

	public void MoveAt (Vector3 dir) {
		velocity = dir;
	}
}
