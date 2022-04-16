using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour {
    Rigidbody2D rb2d;
    SpriteRenderer ammo;
    Camera cam;
    Vector3 bottomLeftLimit;
    Vector3 topRightLimit;
    float lastSpawnTime = 0f;
    [SerializeField]
    float spawnInterval = 15f;

    void Start() {
        cam = Camera.main;

        rb2d = GetComponent<Rigidbody2D>();
        ammo = GetComponent<SpriteRenderer>();

        //Get screen limits
        bottomLeftLimit = cam.ScreenToWorldPoint(new Vector3(2f, 2f, 0));
        topRightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        transform.position = new Vector3(topRightLimit.x, topRightLimit.y + 10f, 0f);
    }

    // Update is called once per frame
    void Update() {
        if ((Time.time - lastSpawnTime) >= spawnInterval) {
            lastSpawnTime = Time.time;
            SpawnBulletReloader();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("ship")) {
            // Destroy(gameObject);
            transform.position = new Vector3(topRightLimit.x, topRightLimit.y + 10f, 0f);
        }
    }

    void SpawnBulletReloader() {
        float x = Random.Range(bottomLeftLimit.x, topRightLimit.x);
        float y = Random.Range(bottomLeftLimit.y, topRightLimit.y - 2f);
        transform.position = new Vector3(x, y, 0f);
    }
}
