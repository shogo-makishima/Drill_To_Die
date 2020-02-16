using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Main {
    public class Main {
        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);
        public static bool _isDebug = true;
        public static bool _wasLoad = false;
        public static string currentLevel = "Tutorial";

        public static void Start() {
            if (!_wasLoad) {
                StaticScriptableObject staticScriptable = SaveManager.Load();

                Ships.ships = staticScriptable.ships;
                Items.items = staticScriptable.items;
                Levels.levels = staticScriptable.levels;
                Player.moneys = staticScriptable.moneys;
                Player.currentShip = staticScriptable.currentShip;
                Main.currentLevel = staticScriptable.currentLevel;

                _wasLoad = true;
            }

            Player.UpgradeStats();
        }

        public static Ship GetShipWithName(string name) {
            foreach (Ship ship in Ships.ships)
                if (name == ship.name)
                    return ship;

            Debug.Log(Ships.ships.Length);
            return Ships.ships[0];
        }

        public static Upgrade GetUpgrade(string shipName, string upgradeName) {
            Ship ship = GetShipWithName(shipName);

            foreach (Upgrade upgrade in ship.upgrades) {
                if (upgrade.name == upgradeName) {
                    return upgrade;
                }
            }

            return null;
        }

        public static LevelUpgrade GetNextLevelUpgrade(string shipName, string upgradeName) {
            Upgrade currentUpgrade = GetUpgrade(shipName, upgradeName);
            if (currentUpgrade.currentLevel + 1 <= currentUpgrade.levelUpgrades.Length - 1) {
                return currentUpgrade.levelUpgrades[currentUpgrade.currentLevel + 1];
            }

            return null;
        }


        public static Item GetItem(string name) {
            foreach (Item item in Items.items) {
                if (item.name == name) {
                    return item;
                }
            }

            return null;
        }

        public static Level GetLevel(string name) {
            foreach (Level level in Levels.levels) {
                if (level.name == name) {
                    return level;
                }
            }

            return null;
        }

        public static void OpenNextLevel() {
            Level currentLevel = GetLevel(Main.currentLevel);
            currentLevel.finish = true;
            int currentInt = 0;
            
            for (int i = 0; i < Levels.levels.Length; i++) {
                if (Levels.levels[i].name == currentLevel.name) {
                    currentInt = i;
                    break;
                }
            }

            if (currentInt + 1 <= Levels.levels.Length)
                Main.currentLevel = Levels.levels[currentInt + 1].name;
        }
    }

    public class Player {
        public static string currentShip = "Betty";
        public static Dictionary<string, int> inventory = new Dictionary<string, int> { };

        public static int inventoryWeight = 25;

        public static float moneys = 1000f;

        public static float laser = 1f;
        public static float fuel = 1f;
        public static float maxFuel = 1f;
        public static float engine = 1f;

        public static float health = 1f;
        public static float maxHealth = 1f;


        public static void UpgradeStats() {
            Upgrade fuelUpgrade = Main.GetUpgrade(Player.currentShip, "Fuel");
            Player.maxFuel = fuelUpgrade.levelUpgrades[fuelUpgrade.currentLevel].variable;
            Player.fuel = Player.maxFuel;

            Upgrade laserUpgrade = Main.GetUpgrade(Player.currentShip, "Laser");
            Player.laser = laserUpgrade.levelUpgrades[laserUpgrade.currentLevel].variable;

            Upgrade engineUpgrade = Main.GetUpgrade(Player.currentShip, "Engine");
            Player.engine = engineUpgrade.levelUpgrades[engineUpgrade.currentLevel].variable;

            Upgrade cargoUpgrade = Main.GetUpgrade(Player.currentShip, "Cargo");
            Player.inventoryWeight = (int)cargoUpgrade.levelUpgrades[cargoUpgrade.currentLevel].variable;


            Upgrade healthUpgrade = Main.GetUpgrade(Player.currentShip, "Health");
            Player.maxHealth = healthUpgrade.levelUpgrades[healthUpgrade.currentLevel].variable;
            Player.health = Player.maxHealth;
        }

        /*Inventory System!*/
        public static void AddItem(string name_of_item, int count_of_item) {
            if (inventory.ContainsKey(name_of_item))
                inventory[name_of_item] += count_of_item;
            else
                inventory.Add(name_of_item, count_of_item);
        }

        public static void RemoveItem(string name_of_item) {
            if (inventory.ContainsKey(name_of_item))
                inventory[name_of_item] -= 1;
            if (inventory[name_of_item] < 1)
                inventory.Remove(name_of_item);
        }

        public static int WeightInventory() {
            int sum = 1;
            foreach (int value in inventory.Values)
                sum += value;
            return sum;
        }


        public static string InventoryString() {
            string ret = "";
            /*
            foreach (string key in Items.itemsDictionary.Keys)
                ret += inventory.Keys.Contains(key) ? $"{key}: {inventory[key]}\t" : "";
            */
            return ret;
        }
    }

    public class Items {

        public static Item[] items = new Item[] { };

        [SerializeField] public enum LootDropItem {
            Stone = 1,
            Ferrum = 2,
            Gold = 3,
            Iridium = 4,
            GreenCrystal = 5,
            WhiteCrystal = 6,
            RedCrystal = 7,
        Trash, };
    }

    [System.Serializable]
    public class Item {
        public string name = "";
        public float price = 1.0f;
        public GameObject gameObject = null;
        public string destription = "";

        public Item(string getName, float getPrice, string getDescription, GameObject getGameObject) {
            name = getName;
            price = getPrice;

            gameObject = getGameObject;
            destription = getDescription;
        }
    }

    // Levels
    public class Levels {
        public static Level[] levels = new Level[] { };
    }

    [System.Serializable]
    public class Level {
        public string name = "";
        public UnityEngine.SceneManagement.Scene nameScene;
        public bool finish = false;
    }


    // Save Manger
    public class SaveManager {
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);

        [DllImport("__Internal")]
        private static extern void ConsoleLog(string message);

        public static void Save() {
            StaticScriptableObject gameDetails = new StaticScriptableObject();
            gameDetails.items = Items.items;
            gameDetails.ships = Ships.ships;
            gameDetails.moneys = Player.moneys;
            gameDetails.levels = Levels.levels;
            gameDetails.currentLevel = Main.currentLevel;

            PlatformSafeMessage($"SaveFile: {JsonUtility.ToJson(gameDetails)} \nPath: {Application.persistentDataPath}/save.json");

            string dataPath = Path.Combine(Application.persistentDataPath, "save.json");

            if (File.Exists(dataPath)) {
                File.WriteAllText(dataPath, string.Empty);
            } else {
                FileStream myFile = File.Create(dataPath);
                myFile.Close();
            }

            File.WriteAllText(dataPath, JsonUtility.ToJson(gameDetails));

            if (Application.platform == RuntimePlatform.WebGLPlayer) {
                ConsoleLog($"SaveFile: {JsonUtility.ToJson(gameDetails)} \nPath: { Application.persistentDataPath}/save.json");
                SyncFiles();
            }
        }

        public static void DestroySave() {
            string dataPath = Path.Combine(Application.persistentDataPath, "save.json");
            File.Delete(dataPath);
        }

        public static StaticScriptableObject Load() {
            StaticScriptableObject gameDetails = Resources.Load<StaticScriptableObject>("ScriptableObjects\\Static");

            if (!Main._isDebug) {
                string dataPath = Path.Combine(Application.persistentDataPath, "save.json");

                if (File.Exists(dataPath))
                    JsonUtility.FromJsonOverwrite(File.ReadAllText(dataPath), gameDetails);
            }

            return gameDetails;
        }

        public static void PlatformSafeMessage(string message) {
            if (Application.platform == RuntimePlatform.WebGLPlayer) {
                WindowAlert(message);
            } else {
                Debug.Log(message);
            }
        }
    }
}
