using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {
    [SerializeField] private LaserDrill[] _drills = null;

    void Start() {

        // print(ships.ships.Length);
    }

    void Update() {
        if (CnControls.CnInputManager.GetButton("Laser")) {
            foreach (LaserDrill laserDrill in _drills)
                laserDrill.Fire();
        } else {
            foreach (LaserDrill laserDrill in _drills)
                laserDrill.Stop();
        }
    }
}
