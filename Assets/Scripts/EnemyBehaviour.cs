using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    //Make the implementation of the enemy be like that of player's;
    //This script should go on the body gameObject

    Rigidbody2D rb;
    GameObject player;
    GameObject face;

    public Vector2 facing;
    public Vector2 directionToPlayer;

    //Colour of enemy
    public Color color;
    //SpriteRenderer of the body
    public SpriteRenderer body;

    //Detection range?
    public float detectionRange;
    public float detectionAngle;
    public float speed;

    bool sighted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogWarning("Cannot find player object!");
        }
        else {
            directionToPlayer = Vector2.zero;
        }
        body.color = color;
        sighted = false;
        facing = Vector2.up;
        face = transform.FindChild("EnemyFace").gameObject;
    }

    void Update()
    {
        FindPlayer();
        if (sighted)
        {
            //TODO: What happens if enemy sees player
            facing = directionToPlayer;
            FaceDirection();
        }
        else {
            //TODO: What happens normally
        }
    }

    void FixedUpdate()
    {

    }

    //Orients character to direction of facing
    void FaceDirection()
    {
        face.transform.rotation = Quaternion.Euler(0, 0, calculateAngle(Vector2.down, facing));
    }

    //Fires a raycast toward the player and sets sighted
    //If player is null, attempts to find the player gameObject instead.
    void FindPlayer()
    {
        if (player != null)
        {
            directionToPlayer = player.transform.position - transform.position;
            RaycastHit2D playerFind = Physics2D.Raycast(transform.position, directionToPlayer, distance: detectionRange);
            if (playerFind.collider && playerFind.collider.gameObject.CompareTag("Player") 
                && Vector2.Angle(facing, directionToPlayer) <= detectionAngle)
            {
                sighted = true;
            }
            else {
                sighted = false;
            }
        }
        else {
            //Recommended to use FindWithTag instead when calling repeatedly
            player = GameObject.Find("Player");
            if (player)
            {
                Debug.Log("Player Found!");
            }
        }
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
    void Die()
    {
        rb.velocity = Vector2.zero;
        rb.AddTorque(9001f);
        Destroy(gameObject, 5.0f);
        //Somewhere far away
        Collider2D goaway = GetComponent<Collider2D>();
        goaway.transform.position = new Vector2(999, 999);
        goaway.enabled = false;
    }

    // CP from PlayerController
    public float calculateAngle(Vector2 vec1, Vector2 vec2)
    {
        float dotProduct = Vector2.Dot(vec1, vec2);
        float determinant = vec1.x * vec2.y - vec1.y * vec2.x;
        float angle = Mathf.Atan2(determinant, dotProduct) * 180 / Mathf.PI;

        return angle;
    }

}