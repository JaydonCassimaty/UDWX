using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	private float moveSpeed;
	private Rigidbody rb;
	private SpriteRenderer flipSprite;
	Animator anim;

	// Use this for initialization
	void Start () {
		moveSpeed = 1f;
		anim = GetComponent<Animator>();
		flipSprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate () {
		anim.SetFloat("Speed", 0);
		anim.SetBool("N", false);
		anim.SetBool("S", false);
		anim.SetBool("W-E", false);
		anim.SetBool("NW-NE", false);
		anim.SetBool("SW-SE", false);

		if ((Input.GetKey("left") && Input.GetKey("up")) || (Input.GetKey("a") && Input.GetKey("w"))) {
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
			transform.Translate(Vector3.left * (moveSpeed/10));
			transform.Translate(Vector3.forward * (moveSpeed/10));
		} else if ((Input.GetKey("left") && Input.GetKey("down")) || (Input.GetKey("a") && Input.GetKey("s"))) {
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
			transform.Translate(Vector3.left * (moveSpeed/10));
			transform.Translate(Vector3.back * (moveSpeed/10));
		} else if ((Input.GetKey("right") && Input.GetKey("up")) || (Input.GetKey("d") && Input.GetKey("w"))) {
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("NW-NE", true);
			transform.Translate(Vector3.right * (moveSpeed/10));
			transform.Translate(Vector3.forward * (moveSpeed/10));
		} else if ((Input.GetKey("right") && Input.GetKey("down")) || (Input.GetKey("d") && Input.GetKey("s"))) {
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("SW-SE", true);
			transform.Translate(Vector3.right * (moveSpeed/10));
			transform.Translate(Vector3.back * (moveSpeed/10));
		} else if (Input.GetKey("right") || Input.GetKey("d")) {
			flipSprite.flipX = true;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
			transform.Translate(Vector3.right * (moveSpeed/10));
		} else if (Input.GetKey("left") || Input.GetKey("a")) {
			flipSprite.flipX = false;
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("W-E", true);
			transform.Translate(Vector3.left * (moveSpeed/10));
		} else if (Input.GetKey("up") || Input.GetKey("w")) {
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("N", true);
			transform.Translate(Vector3.forward * (moveSpeed/10));
		} else if (Input.GetKey("down") || Input.GetKey("s")) {
			anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
			anim.SetBool("S", true);
			transform.Translate(Vector3.back * (moveSpeed/10));
		} else if (Input.GetKey("space")) {
			transform.Translate(Vector3.up * (moveSpeed/10));
		}
	}

}
