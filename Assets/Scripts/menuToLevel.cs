using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuToLevel : MonoBehaviour {

	public void loadLevelOne(){
		SceneManager.LoadScene("Stage1v2");
	}
}
