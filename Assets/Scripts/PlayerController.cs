﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float moveHorizontal;
	private float moveVertical;
	public float speed;
	public GameObject[] toRotate;
	Stack keys;
	SpriteRenderer renderer;
	public AudioSource audio;
	public AudioClip bump;
	public AudioClip open;
	private StageLoader stageLoader;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		keys = new Stack();
		renderer = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
		stageLoader = GetComponent<StageLoader>();
	}
	
	// Update is called once per frame
	void Update () {
		moveHorizontal = Input.GetAxisRaw ("Horizontal");
		moveVertical = Input.GetAxisRaw ("Vertical");
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
			float angle = calculateAngle (from, movement);

			foreach (GameObject g in toRotate) {
				g.transform.rotation = Quaternion.Euler (0,0,angle);
			}
		}
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		// Makes key follow the player on contact
		if (other.gameObject.CompareTag ("Key")) {
			other.gameObject.GetComponent<PickupFollow> ().followPlayer = true;
			other.gameObject.GetComponent<PickupFollow> ().player = this.gameObject;
			other.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
			other.gameObject.layer = LayerMask.NameToLayer("Player");
			//update inventory
			keys.Push (other.gameObject);
			// disable illuminateEnemy for key
			//other.gameObject.GetComponent<illuminateEnemy>().enabled = false;
		} else if (other.gameObject.CompareTag ("StarFloor")) {
			print ("Level Complete");
			StartCoroutine(stageLoader.GoToNextLevel());
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("KeyDoor")) {
			if (checkHasKey ()) {
				audio.clip = open;
				audio.Play ();
				coll.gameObject.GetComponent<OpenDoor> ().Open();
				//remove key
				GameObject popped = (GameObject) keys.Pop ();
				popped.SetActive (false);
			} else {
				// bump into door sound
				audio.clip = bump;
				audio.Play ();
			}
		}
		else if (coll.gameObject.CompareTag ("ColourDoor")) {
			print ("Colour door checker called");
			// TODO: this
			//ChangeColour.changeColor (renderer.color);
			ColourDoorManager.TryOpen (this.transform.Find("Body").gameObject.GetComponent<SpriteRenderer>().color, coll.gameObject.GetComponent<SpriteRenderer>().color, coll);
		}
	}



	public  bool checkHasKey(){
		return !(keys.Count == 0);
	}

	public float calculateAngle(Vector2 vec1, Vector2 vec2){
		float dotProduct = Vector2.Dot (vec1, vec2);
		float determinant = vec1.x * vec2.y - vec1.y * vec2.x;
		float angle = Mathf.Atan2 (determinant, dotProduct) * 180/Mathf.PI;

		return angle;
	}


}
