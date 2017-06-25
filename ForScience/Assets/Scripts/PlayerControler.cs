using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public Collider2D ground;

	private bool isJumping;
	private bool wasJumping;

	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();
		playerRB.velocity = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!isJumping) {
			if (Input.GetKey ("space")) {
				if (playerRB.IsTouchingLayers ( 3 << 8 )) { 	// Layer mask for layers 8 & 9
					playerRB.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
					isJumping = true;
				}
			}
		}
		else {
			isJumping = false;
		}
	}
		
}
