using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illuminateRoom : MonoBehaviour {
	public float lerpSpeed = 0.5f;

	void OnTriggerEnter2D(Collider2D col){
		//Find the parent room and illuminate the place (Make visible)
		Transform parent = transform.parent;
		GetComponent<Collider2D> ().enabled = false;
		StartCoroutine (findIllum(parent));
		print (parent);
	}

	IEnumerator findIllum(Transform parent){
		for (int i = 0; i < parent.childCount; i++) {
			GameObject child = parent.GetChild (i).gameObject;
			print (child);
			StartCoroutine (lerpIllumination(child.GetComponent<SpriteRenderer> ()));
			yield return new WaitForFixedUpdate ();
		}
		yield return null;
	}

	IEnumerator lerpIllumination(SpriteRenderer sr){
		while (sr.color != Color.white) {
			float alpha = Mathf.Lerp (sr.color.a, 1, lerpSpeed);
			sr.color = new Color(1f, 1f, 1f, alpha);
			yield return new WaitForFixedUpdate ();
		}
		yield return null;
	}
}
