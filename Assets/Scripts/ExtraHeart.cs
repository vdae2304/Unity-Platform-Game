using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeart : MonoBehaviour {

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !collected) {
            collected = true;
            if (PlayerScoring.hearts < 3) {
                PlayerScoring.hearts++;
            }            
            Destroy(gameObject);
        }
    }
}
