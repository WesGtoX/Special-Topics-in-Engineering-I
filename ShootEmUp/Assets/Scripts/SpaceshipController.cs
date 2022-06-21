using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, yMin, yMax;
}

public class SpaceshipController : MonoBehaviour {

    [SerializeField]
    VirtualButton dashButton;
    [SerializeField]
    VirtualJoyStick joyStick;

    [SerializeField]
    public float speed;
    public float fireRate;
    public int fireLevel = 1;

    public Boundary boundary;
    public GameObject bullet;
    public Transform[] shotSpawns;

    private Animator animCtrl;
    private Rigidbody2D rb2d;
    private float nextFire;

    void Start() {
        animCtrl = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Time.time > nextFire) {
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

        //UpdateAnimation();
    }

    void FixedUpdate() {
        Vector2 movement = new Vector2(joyStick.GetHorizontal(), joyStick.GetVertical());
        rb2d.velocity = movement * speed;
        rb2d.position = new Vector2(
            Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax)
        );
    }

    void UpdateAnimation() {

    }
}
