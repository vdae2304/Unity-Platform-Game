using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opposum : MonoBehaviour {

    private Animator animator;
    private Collider2D collider;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialPosition;

    public float speed = 2.0f;
    public float chasingSpeed = 3.0f;
    public float maxDistance;

    public float dropProbability = 0.2f;
    public GameObject[] dropItems;

    void Start() {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
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
        else if (distanceToPlayer < maxDistance) {
            spriteRenderer.flipX = (player.position.x > transform.position.x);
            velocity.x = chasingSpeed;
        }
        velocity.x = (spriteRenderer.flipX ? velocity.x : -velocity.x);
        rigidBody.velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            animator.SetTrigger("isDeath");
            collider.enabled = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("RandomDrop", 0.5f);
            Destroy(gameObject, 0.75f);
        }
    }

    void RandomDrop() {
        if (Random.value <= dropProbability) {
            GameObject drop = dropItems[Random.Range(0, dropItems.Length)];
            drop = Instantiate(drop, transform.position, transform.rotation);
            Destroy(drop, 10f);
        }
    }
}