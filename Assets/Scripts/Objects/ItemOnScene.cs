using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemOnScene : MonoBehaviour {
    [SerializeField] public TypeObject typeObject = TypeObject.Asteroid;
    [SerializeField] public enum TypeObject {
        Asteroid = 1,
        Turret = 2,
        Container = 3,
        AbadonnedShip = 4
    }

    [SerializeField] public Main.Items.LootDropItem lootDropItem = Main.Items.LootDropItem.Stone;

    [SerializeField] public Events.DamageEvent damageEvent = null;
    [SerializeField] public UnityEvent AIEvent = null;

    [SerializeField] public EventSystem eventSystem = null;

    [SerializeField] public GameObject[] objects = null;
    [SerializeField] public string[] objectsName = null;
    [SerializeField] public Dictionary<string, GameObject> objectsDictionary = new Dictionary<string, GameObject> { };

    private void Awake() {
        for (int i = 0; i < objects.Length; i++)
            objectsDictionary[objectsName[i]] = objects[i];
    }

    void Start() {

    }

    
    void Update() {
        if (Input.GetKeyDown(KeyCode.G))
            Damage(100f);
    }

    void Damage(float damage) {
        damageEvent.Invoke(damage, objectsDictionary[lootDropItem.ToString()]);
    }
}
