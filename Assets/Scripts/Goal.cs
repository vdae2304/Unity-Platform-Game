using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    LevelLoader levelLoader;

    void Start() {
        levelLoader = GameObject.Find("LevelLoader")
                                .GetComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (PlayerScoring.gems >= 3) {
                levelLoader.displayLevelClearedScreen();
            }
            else {
                levelLoader.displayLevelUnclearedScreen();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && PlayerScoring.gems < 3) {
            levelLoader.displayLevelUnclearedScreen(false);
        }
    }
}
