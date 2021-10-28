using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public float stepSize = 2.0f;
    public float jumpForce = 8.0f;
    
    public float dropProbability = 0.1f;
    public GameObject[] dropItems;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            animator.SetBool("isJumping", false);
            Invoke("Jump", 5.0f);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Invoke("RandomDrop", 0.5f);
        }
    }

    void Jump() {
        spriteRenderer.flipX = (stepSize > 0);
        animator.SetBool("isJumping", true);
        rigidBody.velocity = new Vector2(stepSize, jumpForce);
        stepSize *= -1;
    }

    void RandomDrop() {
        if (Random.value <= dropProbability) {
            GameObject drop = dropItems[Random.Range(0, dropItems.Length)];
            Instantiate(drop, transform.position, transform.rotation);
        }
    }
}
