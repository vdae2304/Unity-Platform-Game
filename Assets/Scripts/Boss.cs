using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialPosition;

    public int lifes = 3;
    public float speed;
    public float maxDistance;

    public GameObject[] dropEnemies;
    private GameObject lastDropped = null;

    private string AttackMode = "FromDistance";
    private float timeToChangeMode = 15.0f;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        initialPosition = transform.position;
        rigidBody.velocity = speed * Vector3.left;
    }

    void Update() {
        timeToChangeMode -= Time.deltaTime;
        switch (AttackMode) {
            case "FromDistance":
                AttackFromDistance();
                break;
            case "GoDown":
                GoDownAndFollowPlayer();
                break;
            case "GoBack":
                GoBackToInitialPosition();
                break;
        }
    }

    void AttackFromDistance() {
        Vector2 newVelocity = rigidBody.velocity;
        float distanceToCenter = transform.position.x - initialPosition.x;

        if (Mathf.Abs(distanceToCenter) > maxDistance) {
            spriteRenderer.flipX = (distanceToCenter < 0);
            newVelocity.x = (spriteRenderer.flipX ? speed : -speed);
        }
        rigidBody.velocity = newVelocity;

        if (lastDropped == null) {
            lastDropped = dropEnemies[Random.Range(0, dropEnemies.Length)];
            lastDropped = Instantiate(lastDropped,
                                      transform.position + Vector3.down, 
                                      transform.rotation);
            if (lastDropped != null) {
                Destroy(lastDropped, 10.0f);
            }
        }

        if (timeToChangeMode <= 0f) {
            AttackMode = "GoDown";
            timeToChangeMode = 5f;
        }
    }

    void GoDownAndFollowPlayer() {
        Vector2 direction = player.transform.position - transform.position;
        spriteRenderer.flipX = (direction.x > 0);
        rigidBody.velocity = speed * direction.normalized;
        if (timeToChangeMode <= 0f) {
            AttackMode = "GoBack";
        }
    }

    void GoBackToInitialPosition() {
        Vector2 direction = initialPosition - transform.position;
        if (direction.magnitude < 0.1f) {
            AttackMode = "FromDistance";
            direction = Vector3.left;
            timeToChangeMode = 15f;
        }
        spriteRenderer.flipX = (direction.x > 0);
        rigidBody.velocity = speed * direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (lifes > 0) {
                lifes--;
                animator.SetTrigger("isHurt");
                other.gameObject.GetComponent<Rigidbody2D>().velocity = 
                PlayerController.jumpForce*Vector2.up;
            }
            if (lifes == 0) {
                animator.SetTrigger("isDeath");
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}