using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrill : MonoBehaviour {
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private LayerMask layerMask;

    private void Awake() {
        _spawnPoint = transform;
        TryGetComponent<LineRenderer>(out _lineRenderer);

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.right * _distance);

        _lineRenderer.enabled = false;
    }

    public void Stop() {
        _lineRenderer.enabled = false;
    }

    public void Fire() {
        _lineRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(_spawnPoint.position, _spawnPoint.right, distance: _distance, layerMask: layerMask);
        Debug.DrawRay(_spawnPoint.position, _spawnPoint.right * _distance, color: Color.black);

        _lineRenderer.SetPosition(0, _spawnPoint.position);

        if (hit.collider != null && !hit.collider.isTrigger) {
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.tag == "Object")
                hit.collider.GetComponent<ItemOnScene>().Damage(1f);

            return;
        }

        _lineRenderer.SetPosition(1, new Vector2((_spawnPoint.right * _distance).x + _spawnPoint.position.x, _lineRenderer.GetPosition(0).y));
    }
}
