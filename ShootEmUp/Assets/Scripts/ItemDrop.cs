using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {
    public ItemEffect effect;

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player != null) {
            player.SetItemEffect(effect);
            Destroy(gameObject);
        }
    }
}
