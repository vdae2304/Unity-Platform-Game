using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplossiveCrate : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        animator.SetTrigger("Destroy");
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 0.5f);
    }
}