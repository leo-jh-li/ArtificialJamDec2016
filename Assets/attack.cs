using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	public GameObject instruction;
	private IDictionary<string, Coroutine> coroutines;

	void Start(){
		coroutines = new Dictionary<string, Coroutine> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			//Enable the instructions
			instruction.GetComponent<SpriteRenderer> ().enabled = true;
			instruction.GetComponent<Animator> ().enabled = true;

			//Run co-routine which enables ability to kill
			Coroutine co = StartCoroutine(killTarget(col.gameObject));
			coroutines.Add (col.gameObject.name, co);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		//Disable the instructions
		if(col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			instruction.GetComponent<SpriteRenderer>().enabled = false;
			instruction.GetComponent<Animator> ().enabled = false;
			if(coroutines[col.gameObject.name] != null)
				StopCoroutine (coroutines[col.gameObject.name]);				//Stop the coroutine (if still running)	
			coroutines.Remove(col.gameObject.name);								//Remove the coroutine reference
		}
	}

	IEnumerator killTarget(GameObject enemy){
		while (true) {
			if (Input.GetButton ("attack")) {
				enemy.SendMessage ("GetKilled", transform.parent.gameObject);
				break;
			}
			yield return new WaitForFixedUpdate ();
		}
		yield return null;
	}
}
