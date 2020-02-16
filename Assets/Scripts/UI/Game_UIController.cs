using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UIController : MonoBehaviour {
    [SerializeField] private Text fuelText = null;
    [SerializeField] private Text cargoText = null;
    [SerializeField] private Text healthText = null;

    void Start() {

    }

    void Update() {
        fuelText.text = $"{System.Math.Round(Main.Player.fuel, 2)} | {Main.Player.maxFuel}";
        cargoText.text = $"{Main.Player.WeightInventory() - 1} | {Main.Player.inventoryWeight}";
        healthText.text = $"{Main.Player.health} | {Main.Player.maxHealth}";
    }
}
