using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opposum : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialPosition;

    public float speed = 2.0f;
    public float chasingSpeed = 3.0f;
    public float maxDistance;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        initialPosition = transform.position;
    }

    void Update() {
        Vector2 velocity = new Vector2(speed, rigidBody.velocity.y);
        float distanceToCenter = transform.position.x - initialPosition.x;
        float distanceToPlayer = Vector3.Distance(initialPosition, 
                                                  player.position);
        if (Mathf.Abs(distanceToCenter) > maxDistance) {
            spriteRenderer.flipX = (distanceToCenter < 0);
        }
        else if (distanceToPlayer < maxDistance - 0.1) {
            spriteRenderer.flipX = (player.position.x > transform.position.x);
            velocity.x = chasingSpeed;
        }
        velocity.x = (spriteRenderer.flipX ? velocity.x : -velocity.x);
        rigidBody.velocity = velocity;
    }
}
