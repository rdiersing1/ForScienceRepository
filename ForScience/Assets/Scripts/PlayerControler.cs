using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : PhysicsEngine {

	public float speed;

	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        targetVelocity = Vector2.right * speed;
    }
		
}
