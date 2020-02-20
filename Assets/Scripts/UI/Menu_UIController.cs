using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Menu_UIController : MonoBehaviour {
    [SerializeField] private Image _shipImage = null;
    [SerializeField] private Button _startButton = null;
    [SerializeField] private Text _moneysText = null;

    [SerializeField] private UpgradePrefab upgradeFuel = null;
    [SerializeField] private UpgradePrefab upgradeEngine = null;
    [SerializeField] private UpgradePrefab upgradeHealth = null;
    [SerializeField] private UpgradePrefab upgradeLaser = null;
    [SerializeField] private UpgradePrefab upgradeCargo = null;

    [SerializeField] private Button _tradeButton = null;
    [SerializeField] private GameObject tradePanel = null;
    [SerializeField] private Transform content = null;
    [SerializeField] private GameObject trade = null;

    [SerializeField] private Dictionary<string, int> lastInventory = new Dictionary<string, int> { };
    [SerializeField] private GameObject inventoryElement = null;
    [SerializeField] private Transform contentInventory = null;


    void Awake() {
        Main.Main.Start();

        EventTrigger trigger = _startButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { StartGame(); });
        trigger.triggers.Add(entry);

        trigger = _tradeButton.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OpenTrade(); });
        trigger.triggers.Add(entry);

        upgradeFuel.SetupButton("Fuel");
        upgradeEngine.SetupButton("Engine");
        upgradeHealth.SetupButton("Health");
        upgradeLaser.SetupButton("Laser");
        upgradeCargo.SetupButton("Cargo");

        GenerateInventory();

        _shipImage.sprite = Main.Main.GetShipWithName(Main.Player.currentShip).UI_Sprite;
    }

    void Update() {
        _moneysText.text = $"{Main.Player.moneys}$";

        // foreach (KeyValuePair<string, int> keyValuePair in Main.Player.inventory) print($"{keyValuePair.Key}");
        print(lastInventory.Keys.Count != Main.Player.inventory.Keys.Count);
        if (lastInventory.Keys.Count != Main.Player.inventory.Keys.Count) {
            GenerateInventory();
            lastInventory = Main.Player.inventory.ToDictionary(entry => entry.Key, entry => entry.Value);
        } else
            lastInventory = Main.Player.inventory.ToDictionary(entry => entry.Key, entry => entry.Value);

        if (Input.GetKeyDown(KeyCode.Delete)) {
            Main.SaveManager.DestroySave();
            Main.Main._wasLoad = false;
            Main.Main.Start();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Main.Main.GetLevel("Menu").name);
        }
    }
    
    void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Main.Main.currentLevel);
    }

    void OpenTrade() {
        tradePanel.SetActive(!tradePanel.activeSelf);

        if (content.childCount == 0)
            SpawnTrader();
    }

    void GenerateInventory() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("InventoryElement");

        for (int i = 0; i < gameObjects.Length; i++) Destroy(gameObjects[i]);

        foreach (KeyValuePair<string, int> keyValuePair in Main.Player.inventory) {
            GameObject element = Instantiate(inventoryElement, contentInventory);
            element.transform.localScale = new Vector3(1, 1, 1);
            element.GetComponent<InventoryElement_UI>().Setup(keyValuePair.Key);
        }
    }

    string RandomTrader() {
        int r = Random.Range(0, Main.Traders.nameTraders.Length);
        print(Main.Traders.nameTraders.Length);
        return Main.Traders.nameTraders[r];
    }

    List<string> GenerateTraders() {
        List<string> tempList = new List<string> { };

        int count = Random.Range(3, 5);
        for (int i = 0; i < count; i++) {
            string name = RandomTrader();
            while (tempList.Contains(name))
                name = RandomTrader();
            tempList.Add(name);
        }

        return tempList;
    }

    void SpawnTrader() {
        Main.Traders.currentTraders = GenerateTraders().ToArray();

        for (int i = 0; i < Main.Traders.currentTraders.Length; i++) {
            GameObject obj = Instantiate(trade, content);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<Trade_UI>().SetupButton(Main.Traders.currentTraders[i]);
        }
    }
}
