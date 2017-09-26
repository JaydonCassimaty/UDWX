using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControllerScript : NetworkBehaviour {
	private float moveSpeed;
	private Rigidbody rb;
	private SpriteRenderer localSprite;
	private bool onGround;
	Animator anim;

	public GameObject bulletPrefab;

	public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		onGround = true;
		moveSpeed = 1f;
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		localSprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{

		if (!isLocalPlayer)
		{
    	return;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
    	CmdFire();
		}

		anim.SetFloat("Speed", 0);
		anim.SetBool("N", false);
		anim.SetBool("S", false);
		anim.SetBool("W-E", false);
		anim.SetBool("NW-NE", false);
		anim.SetBool("SW-SE", false);
		anim.SetBool("grounded", true);

		var x = Input.GetAxis("Horizontal") * 0.1f;
    var z = Input.GetAxis("Vertical") * 0.1f;
		var y = Input.GetAxis("Jump") * 0.1f;

    transform.Translate(x, 0f, z);

		//Jump
		if(Input.GetAxis("Jump") > 0) {
		 transform.Translate(0f, y, 0f);
		 anim.SetBool("grounded",  false);
		 onGround = false;
		}
		//North-East Movement
		if (z >= 0.01f && x >= 0.01f)
		{
			localSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
		}
		//North-West Movement
		else if (z >= 0.01f && x <= -0.01f)
		{
			localSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
		}
		//South-East Movement
		else if (z <= -0.01f && x >= 0.01f)
		{
			localSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
		}
		//South-West Movement
		else if (z <= -0.01f && x <= -0.01f)
		{
			localSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
		}
		//North Movement
		else if (z >= 0.01f)
		{
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("N", true);
		}
		//East Movement
		else if (x >= 0.01f)
		{
			localSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
		}
		//South Movement
		else if (z <= -0.01f)
		{
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("S", true);
		}
		//West Movement
		else if (x <= -0.01f)
		{
			localSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
		}
	}

	//Effects for local player
	public override void OnStartLocalPlayer()
	{
		Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
		GetComponent<SpriteRenderer>().color = new Color(1f,0.30196078f, 0.30196078f);
	}

	[Command]
	void CmdFire()
	{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate (
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
    NetworkServer.Spawn(bullet);

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
	}

 	//Collision Code
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "ground")
		{
			anim.SetBool("grounded", true);
			onGround = true;
		}
	}
}
