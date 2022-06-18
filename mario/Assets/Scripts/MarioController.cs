using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioController : MonoBehaviour {
    [SerializeField]
    float speedMax = 5.0f;
    [SerializeField]
    float jumpImpulse = 7.0f;
    [SerializeField]
    float damageForce = 6f;
    float damageTimeCounter = 0f;
    float damageTime = 1f;
    
    int coinCounter = 0;
    int life = 3;
    
    [SerializeField]
    bool isGrounded = true;
    bool isRunning = false;
    bool isJump = false;
    bool isCrouched = false;
    bool isDamaged = false;

    [SerializeField]
    VirtualButton jumpButton;
    [SerializeField]
    VirtualJoyStick joyStick;
    
    [SerializeField]
    // Text coinCounterText;
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

        // isJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        isJump = jumpButton.GetButtonDown() || Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (isJump && isGrounded == true)
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);

        isCrouched = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        if (!isCrouched)
            horizontalInput = Input.GetAxisRaw("Horizontal");
            // horizontalInput = joyStick.GetHorizontal();

        isRunning = horizontalInput != 0 && Input.GetKey(KeyCode.LeftShift);
        UpdateAnimation();

        CheckDamagedState();
    }

    void FixedUpdate() {
        if (!isDamaged) {
            float speedRun = isRunning ? 1.5f : 1f;
            rb2d.velocity = new Vector2(horizontalInput * (speedMax * speedRun), rb2d.velocity.y);
        }
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

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("coin")) {
            coinCounter++;
            Destroy(collision.gameObject);
            // coinCounterText.text = coinCounter.ToString();
        } else if (collision.CompareTag("enemy")) {
            float marioPosX = transform.position.x;
            float enemyPosX = collision.transform.position.x;
            bool isLeft = (marioPosX >= enemyPosX);
            TakeDamage(isLeft);
        }
    }

    void TakeDamage(bool isLeft) {
        isDamaged = true;
        
        Vector2 directionForce = isLeft ?new Vector2(1f, 1f) : new Vector2(-1f, 1f);
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.AddForce(directionForce * damageForce, ForceMode2D.Impulse);

        life--;
        if (life < 0) {
            UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }
    }

    void CheckDamagedState() {
        if (isDamaged) {
            damageTimeCounter += Time.deltaTime;
            
            if (damageTimeCounter >= damageTime) {
                isDamaged = false;
                damageTimeCounter = 0f;
            }
        }
    }
}
