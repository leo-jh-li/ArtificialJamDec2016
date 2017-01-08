using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	public GameObject instruction;
	public GameObject instruction2;
	private IDictionary<string, Coroutine> coroutines;

	void Start(){
		coroutines = new Dictionary<string, Coroutine> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy") && 
			!col.gameObject.GetComponent<EnemyBehaviour> ().dying && 
			col.gameObject.GetComponent<InvincibleEnemy>() == null) {
			//Enable the instructions
			instruction.GetComponent<SpriteRenderer> ().enabled = true;
			instruction.GetComponent<Animator> ().enabled = true;
			instruction2.GetComponent<SpriteRenderer> ().enabled = false;		//safe to ensure disable the other instruction

			//Run co-routine which enables ability to kill
			Coroutine co = StartCoroutine (killTarget (col.gameObject));
			if (!coroutines.ContainsKey (col.gameObject.name))
				coroutines.Add (col.gameObject.name, co);
		} 
		else if(col.gameObject.layer == LayerMask.NameToLayer ("Enemy") && 
				!col.gameObject.GetComponent<EnemyBehaviour> ().dying && 
				col.gameObject.GetComponent<InvincibleEnemy>() != null) {
			//Enable the instructions (cant do anything)
			instruction2.GetComponent<SpriteRenderer> ().enabled = true;
			instruction.GetComponent<SpriteRenderer> ().enabled = false;		//safe to ensure disable the other instruction
			instruction.GetComponent<Animator> ().enabled = false;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		//Disable the instructions
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy") && col.gameObject.GetComponent<InvincibleEnemy> () == null) {
			instruction.GetComponent<SpriteRenderer> ().enabled = false;
			instruction.GetComponent<Animator> ().enabled = false;
			if (coroutines.ContainsKey (col.gameObject.name)) {
				StopCoroutine (coroutines [col.gameObject.name]);				//Stop the coroutine (if still running)	
				coroutines.Remove (col.gameObject.name);							//Remove the coroutine reference
			}
		} 
		else if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy") && col.gameObject.GetComponent<InvincibleEnemy> () != null) {
			//Disable the instructions (cant do anything)
			instruction2.GetComponent<SpriteRenderer> ().enabled = false;
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
