using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    private Animator animator;
    private Collider2D collider;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public float stepSize = 2.0f;
    public float jumpForce = 8.0f;
    
    public float dropProbability = 0.2f;
    public GameObject[] dropItems;

    void Start() {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Jump() {
        spriteRenderer.flipX = (stepSize > 0);
        animator.SetBool("isJumping", true);
        rigidBody.velocity = new Vector2(stepSize, jumpForce);
        stepSize *= -1;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            animator.SetBool("isJumping", false);
            Invoke("Jump", 5.0f);
        }
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