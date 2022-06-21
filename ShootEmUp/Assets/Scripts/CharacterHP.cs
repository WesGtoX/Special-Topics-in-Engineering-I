using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : MonoBehaviour {
    
    public GameObject explosion;
    public Color damageColor;

    public int health;
    public int scorePoint;

    [HideInInspector]
    public bool isDead = false;
    private SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage) {
        if (!isDead) {
            health -= damage;

            if (health <= 0) {
                isDead = true;
                Instantiate(explosion, transform.position, transform.rotation);

                if (this.GetComponent<Player>() != null) {
                    GetComponent<Player>().Respawn();
                } else {
                    LevelController.levelController.SetScore(scorePoint);
                    Destroy(gameObject);
                }
            } else {
                StartCoroutine(TakingDamage());
            }
        }
    }

    IEnumerator TakingDamage() {
        sprite.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
