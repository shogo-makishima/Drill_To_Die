using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradePrefab : MonoBehaviour {
    [SerializeField] public Button upgardeButton = null;
    [SerializeField] public Text priceText = null;
    [SerializeField] public Text countUpgrades = null;

    [HideInInspector] private Main.Upgrade upgrade = null;
    [HideInInspector] private Main.LevelUpgrade levelUpgrade = null;
    [HideInInspector] private string nameUpgrade = "";

    public void SettingText() {
        upgrade = Main.Main.GetUpgrade(Main.Player.currentShip, nameUpgrade);
        levelUpgrade = Main.Main.GetNextLevelUpgrade(Main.Player.currentShip, nameUpgrade);

        countUpgrades.text = $"{upgrade.currentLevel + 1}/{upgrade.levelUpgrades.Length}";
        if (levelUpgrade == null) {
            priceText.text = $"MAX";
        } else {
            priceText.text = $"{levelUpgrade.price}";
        }
    }

    public void SetupButton(string getName) {
        EventTrigger trigger = upgardeButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Upgrade(); });
        trigger.triggers.Add(entry);
        nameUpgrade = getName;
        SettingText();
    }

    public void Upgrade() {
        // print($"{nameUpgrade}: {upgrade.currentLevel}, {upgrade.levelUpgrades.Length - 1};");
        if (levelUpgrade != null && Main.Player.moneys >= levelUpgrade.price) {
            upgrade.currentLevel += 1;
            Main.Player.moneys -= levelUpgrade.price;
            Main.Player.UpgradeStats();
            SettingText();
            Main.SaveManager.Save();
        }
    }
}
