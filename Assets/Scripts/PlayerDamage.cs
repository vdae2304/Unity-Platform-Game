using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private UIManager UI;
    
    public static int lifes = 3;
    public static int hearts = 3;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UI = GameObject.Find("UI").GetComponent<UIManager>();
    }

    void Update() {
        if (Time.deltaTime != 0 && transform.position.y < -10) {
            UI.displayTryAgainScreen();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            animator.SetTrigger("isHurt");
            increaseNumberOfHearts(-1);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.enabled = false;
            rigidBody.velocity = PlayerController.jumpForce * Vector2.up;
            other.gameObject.GetComponent<Animator>().SetTrigger("isDeath");
            Destroy(other.gameObject, 0.5f);
        }
    }

    public void increaseNumberOfHearts(int value) {
        hearts = Mathf.Clamp(hearts + value, 0, 3);
        UI.updateNumberOfHearts(hearts);
        if (hearts == 0) {
            UI.displayTryAgainScreen();
        }
    }

    public void increaseNumberOfLifes(int value, bool resetHearts = true) {
        if (resetHearts) {
            hearts = 3;
            UI.updateNumberOfHearts(hearts);
        }
        lifes = Mathf.Max(lifes + value, 0);
        UI.updateNumberOfLifes(lifes);
    }
}
