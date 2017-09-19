using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	private float moveSpeed;
	private float jumpPressure;
	private float minJump;
	private float maxJumpPressure;
	public Rigidbody rb;
	public bool onGround;

	// Use this for initialization
	void Start () {
		moveSpeed = 10f;
		jumpPressure = 0f;
		minJump = 2f;
		maxJumpPressure = 10f;
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		if(onGround){
		transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f,moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);

		if(Input.GetButton("Jump")){
			if(jumpPressure < maxJumpPressure)
			{
				jumpPressure += Time.deltaTime*10f;
			} else {
				jumpPressure = maxJumpPressure;
			}
		}

		else
		{
			if (jumpPressure > 0f) {
				jumpPressure = jumpPressure + minJump;
				rb.velocity = new Vector3(jumpPressure/10f, jumpPressure,0f);
				jumpPressure = 0f;
				onGround = false;
			}
		}
	}
}

void OnCollisionEnter(Collision other){
	if(other.gameObject.CompareTag("ground"))
	{
		onGround = true;
	}
}

}
