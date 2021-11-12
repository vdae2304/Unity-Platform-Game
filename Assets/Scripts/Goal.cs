using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public int gemsRequired = 0;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (PlayerController.gems >= gemsRequired) {
                AudioManager.Stop();
                AudioManager.PlayOneShot(AudioManager.stageClearSound);
                other.gameObject.GetComponent<Rigidbody2D>()
                                .constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("displayLevelClearedScreen", 7f);
            }
            else {
                LevelLoader.displayLevelUnclearedScreen();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (
            other.gameObject.tag == "Player" && 
            PlayerController.gems < gemsRequired
        ) {
            LevelLoader.displayLevelUnclearedScreen(false);
        }
    }

    void displayLevelClearedScreen() {
        LevelLoader.displayLevelClearedScreen();
    }
}