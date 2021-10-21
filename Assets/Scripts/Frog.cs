using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public float stepSize = 2.0f;
    public float jumpForce = 8.0f;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            StartCoroutine(Jump(stepSize));
            stepSize *= -1;
        }
    }

    IEnumerator Jump(float distance) {
        yield return new WaitForSeconds(5.0f);
        spriteRenderer.flipX = (distance > 0);
        animator.SetBool("jump", true);
        rigidBody.velocity = new Vector2(distance, jumpForce);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("jump", false);
    }
}
