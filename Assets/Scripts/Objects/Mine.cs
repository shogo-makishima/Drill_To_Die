using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {
    [SerializeField] [Range(0f, 100f)] private float _damage = 10;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.isTrigger && collision.collider.tag == "Player") {
            Main.Player.health -= _damage;
            Destroy(gameObject);
        }
    }
}