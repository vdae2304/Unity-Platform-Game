using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    
    private AudioSource audioSource;
    private AudioClip jumpSound;

    public float speed = 7.0f;
    public float jumpForce = 8.0f;
    private bool isOnGround = true;
    private bool isOnLadder = false;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        jumpSound = Resources.Load<AudioClip>("Audio/smrpg_jump");
    }

    void Update() {
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
        if (Input.GetKeyDown("space") && isOnGround) {
            velocity.y = jumpForce;
            animator.SetBool("jump", true);
            audioSource.PlayOneShot(jumpSound, 0.7f);
        }
        
        rigidBody.velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground") {
            isOnGround = true;
            animator.SetBool("jump", false);
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
