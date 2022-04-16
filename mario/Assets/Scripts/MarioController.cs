using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {
    [SerializeField]
    float speedMax = 5.0f;
    [SerializeField]
    bool isGrounded = false;
    [SerializeField]
    float jumpImpulse = 7.0f;
    Animator animCtrl;
    SpriteRenderer spr;
    Rigidbody2D rb2d;

    float horizontalInput = 0.0f;

    void Start() {
        animCtrl = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);

        horizontalInput = Input.GetAxisRaw("Horizontal");
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

        // Apenas para o caso de utilização de animações para a direita
        // if (horizontalInput > 0) {
        //     spr.flipX = false;
        // } else if(horizontalInput < 0) {
        //     spr.flipX = true;
        // }
    }

    void CheckGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
        // Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);
    }
}
