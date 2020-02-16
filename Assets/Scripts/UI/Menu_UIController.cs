using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_UIController : MonoBehaviour {
    [SerializeField] private Image _shipImage = null;
    [SerializeField] private Button _startButton = null;
    [SerializeField] private Text _moneysText = null;

    [SerializeField] private UpgradePrefab upgradeFuel = null;
    [SerializeField] private UpgradePrefab upgradeEngine = null;
    [SerializeField] private UpgradePrefab upgradeHealth = null;
    [SerializeField] private UpgradePrefab upgradeLaser = null;
    [SerializeField] private UpgradePrefab upgradeCargo = null;


    void Awake() {
        Main.Main.Start();

        // _startButton.onClick.AddListener(delegate () { StartGame(); });

        EventTrigger trigger = _startButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { StartGame(); });
        trigger.triggers.Add(entry);

        upgradeFuel.SetupButton("Fuel");
        upgradeEngine.SetupButton("Engine");
        upgradeHealth.SetupButton("Health");
        upgradeLaser.SetupButton("Laser");
        upgradeCargo.SetupButton("Cargo");
    }

    void Update() {
        _moneysText.text = $"{Main.Player.moneys}$";
    }
    
    void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Main.Main.currentLevel);
    }
}
