using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour {

    [SerializeField]
    VirtualButton dashButton;
    [SerializeField]
    VirtualJoyStick joyStick;

    [SerializeField]
    public float speed;
    public float fireRate;
    public float spawnTime;
    public float invencibilityTime;
    public int fireLevel = 1;
    public int lives = 3;

    public Boundary boundary;
    public GameObject bullet;
    public Transform[] shotSpawns;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private Vector3 startPosition;
    private CharacterHP characterHP;

    private float nextFire;
    private bool isDead = false;

    void Start() {
        characterHP = GetComponent<CharacterHP>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    void Update() {
        if (!isDead && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            if (fireLevel >= 1) {
                Instantiate(bullet, shotSpawns[0].position, shotSpawns[0].rotation);
            }

            if (fireLevel >= 2) {
                Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation);
                Instantiate(bullet, shotSpawns[2].position, shotSpawns[2].rotation);
            }

            if (fireLevel >= 3) {
                Instantiate(bullet, shotSpawns[3].position, shotSpawns[4].rotation);
                Instantiate(bullet, shotSpawns[4].position, shotSpawns[4].rotation);
            }
        }
    }

    void FixedUpdate() {
        Vector2 movement = new Vector2(joyStick.GetHorizontal(), joyStick.GetVertical());
        rb2d.velocity = movement * speed;
        rb2d.position = new Vector2(
            Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax)
        );
    }

    public void Respawn() {
        lives--;
        
        if (lives > 0) {
            StartCoroutine(Spawning());
        } else {
            lives = 0;
            isDead = true;
            sprite.enabled = false;
            fireLevel = 0;
            LevelController.levelController.GameOver();
        }

        LevelController.levelController.SetLivesText(lives);
    }

    IEnumerator Spawning() {
        isDead = true;
        sprite.enabled = false;
        fireLevel = 1;
        gameObject.layer = 9;
        yield return new WaitForSeconds(spawnTime);
        isDead = false;
        transform.position = startPosition;

        for (float i = 0; i < invencibilityTime; i+= 0.1f) {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.layer = 6;
        sprite.enabled = true;
        fireLevel = 1;
        characterHP.isDead = false;
    }
}
