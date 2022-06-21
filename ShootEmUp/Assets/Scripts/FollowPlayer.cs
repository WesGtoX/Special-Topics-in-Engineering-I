using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float speed;

    private Transform spaceship;

    void Start() {
        spaceship = FindObjectOfType<Player>().transform;
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, spaceship.position, speed * Time.deltaTime);
    }
}
