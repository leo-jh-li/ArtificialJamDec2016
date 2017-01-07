using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float moveHorizontal;
	private float moveVertical;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
	}

	void FixedUpdate(){
		if (moveHorizontal == 0) {
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
		if (moveVertical == 0) {
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb.AddForce(movement * speed);
		print (movement.ToString ());
	}
}
