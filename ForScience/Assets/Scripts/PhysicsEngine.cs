using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    Much of this code was derived from a unity tutorial:
    https://unity3d.com/learn/tutorials/topics/2d-game-creation/horizontal-movement?playlist=17093
     */

public class PhysicsEngine : MonoBehaviour {

    public float gravityModifier = 1f;
    public float minGroundNormalY = 0.65f;

    protected bool isGrounded;
    protected Vector2 groundNormal;
    protected Vector2 velocity;
    protected Rigidbody2D rb2D;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

    protected const float minMoveDist = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable() {
        rb2D = GetComponent<Rigidbody2D> ();
    }

    // Use this for initialization
    void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // FixedUpdate is called on a constant interval
    private void FixedUpdate() {

        isGrounded = false;
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPosition.y;
        Movement (move, true);
    }

    private void Movement(Vector2 move, bool yMovement) {

        float distance = move.magnitude;

        if (distance > minMoveDist) {                                   // Note minMoveDist > gravityModifier * Physics2d.gravity * dt
                                                                        // if not the item will fall through floors slowly (which is bad)
            int count = rb2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);  
            hitBufferList.Clear();
            for (int i = 0; i < count; ++i) {                           // Cast reorders hitBuffer
                hitBufferList.Add (hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; ++i) {             // This will only be entered with objects that your colliding with

                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY) {               // The normal vector is a unit vector so this is basically comparing the angles of the two
                                                                        // sin(theta) = currentNormal.y where theta is the angle between the ground normal and x axis
                    isGrounded = true;
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0) {
                    velocity = velocity - projection * currentNormal;       // So you dont walk into walls when walking up a slope
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2D.position += move.normalized * distance;
    }
}
