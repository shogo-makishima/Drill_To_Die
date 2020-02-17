using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] [Range(0f, 100f)] private float _damage = 10f;
    [SerializeField] [Range(0f, 100f)] private float _lifeTime = 1f;

    private void Update() {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.isTrigger && collision.collider.tag == "Player")
            Main.Player.health -= _damage;
        if (!collision.collider.isTrigger && collision.collider.tag == "Object")
            collision.collider.gameObject.GetComponent<ItemOnScene>().Damage(_damage);
        if (!collision.collider.isTrigger)
            Destroy(gameObject);
    }
}
