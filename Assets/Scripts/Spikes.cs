using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Boss") {
            animator.SetTrigger("Destroy");
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(this.gameObject, 0.5f);
        }
    }
}