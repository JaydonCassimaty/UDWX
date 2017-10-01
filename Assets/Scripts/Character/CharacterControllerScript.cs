using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControllerScript : NetworkBehaviour {
	public float moveSpeed = 0.1f;
	private bool onGround = true;

	SpriteRenderer localSprite;
	Animator animator;
	Rigidbody rb;

	//some flags to check when certain animations are playing
	// bool _isPlaying_walk = false;

	[SyncVar(hook = "changeDirection")]
	string _currentDirection = "left";

	//Current Spell implementation
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float projectileForce = 1f;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		localSprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
    	return;

		if (Input.GetKeyDown(KeyCode.Z))
		{
    	CmdFire();
		}

		var x = Input.GetAxis("Horizontal") * moveSpeed;
    var z = Input.GetAxis("Vertical") * moveSpeed;
		var y = Input.GetAxis("Jump");

		bool isWalking = (Mathf.Abs(x) + Mathf.Abs(z)) > 0;

		animator.SetBool("isWalking", isWalking);

		//Jump
		if (Input.GetAxis("Jump") > 0)
		{
				onGround = false;
	 		 	transform.Translate(0f, Mathf.Clamp(y, 0.08f, 0.08f), 0f);
		}

		if(isWalking)
		{
			animator.SetFloat("x", x);
			animator.SetFloat("y", z);

			transform.Translate(x, 0f, z);
			//North-East Movement
			if (z >= 0.01f && x >= 0.01f)
			{
				CmdChangeDirection("right");
			}
			//North-West Movement
			else if (z >= 0.01f && x <= -0.01f)
			{
				CmdChangeDirection("left");
			}
			//South-East Movement
			else if (z <= -0.01f && x >= 0.01f)
			{
				CmdChangeDirection("right");
			}
			//South-West Movement
			else if (z <= -0.01f && x <= -0.01f)
			{
				CmdChangeDirection("left");
			}
			//East Movement
			else if (x >= 0.01f)
			{
				CmdChangeDirection("right");
			}
			//West Movement
			else if (x <= -0.01f)
			{
				CmdChangeDirection("left");
			}
		}
	}

	//Effects for local player
	public override void OnStartLocalPlayer()
	{
		// transform.LookAt(Camera.main.transform);
		Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
		GetComponentInChildren<SpriteRenderer>().color = new Color(1f,0.30196078f, 0.30196078f);
	}

	[Command]
	void CmdFire()
	{

		var mana = GetComponent<Mana>();
		var spellCost = 10;

		if (mana.ReturnMana() > spellCost)
		{
			mana.UseMana(spellCost);

			//Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
			var clonedBullet = Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);

			//Add force to the instantiated bullet, pushing it forward away from the bulletSpawn location, using projectile force for how hard to push it away
			clonedBullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * projectileForce);

			// // Create the Bullet from the Bullet Prefab
			// var bullet = (GameObject)Instantiate (
			// 		bulletPrefab,
			// 		bulletSpawn.position,
			// 		bulletSpawn.rotation);
			//
			// // Add velocity to the bullet
			// bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

			// Spawn the bullet on the Clients
			NetworkServer.Spawn(clonedBullet);

			// Destroy the bullet after 2 seconds
			Destroy(clonedBullet, 2.0f);
		}
	}

	//--------------------------------------
	// Change the players animation state
	//--------------------------------------
	// void changeState(int state){
	//
	// 		if (_currentAnimationState == state)
	// 		return;
	//
	// 		switch (state) {
	//
	// 		case STATE_WALK_UP:
	// 				animator.SetInteger("state", STATE_WALK_UP);
	// 				break;
	//
	// 		case STATE_WALK_DOWN:
	// 				animator.SetInteger("state", STATE_WALK_DOWN);
	// 				break;
	//
	// 		case STATE_WALK_LEFT:
	// 				animator.SetInteger("state", STATE_WALK_LEFT);
	// 				break;
	//
	// 		case STATE_JUMP:
	// 				animator.SetInteger("state", STATE_JUMP);
	// 				break;
	//
	// 		case STATE_WALK_DL:
	// 				animator.SetInteger("state", STATE_WALK_DL);
	// 				break;
	//
	// 		case STATE_WALK_UL:
	// 				animator.SetInteger("state", STATE_WALK_UL);
	// 				break;
	//
	// 		case STATE_IDLE:
	// 				animator.SetInteger("state", STATE_IDLE);
	// 				break;
	// 		}
	// 		_currentAnimationState = state;
	// }

	//--------------------------------------
  // Check if player has collided with the floor
  //--------------------------------------
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "ground")
		{
			onGround = true;
		}
	}

	//--------------------------------------
	// Flip player sprite for left/right walking on server
	//--------------------------------------
	[Command]
	public void CmdChangeDirection(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				localSprite.flipX = true;
				_currentDirection = "right";
			}
			else if (direction == "left")
			{
				localSprite.flipX = false;
	      _currentDirection = "left";
			}
		}
	}

	//--------------------------------------
	// Flip player sprite for left/right walking
	//--------------------------------------
	void changeDirection(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				localSprite.flipX = true;
				_currentDirection = "right";
			}
			else if (direction == "left")
			{
				localSprite.flipX = false;
	      _currentDirection = "left";
			}
		}
	}

}
