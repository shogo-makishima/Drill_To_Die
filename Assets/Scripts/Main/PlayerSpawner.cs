using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Awake() {
        Main.Main.Start();
        Instantiate(Main.Main.GetShipWithName(Main.Player.currentShip).prefab, transform.position, transform.rotation);
    }
}
