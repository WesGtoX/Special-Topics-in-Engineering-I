using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {
    [SerializeField]
    float speedMax = 5.0f;
    [SerializeField]
    bool isGrounded = true;
    bool isRunning = false;
    bool isJump = false;
    bool isCrouched = false;
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

        horizontalInput = 1f;
    }

    void Update() {
        CheckGround();

        isJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (isJump && isGrounded == true)
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);

        isCrouched = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        if (!isCrouched)
            horizontalInput = Input.GetAxisRaw("Horizontal");

        isRunning = horizontalInput != 0 && Input.GetKey(KeyCode.LeftShift);
        UpdateAnimation();
    }

    void FixedUpdate() {
        float speedRun = isRunning ? 1.5f : 1f;
        rb2d.velocity = new Vector2(horizontalInput * (speedMax * speedRun), rb2d.velocity.y);
    }

    void UpdateAnimation() {
        animCtrl.SetFloat("speed", Mathf.Abs(horizontalInput));
        animCtrl.SetBool("isGrounded", isGrounded);
        animCtrl.SetBool("isRunning", isRunning);
        animCtrl.SetBool("isCrouched", isCrouched);

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
