using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Transform player;
    private Vector3 initialPosition;

    public Transform path;
    private int currentPoint = 0;
    public float speed;
    public bool isMoving;
    public bool loop;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("player").transform;
        initialPosition = transform.position;
    }

    void FixedUpdate() {
        if (isMoving) {
            Transform target = path.GetChild(currentPoint);
            Vector2 direction = target.position - transform.position;
            if (direction.magnitude > 0.1) {
                rigidBody.velocity = speed * direction.normalized;
            }
            else if (currentPoint < path.childCount - 1) {
                currentPoint++;
            }
            else if (loop) {
                currentPoint = 0;
            }
            else {
                isMoving = false;
                rigidBody.velocity = Vector2.zero;
                Invoke("ResetPosition", 5f);
            }
        }
    }

    void ResetPosition() {
        player.SetParent(null);
        transform.position = initialPosition;
        currentPoint = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.transform.SetParent(transform);
            isMoving = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.transform.SetParent(null);
        }
    }
}