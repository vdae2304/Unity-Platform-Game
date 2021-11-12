using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeart : MonoBehaviour {

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !collected) {
            collected = true;
            if (PlayerController.hearts < 3) {
                PlayerController.hearts++;
            }            
            Destroy(gameObject);
        }
    }
}