using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private UIManager UI;
    
    public static int lifes = 3;
    public static int hearts = 3;
    public static int cherries = 0;
    public static int gems = 0;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UI = GameObject.Find("UI").GetComponent<UIManager>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            animator.SetTrigger("isHurt");
            if (hearts > 0) {
                hearts--;
            }
            if (hearts == 0) {
                animator.SetTrigger("isDeath");
                Invoke("KillPlayer", 0.75f);
            }
        }
        else if (other.gameObject.tag == "InstantKill") {
            hearts = 0;
            KillPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.enabled = false;
            rigidBody.velocity = PlayerController.jumpForce * Vector2.up;
            other.gameObject.GetComponent<Animator>().SetTrigger("isDeath");
            Destroy(other.gameObject, 0.75f);
        }
    }

    void KillPlayer() {
        gameObject.SetActive(false);
        UI.displayTryAgainScreen();
    }
}
