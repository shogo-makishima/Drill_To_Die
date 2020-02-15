using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStationPrefab : MonoBehaviour {
    [SerializeField] private Vector3 _startPos = new Vector3();
    [SerializeField] private Vector3 _relativePos = new Vector3();

    private void Awake() {
        _startPos = transform.position;
    }

    void Update() {
        transform.position = new Vector3(_startPos.x + _relativePos.x, _startPos.y + _relativePos.y, _startPos.z + _relativePos.z);
    }
}
