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
		int myLayer = (1 << LayerMask.NameToLayer ("Player")) | (1 << LayerMask.NameToLayer ("Wall"));
		foreach (GameObject player in playerReference) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, (player.transform.position - transform.position).normalized, Mathf.Infinity, myLayer);
			if (hit.collider != null) {
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Player")) {
					foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer> ()) {
						sr.enabled = true;
					}
					break;
				} else {
					foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer> ()) {
						sr.enabled = false;
					}
				}
			}
		}
	}
}
