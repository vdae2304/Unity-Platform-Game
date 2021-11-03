using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialPosition;

    public Vector2 velocity;
    public Vector2 maxDistance;
    public float visibilityRadius;

    public float dropProbability = 0.2f;
    public GameObject[] dropItems;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        initialPosition = transform.position;
        rigidBody.velocity = velocity;
    }

    void Update() {
        Vector2 newVelocity = rigidBody.velocity;
        Vector2 distanceToCenter = transform.position - initialPosition;
        float distanceToPlayer = Vector3.Distance(initialPosition, 
                                                  player.position);
        if (velocity.x == 0 && distanceToPlayer <= visibilityRadius) {
            spriteRenderer.flipX = (player.position.x > transform.position.x);
        }
        if (Mathf.Abs(distanceToCenter.x) > maxDistance.x) {
            spriteRenderer.flipX = (distanceToCenter.x < 0);
            newVelocity.x = (spriteRenderer.flipX ? velocity.x : -velocity.x);
        }
        if (Mathf.Abs(distanceToCenter.y) > maxDistance.y) {
            newVelocity.y = (distanceToCenter.y < 0 ? velocity.y : -velocity.y);
        }
        rigidBody.velocity = newVelocity;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Invoke("RandomDrop", 0.5f);
        }
    }

    void RandomDrop() {
        if (Random.value <= dropProbability) {
            GameObject drop = dropItems[Random.Range(0, dropItems.Length)];
            Instantiate(drop, transform.position, transform.rotation);
        }
    }
}