using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour {

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D collision) {
        CharacterHP character = collision.GetComponent<CharacterHP>();
        
        if (character != null) {
            character.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
