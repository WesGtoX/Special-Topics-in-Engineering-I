using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpaceship : MonoBehaviour {

    public float speed;

    private Transform spaceship;

    void Start() {
        spaceship = FindObjectOfType<SpaceshipController>().transform;
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, spaceship.position, speed * Time.deltaTime);
    }
}
