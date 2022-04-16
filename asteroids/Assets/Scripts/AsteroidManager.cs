using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    
    [SerializeField]
    GameObject asteroidPrefab;
    [SerializeField]
    float spawnInterval = 5f;

    Vector3 bottomLeftLimit;
    Vector3 topRighttLimit;

    Camera cam;
    
    // float lastSpawnTime = 0f;
    float timeCounter = 0f;

    void Start() {
        // Camera.main vai ter a referencia da primeira camera encontrada com a Tag MainCam
        cam = Camera.main;

        // Conversao ponto em coordenada do dispositivo grafico (screen) para ponto equivalente no mundo
        bottomLeftLimit = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        topRighttLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // print(bottomLeftLimit);
        // print(topRighttLimit);
    }

    void Update() {
        // Spawn feito em relacao ao tempo do ultmo spawn comparo
        // esse tempo atual se a diferenca for maior que o
        // spawnInterval, spawno e atualizo o tempo do ultimo spwn
        /* if ((Time.time - lastSpawnTime) >= spawnInterval) {
            Instantiate(asteroidPrefab, Vector3.zero, Quaternion.identity);
            lastSpawnTime = Time.time;
        } */

        // utilizamos um contador de tempo. Ele acumula o deltaTime,
        // que é o tempo passado entre dois quadros (update) quando
        // o valor do contador passa spawnInterval spawno e zero o contador
        timeCounter += Time.deltaTime;
        if (timeCounter >= spawnInterval) {
            SpawnAsteroid();
            timeCounter = 0f;
        }
    }

    void SpawnAsteroid() {
        // sorteio uma posicao X aleatoria entre os limites da esquerda e direita
        float posX = Random.Range(bottomLeftLimit.x, topRighttLimit.x);

        // sorteio uma posicao Y aleatoria entre os limites de cima e baixo
        float posY = Random.Range(bottomLeftLimit.y, topRighttLimit.y);
        
        Instantiate(asteroidPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
    }
}
