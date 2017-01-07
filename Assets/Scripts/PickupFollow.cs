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
			transform.position = Vector3.Lerp (this.transform.position, player.transform.position, speed);

		}
	}

	public void Follow(){
		followPlayer = true;
	}
}
