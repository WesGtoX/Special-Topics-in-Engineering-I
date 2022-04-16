using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerTransform : MonoBehaviour {
    
    [SerializeField]
    private float speed = 5.0f;
    
    // Start e chamado (por instancia) no momento da criacao da
    // componente (instancia) => isso e ligado com a criacao do objeto de jogo
    // uma unica vez e antes do primeiro update
    void Start() {
        print(gameObject.name);

        // Desaloca o objeto de jogo quem contem uma instancia de ShipController
        // 5 segundos apas o Start ser chamado
        // Destroy(gameObject, 5f);

        // transform variavel com a referncia da instancia da componente Transform
        // que reside no mesmo objeto de jogo que essa instancia reside.
        // transform ja esta devidamente incializado
        // transform => posicao, rotacao e escala

        // Teletransporte para a posicao (x, y, z) = (5, 4, 0)
        transform.position = new Vector3(5f, 4f, 0f);

        // Metodo tranlate � um deslocamento (em relacao a posicao atual)
        // transform.Translate(-3f, -3f, 0f);
    }

    // Update e chamado um �nica vez em toda atualizacao de quadro
    // sincronizado com o motor de renderizacao (desenho)
    void Update() {
        // print(Input.GetKeyDown(KeyCode.RightArrow));

        // deltaS = Speed * deltaT
        float deltaS = speed * Time.deltaTime;

        /*if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(deltaS, 0f, 0f);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(-deltaS, 0f, 0f);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(0f, -deltaS, 0f);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(0f, deltaS, 0f);*/

        transform.Translate(Input.GetAxis("Horizontal") * deltaS, Input.GetAxis("Vertical") * deltaS, 0f);
    }
}
