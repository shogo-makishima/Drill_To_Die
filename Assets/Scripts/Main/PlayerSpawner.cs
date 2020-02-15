using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] shipsObj = new GameObject[] { };
    [SerializeField] private string[] namesObj = new string[] { };
    [SerializeField] private Dictionary<string, GameObject> shipsDictionary = new Dictionary<string, GameObject> { };

    private void Awake() {
        for (int i = 0; i < namesObj.Length; i++)
            shipsDictionary[namesObj[i]] = shipsObj[i];

        Instantiate(shipsDictionary[Main.Player.currentShip], transform.position, transform.rotation);
    }
}
