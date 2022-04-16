using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    
    [SerializeField]
    Transform fireSpotTrans;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float linearSpeed = 5f;
    [SerializeField]
    float angularSpeed = 2f;
    // [SerializeField]
    // Transform targetTrans;

    [SerializeField]
    int bulletLimit = 0;
    [SerializeField]
    int shipDamage = 0;

    Rigidbody2D rb2d;
    SpriteRenderer spr;

    Vector3 bottomLeftLimit;
    Vector3 topRightLimit;
    Vector3 shipRespawn = new Vector3(0, 0, 0);
    
    Camera cam;

    void Start() {
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
       
        cam = Camera.main;
        bottomLeftLimit = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        topRightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update() {
        // Instanciar um nova bala com um posicao e orientacao especificas
        // O que vou instanciar? Como obter posicao e orientacao?
        // Clone de Bullet Prefab. Obtenho posicao e orientacao atraves do fireSpot

        CheckCamLimits();

        // Debug.DrawLine(fireSpotrans.position, targetTrans.position, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && bulletLimit < 10) {
            Instantiate(bulletPrefab, fireSpotTrans.position, fireSpotTrans.rotation);
            // Vector3 dirPlayerTarget = targetTrans.position - fireSpotTrans.position;
            // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, dirPlayerTarget);
            // Instantiate(bulletPrefab, fireSpotTrans.position, rotation);

            bulletLimit += 1;
        }

        // Mudanca de color em spriteRender em acordo comentrada do usurio/jogador
        if (Input.GetKeyDown(KeyCode.R))
            spr.color = Color.red;
        else if (Input.GetKeyDown(KeyCode.G))
            spr.color = Color.green;
        else if (Input.GetKeyDown(KeyCode.B))
            spr.color = Color.blue;

        /*if (Input.GetKeyDown(KeyCode.Space))
            rb2d.AddForce(Vector2.up * 300f); // new Vector2(0f, 1f) * 300f
        */
    }

    // FixedUpdate: Funcao chamada um unica vez em toda atualizacao do motor
    // de fisica. Ou seja, FixedUpdate e sincronizado com o motor de fisica]
    // Recomenda-se que a "comunicacao" continua com o motor de fisica
    // (atraves do RigidBody2d) seja feita no FixedUpdate
    void FixedUpdate() {
        if(Input.GetKey(KeyCode.UpArrow)) {
            //rb2d.AddRelativeForce(Vector2.up * linearSpeed);
            rb2d.AddForce(transform.up * linearSpeed);
        }

        rb2d.AddTorque(-angularSpeed * Input.GetAxisRaw("Horizontal"));
    }

    void CheckCamLimits() {
        if (transform.position.y > topRightLimit.y)
            transform.position = new Vector3(transform.position.x, bottomLeftLimit.y, 0f);

        else if (transform.position.y < bottomLeftLimit.y)
            transform.position = new Vector3(transform.position.x, topRightLimit.y, 0f);

        else if (transform.position.x > topRightLimit.x)
            transform.position = new Vector3(bottomLeftLimit.x, transform.position.y, 0f);

        else if (transform.position.x < bottomLeftLimit.x)
            transform.position = new Vector3(topRightLimit.x, transform.position.y, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("asteroid")) {
            Destroy(collision.gameObject);
            
            if (shipDamage < 3) {
                transform.position = shipRespawn;
                // Destroy(gameObject);
                shipDamage = 0;
            } else {
                shipDamage += 1;
            }
        }
        if(collision.CompareTag("ammo")) {
            bulletLimit -= 5;
            if (bulletLimit < 0) {
                bulletLimit = 0;
            }
        }
    }

    /*void OnCollisionEnter2D(Collision2D collision) {
        Destroy(collision.collider.gameObject);
    }*/
}
