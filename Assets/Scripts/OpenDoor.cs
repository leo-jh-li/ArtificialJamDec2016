using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	public Sprite doorSprite;
	private SpriteRenderer sr;

	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
	}

	public void Open () {
		//replace door with panel
		sr.sprite = doorSprite;
	}
}
