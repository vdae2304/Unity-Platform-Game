using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialogue : MonoBehaviour {

    public Canvas canvas;

    void Start() {
        canvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "player") {
            canvas.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.name == "player") {
            canvas.enabled = false;
        }
    }
}
