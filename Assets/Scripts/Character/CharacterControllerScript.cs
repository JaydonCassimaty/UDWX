using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	private float moveSpeed;
	private Rigidbody rb;
	private SpriteRenderer flipSprite;
	private bool onGround;
	public float maxJump = 10.0f;
	Animator anim;

	// Use this for initialization
	void Start () {
		onGround = true;
		moveSpeed = 1f;
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		flipSprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		anim.SetFloat("Speed", 0);
		anim.SetBool("N", false);
		anim.SetBool("S", false);
		anim.SetBool("W-E", false);
		anim.SetBool("NW-NE", false);
		anim.SetBool("SW-SE", false);

		var x = Input.GetAxis("Horizontal") * 0.1f;
    var z = Input.GetAxis("Vertical") * 0.1f;
		var y = Input.GetAxis("Jump") * 0.1f;

    transform.Translate(x, y, z);

		//North-East Movement
		if (z >= 0.01f && x >= 0.01f)
		{
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
		}
		//North-West Movement
		else if (z >= 0.01f && x <= -0.01f)
		{
			Debug.Log("x:" + x + "Z:" + z, gameObject);
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
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
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
		}

		//South-East Movement
		if (z <= -0.01f && x >= 0.01f)
		{
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
		}
		//South-West Movement
		else if (z <= -0.01f && x <= -0.01f)
		{
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
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
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
		}


	}

	// void FixedUpdate () {
	// 	anim.SetFloat("Speed", 0);
	// 	anim.SetBool("N", false);
	// 	anim.SetBool("S", false);
	// 	anim.SetBool("W-E", false);
	// 	anim.SetBool("NW-NE", false);
	// 	anim.SetBool("SW-SE", false);
	// 	rb.velocity -= new Vector3 (0, 0.1f, 0f);
	//
	// 	if (Input.GetKey("space") && onGround)
	// 	{
	// 		onGround = false;
	// 		anim.SetBool("grounded", false);
	// 		transform.Translate(Vector3.up * maxJump);
	// 		//rb.velocity = new Vector3(0f, 6f, 0f);
	// 	}
	//
	// 	if ((Input.GetKey("left") && Input.GetKey("up")) || (Input.GetKey("a") && Input.GetKey("w")))
	// 	{
	// 		flipSprite.flipX = false;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("NW-NE", true);
	// 		transform.Translate(Vector3.left * (moveSpeed/10));
	// 		transform.Translate(Vector3.forward * (moveSpeed/10));
	// 	}
	//
	// 	if ((Input.GetKey("left") && Input.GetKey("down")) || (Input.GetKey("a") && Input.GetKey("s")))
	// 	{
	// 		flipSprite.flipX = false;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("SW-SE", true);
	// 		transform.Translate(Vector3.left * (moveSpeed/10));
	// 		transform.Translate(Vector3.back * (moveSpeed/10));
	// 	}
	//
	// 	if ((Input.GetKey("right") && Input.GetKey("up")) || (Input.GetKey("d") && Input.GetKey("w")))
	// 	{
	// 		flipSprite.flipX = true;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("NW-NE", true);
	// 		transform.Translate(Vector3.right * (moveSpeed/10));
	// 		transform.Translate(Vector3.forward * (moveSpeed/10));
	// 	}
	//
	// 	if ((Input.GetKey("right") && Input.GetKey("down")) || (Input.GetKey("d") && Input.GetKey("s")))
	// 	{
	// 		flipSprite.flipX = true;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("SW-SE", true);
	// 		transform.Translate(Vector3.right * (moveSpeed/10));
	// 		transform.Translate(Vector3.back * (moveSpeed/10));
	// 	}
	//
	// 	if (Input.GetKey("right") || Input.GetKey("d"))
	// 	{
	// 		flipSprite.flipX = true;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("W-E", true);
	// 		transform.Translate(Vector3.right * (moveSpeed/10));
	// 	}
	//
	// 	if (Input.GetKey("left") || Input.GetKey("a"))
	// 	{
	// 		flipSprite.flipX = false;
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("W-E", true);
	// 		transform.Translate(Vector3.left * (moveSpeed/10));
	// 	}
	//
	// 	if (Input.GetKey("up") || Input.GetKey("w"))
	// 	{
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("N", true);
	// 		transform.Translate(Vector3.forward * (moveSpeed/10));
	// 	}
	//
	// 	if (Input.GetKey("down") || Input.GetKey("s"))
	// 	{
	// 		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	// 		anim.SetBool("S", true);
	// 		transform.Translate(Vector3.back * (moveSpeed/10));
	// 	}
	// }

 	//Collision Code
	// void OnCollisionEnter (Collision col)
	// {
	// 	if(col.gameObject.tag == "ground")
	// 	{
	// 		anim.SetBool("grounded", true);
	// 		onGround = true;
	// 	}
	// }
}
