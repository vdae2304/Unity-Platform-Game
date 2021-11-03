using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialPosition;

    public int lifes;
    public float speed;
    public float maxDistance;

    private string AttackMode = "FromDistance";
    private bool canSufferDamage = true;
    private bool canDropObject = true;
    private float timeToChangeMode = 15.0f;

    public float probabilityToDropEnemy = 0.01f;
    public GameObject[] dropObjects;
    public GameObject[] dropEnemies;
    private GameObject objectDropped = null;
    private GameObject enemyDropped = null;

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
        if (canDropObject) {
            DropNewObject(distanceToCenter);
        }
        if (timeToChangeMode <= 0f) {
            AttackMode = "GoDown";
            timeToChangeMode = 3f;
        }
    }

    void DropNewObject(float distanceToCenter) {
        if (
            enemyDropped == null && 
            Mathf.Abs(distanceToCenter) <= 0.5*maxDistance &&
            Random.value <= probabilityToDropEnemy
        ) {
            enemyDropped = dropEnemies[Random.Range(0, dropEnemies.Length)];
            enemyDropped = Instantiate(enemyDropped, 
                                       transform.position + Vector3.down, 
                                       transform.rotation);
            canDropObject = false;
            Invoke("ResetCanDropObject", 2f);
            Destroy(enemyDropped, 9f);
        }
        else if (objectDropped == null) {
            objectDropped = dropObjects[Random.Range(0, dropObjects.Length)];
            objectDropped = Instantiate(objectDropped, 
                                       transform.position + Vector3.down, 
                                       transform.rotation);
            canDropObject = false;
            Invoke("ResetCanDropObject", 1f);
        }
    }

    void ResetCanDropObject() {
        canDropObject = true;
    }

    void GoDownAndFollowPlayer() {
        Vector2 direction = player.transform.position - transform.position;
        spriteRenderer.flipX = (direction.x > 0);
        rigidBody.velocity = speed * direction.normalized;
        if (timeToChangeMode <= 0f) {
            AttackMode = "GoBack";
            timeToChangeMode = 3f;
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
        if (other.gameObject.tag == "Player" && canSufferDamage) {
            if (lifes > 0) {
                lifes--;
                animator.SetTrigger("isHurt");
                canSufferDamage = false;
                Invoke("ResetCanSufferDamage", 2f);
            }
            if (lifes == 0) {
                animator.SetTrigger("isDeath");
                Destroy(this.gameObject, 0.5f);
            }
            other.gameObject.GetComponent<Rigidbody2D>().velocity = 
            PlayerController.jumpForce*Vector2.up;
        }
    }

    void ResetCanSufferDamage() {
        canSufferDamage = true;
    }
}