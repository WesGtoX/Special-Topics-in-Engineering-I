using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    float forceIntensity = 100.0f;

    void Start() {
        Rigidbody2D rb2d;
        rb2d = GetComponent<Rigidbody2D>();

        // Adiciono um forca (qdo a bala e criada) 
        // na direcao UP local (nosso pra frente local)
        rb2d.AddForce(transform.up * forceIntensity);

        // Agendo a destruicao do objeto (e suas compoenentes) para daqui 5 segundos
        Destroy(gameObject, 5.0f);
    }
}
