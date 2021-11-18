using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform player;
    public float minX = 0;
    public float maxX = Mathf.Infinity;
    public float minY = 0;
    public float maxY = Mathf.Infinity;

    void LateUpdate() {
        if (player != null) {
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(player.position.x, minX, maxX);
            position.y = Mathf.Clamp(player.position.y, minY, maxY);
            transform.position = position;
        }
    }
}