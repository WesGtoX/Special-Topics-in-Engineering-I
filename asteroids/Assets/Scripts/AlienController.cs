using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour {

    [SerializeField]
    float linearSpeed = 3f;
    Rigidbody2D rb2d;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform target;


    Vector3 bottomLeftLimit;
    Vector3 topRightLimit;

    [SerializeField]
    float shooterInterval = 0.2f;
    [SerializeField]
    int shooterCounter = 0;
    [SerializeField]
    float lastShooterTime = 0f;
    [SerializeField]
    float displayInterval = 5f;
    float lastSpawnTime = 0f;
    Vector3 startPoint;

    Camera cam;

    void Start() {
        // Definicoes e restauracao da posicao do alien inimigo
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        bottomLeftLimit = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        topRightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        startPoint = new Vector3(topRightLimit.x + 1.6f, topRightLimit.y - 1.5f, 0f);
        
        RespawnShip();
    }

    void Update() {
        if ((Time.time - lastSpawnTime) >= displayInterval) {
            ShowEnemy();
            lastSpawnTime = Time.time + 10f;
        }

        // Limitar a posicao do alien inimigo ate o final da area de visao
        if (transform.position.x <= -13f) {
            RespawnShip();
        }

        TargetShip();
    }

    void TargetShip() {
        if (transform.position.x < topRightLimit.x) {
            // Intervalo entre tiros por tempo
            if ((Time.time - lastShooterTime) >= shooterInterval) {
                lastShooterTime = Time.time + 0.2f;
                Quaternion targetDirection = Quaternion.FromToRotation(transform.up, target.position);
                Instantiate(bulletPrefab, transform.position, transform.rotation * targetDirection);
                shooterCounter++;
            }

            // Limitar intervalo de tiro do alien
            if (shooterCounter >= 50) {
                shooterCounter = 0;
                lastShooterTime = Time.time + 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("bullet")) {
            if (transform.position.x < topRightLimit.x) {
                Destroy(collider.gameObject);
                RespawnShip();
            }
        }
        
        if (collider.CompareTag("ship")) {
            RespawnShip();
        }
    }

    void ShowEnemy() {
        // Mostrar o alien inimigo em uma posicao Y aleatoria do canto direito
        float y = Random.Range(topRightLimit.y, bottomLeftLimit.y + 2f);
        Vector3 respawn = new Vector3(topRightLimit.x, y, 0f);
        rb2d.velocity = Vector2.left * linearSpeed;
        transform.rotation = Quaternion.identity;
        transform.position = respawn;
    }

    void RespawnShip() {
        // Restaurar a posicao inicial do alien inimigo para fora da area de visao
        transform.rotation = Quaternion.identity;
        transform.position = startPoint;
        rb2d.velocity = new Vector2(0f, 0f);
        lastSpawnTime = Time.time;
        shooterCounter = 0;
    }
}
