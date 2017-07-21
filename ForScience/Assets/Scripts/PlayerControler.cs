using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : PhysicsEngine {

	public float runSpeed;
    public float jumpInitialSpeed;
    public float jumpHeight;

	private Rigidbody2D playerRB;

	// Start is called during initilization 
	void Start () {
        
	}

    // ComputeVelocity is called during update and is what you should use to compute the velocity
    protected override void ComputeVelocity() {
        Vector2 move = new Vector2(runSpeed, 0f);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Movement(new Vector2(0f, jumpHeight), true);        // Movement() is great for telliporting! 
            velocity.y = jumpInitialSpeed;                      // Velocity is great for when you want to set velocity for more than a frame
        }

        targetVelocity = move;                                  // Target velocity is good for when you want set velocity for a frame
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Trap")) {                     // Activates when the player hits a trap
            print("The player has hit a trap");
        }
    }

}
