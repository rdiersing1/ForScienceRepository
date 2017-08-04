using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControler : MonoBehaviour {

    public float monsterSpeed = 1f;
    public Rigidbody2D monsterRB;

	void Start () {
        monsterRB.velocity = new Vector2(-monsterSpeed, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
