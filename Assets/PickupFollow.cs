using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFollow : MonoBehaviour {

	public bool followPlayer;
	public GameObject player;
	public float speed;

	void Start () {
		followPlayer = false;
	}

	void FixedUpdate () {
		if (followPlayer) {
			transform.position = Vector3.Lerp (this.transform.position, player.transform.position, 0.01f);
			/*
			transform.LookAt(player.transform);
			transform.position += transform.forward * speed * Time.deltaTime;
			*/
		}
	}

	public void Follow(){
		followPlayer = true;
	}
}
