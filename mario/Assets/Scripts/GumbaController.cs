using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumbaController : MonoBehaviour {
    [SerializeField]
    float speedMax = 1.0f;
    [SerializeField]
    bool isGrounded = true;
    bool isJump = false;
    [SerializeField]
    float jumpImpulse = 4.0f;
    Animator animCtrl;
    SpriteRenderer spr;
    Rigidbody2D rb2d;

    float horizontalInput = 0.0f;

    void Start() {
        animCtrl = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        
        horizontalInput = 1f;
    }

    void Update() {
        CheckGround();

        UpdateAnimation();
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(horizontalInput * speedMax, rb2d.velocity.y);
    }

    void UpdateAnimation() {
        animCtrl.SetFloat("speed", Mathf.Abs(horizontalInput));
        animCtrl.SetBool("isGrounded", isGrounded);

        if (horizontalInput != 0)
            animCtrl.SetFloat("direction", horizontalInput);
    }

    void CheckGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("waypoint01"))
            horizontalInput = 1f;
        
        if (collider.CompareTag("waypoint02")) {
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }
        
        if (collider.CompareTag("waypoint03"))
            horizontalInput = -1;
    }
}
