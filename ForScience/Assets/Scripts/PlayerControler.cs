using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : PhysicsEngine {

	public float runSpeed;
    public float jumpInitialSpeed;
    public float jumpHeight;

	private Rigidbody2D playerRB;

    // ComputeVelocity is called during update and is what you should use to compute the velocity
    protected override void ComputeVelocity() {
        targetVelocity = new Vector2(runSpeed, 0f);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Movement(new Vector2(0f, jumpHeight), true);        // Movement() is great for telliporting! 
            velocity.y = jumpInitialSpeed;                      // Velocity is great for when you want to set velocity for more than a frame
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Trap") || collision.CompareTag("Monster")) {     // Activates when the player hits a trap or monster
            SceneManager.LoadScene("DeathMenu");
        }
    }
}
