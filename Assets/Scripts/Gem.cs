using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    private Animator animator;
    private bool collected = false;

    void Start() {
        animator = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !collected) {
            collected = true;
            PlayerScoring.gems++;
            animator.SetTrigger("collected");
            Destroy(gameObject, 0.4f);
        }
    }
}
