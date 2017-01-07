using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float moveHorizontal;
	private float moveVertical;
	public float speed;
	public GameObject face;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		moveHorizontal = Input.GetAxisRaw ("Horizontal");
		moveVertical = Input.GetAxisRaw ("Vertical");
		print (new Vector2(moveHorizontal, moveVertical).ToString ());
	}

	void FixedUpdate(){
		
		if (moveHorizontal < 0.1f) {
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
		if (moveVertical < 0.1f) {
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb.velocity = movement * speed;

		//transform body
		if (moveHorizontal != 0 || moveVertical != 0) {
			Vector2 from = new Vector2 (0, -1);
			float dotProduct = Vector2.Dot (from, movement);
			/*
			dotProduct = dotProduct / (from.magnitude * movement.magnitude);
			float acos = Mathf.Acos (dotProduct);
			float angle = acos * 180 / Mathf.PI;
			*/
			float determinant = from.x * movement.y - from.y * movement.x;
			float angle = Mathf.Atan2 (determinant, dotProduct) * 180/Mathf.PI;
			face.transform.rotation = Quaternion.Euler (0,0,angle);


			//face.transform.rotation = Quaternion.Euler (0,0, Vector2.Angle (from, movement));
		}
	}
}
