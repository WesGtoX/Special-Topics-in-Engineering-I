using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
    
    [SerializeField]
    private float minSpeed = 2.0f;
    [SerializeField]
    private float maxSpeed = 6.0f;

    Vector3 bottomLeftLimit;
    Vector3 topRightLimit;
    Camera cam;

    void Start() {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        float speed = Random.Range(minSpeed, maxSpeed);
        
        // Calculo de direcao aleatoria
        // Para calcular, calculo o componente X aleatorio [-1, 1] e
        // calculo o componente y aleatorio [-1, 1]
        float dirX = Random.Range(-1.0f, 1.0f);
        float dirY = Random.Range(-1.0f, 1.0f);

        // Crio então, direcao aleatoria
        // Para ter controle da intensidade da velocidade, 
        Vector2 direction = new Vector2(dirX, dirY);
        // A direcao tera que ter tamanho unitario.
        // Devido a isso utilizamos o método Vector2::Normalize()
        direction.Normalize();

        rb2D.velocity = direction * speed;

        cam = Camera.main;
        bottomLeftLimit = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        topRightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update() {
        CheckCamLimits();
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
        
        // apenas serei destruido se o que me tocou for da categoria (TAG) bullet
        if(collision.CompareTag("bullet")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
