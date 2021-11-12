using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour {

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !collected) {
            collected = true;
            PlayerController.cherries++;
            Destroy(gameObject);
        }
    }
}