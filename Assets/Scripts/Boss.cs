using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public GameObject goal;
    private Transform player;
    private Vector3 initialPosition;

    public int lifes;
    public float speed;
    public float maxDistance;

    private string AttackMode = "AttackFromDistance";
    private float timeToChangeMode = 15.0f;

    public GameObject[] dropObjects;
    private bool canDropObject = false;
    private bool canDropEnemy = false;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        initialPosition = transform.position;
        Invoke("ResetCanDropObject", 1f);
        Invoke("ResetCanDropEnemy", 2f);
    }

    void Update() {
        timeToChangeMode -= Time.deltaTime;
        switch (AttackMode) {
            case "AttackFromDistance":
                AttackFromDistance();
                break;
            case "GoDownAndFollowPlayer":
                GoDownAndFollowPlayer();
                break;
            case "GoBackToInitialPosition":
                GoBackToInitialPosition();
                break;
        }
    }

    void AttackFromDistance() {
        if (timeToChangeMode > 0f) {
            float distanceToCenter = transform.position.x - initialPosition.x;
            if (Mathf.Abs(distanceToCenter) > maxDistance) {
                spriteRenderer.flipX = (distanceToCenter < 0);
            }
            float horizontalVelocity = spriteRenderer.flipX ? speed : -speed;
            rigidBody.velocity = new Vector2(horizontalVelocity, 0);
            if (canDropObject) {
                DropNewObject(distanceToCenter);
            }
        }
        else {
            AttackMode = "GoDownAndFollowPlayer";
            timeToChangeMode = 3f;
        }
    }

    void GoDownAndFollowPlayer() {
        if (timeToChangeMode > 0f) {
            Vector2 direction = player.position - transform.position;
            spriteRenderer.flipX = (direction.x > 0);
            rigidBody.velocity = speed * direction.normalized;
        }
        else {
            AttackMode = "GoBackToInitialPosition";
            timeToChangeMode = 3f;
        }
    }

    void GoBackToInitialPosition() {
        Vector2 direction = initialPosition - transform.position;
        if (direction.magnitude > 0.1f) {
            spriteRenderer.flipX = (direction.x > 0);
            rigidBody.velocity = speed * direction.normalized;
        }
        else {
            AttackMode = "AttackFromDistance";
            timeToChangeMode = 15f;
        }
    }

    void DropNewObject(float distanceToCenter) {
        GameObject drop = dropObjects[Random.Range(0, dropObjects.Length)];
        if (
            drop.tag != "Enemy" || 
            (canDropEnemy && Mathf.Abs(distanceToCenter) <= 0.6*maxDistance)
        ) {
            canDropObject = false;
            drop = Instantiate(drop, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), 
                                      drop.GetComponent<Collider2D>());
            Invoke("ResetCanDropObject", 2f);
            if (drop.tag == "Enemy") {
                canDropEnemy = false;
                Invoke("ResetCanDropEnemy", 9f);
                Destroy(drop, 9f);
            }
        }
    }

    void ResetCanDropObject() {
        canDropObject = true;
    }

    void ResetCanDropEnemy() {
        canDropEnemy = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (lifes > 0) {
                lifes--;
                animator.SetTrigger("isHurt");
                StartCoroutine(IgnoreDealtDamage(other));
            }
            if (lifes == 0) {
                animator.SetTrigger("isDeath");
                goal.SetActive(true);
                Destroy(gameObject, 0.5f);
            }
            other.gameObject.GetComponent<Rigidbody2D>().velocity = 
            PlayerController.jumpForce * Vector2.up;
        }
    }

    IEnumerator IgnoreDealtDamage(Collider2D other) {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, false);
    }
}