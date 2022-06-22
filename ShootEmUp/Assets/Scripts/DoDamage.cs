using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour {

    public int damage = 1;

    public bool destroyByContact = true;
    public bool destroyShots = false;

    void OnTriggerEnter2D(Collider2D collision) {
        CharacterHP character = collision.GetComponent<CharacterHP>();
        
        if (character != null) {
            character.TakeDamage(damage);
            
            if (destroyByContact)
                Destroy(gameObject);
        }

        DoDamage shot = collision.GetComponent<DoDamage>();
        if (shot != null && destroyShots)
            Destroy(collision.gameObject);
    }
}
