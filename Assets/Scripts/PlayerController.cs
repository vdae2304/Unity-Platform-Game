using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public static float speed = 7.0f;
    public static float jumpForce = 8.0f;
    private bool isOnGround = true;
    private bool isOnLadder = false;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Time.timeScale != 0) {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 velocity = rigidBody.velocity;
            velocity.x = horizontalInput * speed;
            if (isOnLadder) {
                velocity.y = verticalInput * speed;
            }
            
            if (horizontalInput != 0) {
                animator.SetBool("isRunning", true);
                spriteRenderer.flipX = (horizontalInput < 0);
            }
            else {
                animator.SetBool("isRunning", false);
            }
            if (Input.GetButtonDown("Jump") && isOnGround) {
                velocity.y = jumpForce;
                animator.SetBool("isJumping", true);
                AudioManager.audioSource.PlayOneShot(AudioManager.jump, 0.7f);
            }
            
            rigidBody.velocity = velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground") {
            isOnGround = true;
            animator.SetBool("isJumping", false);
        }
        else if (other.gameObject.tag == "Ladder") {
            isOnLadder = true;
            animator.SetBool("isClimbing", true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Ground") {
            isOnGround = false;
        }
        else if (other.tag == "Ladder") {
            isOnLadder = false;
            animator.SetBool("isClimbing", false);
        }
    }
}
