using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illuminateEnemy : MonoBehaviour {

	public GameObject[] playerReference;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject player in playerReference) {
			RaycastHit2D hit = Physics2D (transform.position, (player.transform.position - transform.position));
			if (hit.collider != null) {
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Player")) {
					GetComponent<SpriteRenderer> ().enabled = true;
				}
				break;
			}
		}
	}
}
