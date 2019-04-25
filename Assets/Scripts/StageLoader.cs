using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
	public string nextLevelName;

    public IEnumerator GoToNextLevel() {
        yield return new WaitForSeconds(0.5f);
        if (Application.CanStreamedLevelBeLoaded(nextLevelName)) {
		    SceneManager.LoadScene(nextLevelName);
        } else {
            Debug.LogWarning("Failed to load level " + nextLevelName);
        }
    }
}
