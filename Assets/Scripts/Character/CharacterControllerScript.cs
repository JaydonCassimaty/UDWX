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
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
		}
		//North-West Movement
		else if (z >= 0.01f && x <= -0.01f)
		{
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
		}
		//South-East Movement
		else if (z <= -0.01f && x >= 0.01f)
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
