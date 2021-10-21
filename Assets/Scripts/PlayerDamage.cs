using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private UIManager ui;
    private Vector3 initialPosition;
    
    public int lives = 3;
    public int hearts = 3;
    private float jumpForce;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ui = GameObject.Find("UI").GetComponent<UIManager>();
        initialPosition = transform.position;
        jumpForce = GetComponent<PlayerController>().jumpForce;
    }

    void Update() {
        if (transform.position.y < -10) {
            StartCoroutine(DecreaseLivesByOne());
        }
    }

    void DecreaseHeartsByOne() {
        ui.updateNumberOfHearts(--hearts);
        if (hearts == 0) {
            StartCoroutine(DecreaseLivesByOne());
        }
    }

    IEnumerator DecreaseLivesByOne() {
        spriteRenderer.enabled = false;
        transform.position = initialPosition;
        yield return new WaitForSeconds(1.0f);

        hearts = 3;
        spriteRenderer.enabled = true;
        ui.updateNumberOfHearts(hearts);
        ui.updateNumberOfLives(--lives);
        if (lives == 0) {
            Debug.Log("Game Over");
            Application.Quit();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Vector2 bounce = other.transform.position - transform.position;
            animator.SetTrigger("isHurt");
            rigidBody.AddForce(jumpForce*bounce.normalized,ForceMode2D.Impulse);
            DecreaseHeartsByOne();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "ExtraLive") {
            ui.updateNumberOfLives(++lives);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy") {
            other.enabled = false;
            rigidBody.velocity = jumpForce * Vector2.up;
            other.gameObject.GetComponent<Animator>().SetTrigger("isDeath");
            Destroy(other.gameObject, 0.5f);
        }
    }
}
