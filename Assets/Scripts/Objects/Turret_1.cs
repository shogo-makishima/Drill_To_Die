using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_1 : MonoBehaviour {
    [SerializeField] [Range(0f, 1000f)] private float _health = 100;
    [SerializeField] [Range(0, 10)] private int _maxCount = 10;
    [SerializeField] [Range(0f, 100f)] private float _bulletForce = 10;
    [SerializeField] [Range(0f, 3f)] private float _pause = 0.2f;
    [SerializeField] private GameObject dropObject = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject[] _bullets = null;
    [SerializeField] private Transform[] _spawnPoints = null;
    [SerializeField] private bool _isCanShoot = true;

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

    public void AI() {
        Debug.Log("AI");
        if (player) {
            if (_isCanShoot) StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {
        _isCanShoot = false;
        for (int i = 0; i < _spawnPoints.Length; i++) {
            GameObject Bullet = Instantiate(_bullets[i], _spawnPoints[i].position, _spawnPoints[i].rotation) as GameObject;
            Bullet.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[i].right * _bulletForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(_pause);
        _isCanShoot = true;
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (!collider.isTrigger && collider.tag == "Player") {
            player = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (!collider.isTrigger && collider.tag == "Player") {
            player = null;
        }
    }
}
