using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour {
    [SerializeField] [Range(0f, 1000f)] private float _health = 100f;
    [SerializeField] [Range(0f, 1000f)] private float _damage = 20f;
    [SerializeField] [Range(0, 10)] private int _maxCount = 10;
    [SerializeField] private GameObject dropObject = null;
    [SerializeField] private Rigidbody2D rigidbody2D = null;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddTorque(1000f * transform.localScale.x * transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.isTrigger && collision.collider.tag == "Player") {
            Main.Player.health -= _damage * CnControls.CnInputManager.GetAxis("Horizontal");
            Main.Player.health -= _damage * CnControls.CnInputManager.GetAxis("Vertical");
        }
    }

    public void Damage(float damage, GameObject obj) {
        _health -= damage;
        dropObject = obj;

        if (_health <= 0) {
            Drop();
            gameObject.SetActive(false);
        }
    }

    private void Drop() {
        int count = Random.Range(1, _maxCount);
        for (int i = 0; i < count; i++) {
            GameObject obj = Instantiate(dropObject, transform.position, transform.rotation) as GameObject;

            Vector2 forceVector = new Vector2(Random.value, Random.value);
            forceVector.Normalize();

            float force = Random.Range(1f, 10f);

            obj.GetComponent<Rigidbody2D>().AddForce(forceVector * force, ForceMode2D.Impulse);
        }
    }
}


