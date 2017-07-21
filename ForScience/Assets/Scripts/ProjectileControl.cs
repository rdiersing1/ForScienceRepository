using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2D;
	// Use this for initialization
	//Code from unity tortorial space shooter
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		rb2D.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
