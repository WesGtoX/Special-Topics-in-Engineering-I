using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float destroyTime;
    
    void Start() {
        Destroy(gameObject, destroyTime);
    }
}
