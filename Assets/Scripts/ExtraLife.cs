using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {

    private PlayerDamage player;

    void Start() {
        player = GameObject.Find("player").GetComponent<PlayerDamage>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            player.increaseNumberOfLifes(1);
            Destroy(gameObject);
        }
    }
}
