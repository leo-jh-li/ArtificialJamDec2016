using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	public Sprite doorSprite;
	public bool hasKey;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {

		hasKey = false;
		sr = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasKey) {
			//replace door with panel
			sr.sprite = doorSprite;
		}
			
	}
}
