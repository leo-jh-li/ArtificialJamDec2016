using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleEnemy : EnemyBehaviour {

    public void GetKilled()
    {
        Debug.Log("Nope");
    }
}
