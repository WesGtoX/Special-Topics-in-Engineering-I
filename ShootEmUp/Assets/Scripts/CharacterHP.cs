using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : MonoBehaviour {
    
    public GameObject explosion;
    public GameObject[] dropItems;
    public Color damageColor;

    public int health;
    public int scorePoint;

    [HideInInspector]
    public bool isDead = false;
    private SpriteRenderer sprite;
    private static int chanceToDropItem = 0;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage) {
        if (!isDead) {
            health -= damage;

            if (health <= 0) {
                isDead = true;

                if (explosion != null)
                    Instantiate(explosion, transform.position, transform.rotation);

                if (this.GetComponent<Player>() != null) {
                    GetComponent<Player>().Respawn();
                } else {
                    chanceToDropItem++;
                    int random = Random.Range(0, 100);
                    
                    if (random < chanceToDropItem && dropItems.Length > 0) {
                        Instantiate(dropItems[Random.Range(0, dropItems.Length)], transform.position, Quaternion.identity);
                        chanceToDropItem = 0;
                    }
                    
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
