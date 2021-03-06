using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public static int lifes = 3;
    public static int hearts = 3;
    public static int cherries = 0;
    public static int gems = 0;

    public float speed = 7.0f;
    public float jumpForce = 8.0f;

    private bool isOnGround = true;
    private bool isOnLadder = false;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Time.timeScale != 0) {
            Vector2 velocity = rigidBody.velocity;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0) {
                velocity.x = horizontalInput * speed;
                spriteRenderer.flipX = (velocity.x < 0);
            }
            animator.SetFloat("speed", Mathf.Abs(horizontalInput));
            if (Input.GetButtonDown("Jump") && isOnGround) {
                velocity.y = jumpForce;
                animator.SetBool("isJumping", true);
                AudioManager.PlayOneShot(AudioManager.jumpSound);
            }
            if (isOnLadder) {
                velocity.y = verticalInput * speed;
            }
            rigidBody.velocity = velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Ground":
                isOnGround = true;
                animator.SetBool("isJumping", false);
                break;
            case "Ladder":
                isOnLadder = true;
                animator.SetBool("isClimbing", true);
                break;
            case "Enemy":
                rigidBody.velocity = jumpForce * Vector2.up;
                break;
            case "Finish":
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(LevelLoader.displayLevelClearedScreen());
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Ground":
                isOnGround = false;
                break;
            case "Ladder":
                isOnLadder = false;
                animator.SetBool("isClimbing", false);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag) {
            case "Enemy":
            case "EnemyInvincible":
                animator.SetTrigger("isHurt");
                if (hearts > 0) {
                    hearts--;
                }
                if (hearts == 0) {
                    StartCoroutine(PlayerDeath());
                }
                break;
            case "InstantKill":
                StartCoroutine(PlayerDeath());
                break;
        }
    }

    IEnumerator PlayerDeath() {
        hearts = 0;
        animator.SetTrigger("isDeath");
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.75f);
        LevelLoader.displayTryAgainScreen("Has muerto");
    }
}