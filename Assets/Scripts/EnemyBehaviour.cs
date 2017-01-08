using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    //Make the implementation of the enemy be like that of player's;
    //This script should go on the body gameObject

    Rigidbody2D rb;
    GameObject player;
    
    //Colour of enemy
    public Color color;
    //SpriteRenderer of the body
    public SpriteRenderer body;
    Vector2 directionToPlayer;
    
    //Detection range?
    public float detectionRange;
    public float speed;	
    
    bool sighted;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        if (player == null){
            Debug.LogWarning("Cannot find player object!");
        }
        else{
            directionToPlayer = Vector2.zero;
        }
        body.color = color;
        sighted = false;
    }
    
    void Update() {
        if (sighted) {
            //TODO: What happens if enemy sees player
        }
        else {
            //TODO: What happens normally
        }
    }
    
    void FixedUpdate(){
        if (player != null) {
            directionToPlayer = transform.position - player.transform.position;
            RaycastHit2D playerFind = Physics2D.Raycast(transform.position, directionToPlayer, distance:detectionRange);
            if (playerFind.collider && playerFind.collider.gameObject.CompareTag("Player")){
                sighted = true;
            }
            else {
                sighted = false;
            }
        }
        else {
            //Recommended to use FindWithTag instead when calling repeatedly
            player = GameObject.Find("Player");
            if (player){
                Debug.Log("Player Found!");
            }
        }
    }

    void FaceDirection() {
        //TODO: Rotate face when enemy changes direction
    }
    
    //Wanna try out SendMessage as a means of passing values between objects.
    //In the player body's script implement a method GetColor that takes in a Color object
    //And change the body colour accordingly
    //Also, in the method that determines killing, call this method using sendMessage
    //and pass in the player GameObject
    void GetKilled(GameObject killer) {
        killer.SendMessage("changeColor", color);
        Die();
    }

    //Fun
    void Die() {
        rb.velocity = Vector2.zero; 
        rb.AddTorque(9001f);
        Destroy(gameObject, 5.0f);
        GetComponent<Collider2D>().enabled = false;
    }

}