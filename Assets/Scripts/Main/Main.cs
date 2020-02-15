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
        public static void Start() {
            StaticScriptableObject staticScriptable = Resources.Load<StaticScriptableObject>("ScriptableObjects\\Static");
            Ships.ships = staticScriptable.ships;
            Items.itemsDictionary = staticScriptable.items;

            foreach (Ship ship in Ships.ships)
                Debug.LogWarning(ship);

            Debug.LogError(JsonUtility.ToJson(staticScriptable));
        }

        public static Ship GetShipWithName(string name) {
            foreach (Ship ship in Ships.ships)
                if (name == ship.name)
                    return ship;

            Debug.Log(Ships.ships.Length);
            return Ships.ships[0];
        }
    }

    public class Player {
        public static string currentShip = "Betty";
        public static Dictionary<string, int> inventory = new Dictionary<string, int> { };

        public static int inventoryWeight = 25;

        public static float moneys = 1000f;

        public static float laser = 1;
        public static float fuel = 1;
        public static float engine = 1;
        public static float guns = 1;

        public static int levelFuel = 0;
        public static int levelLaser = 0;
        public static int levelEngine = 0;
        public static int levelGuns = 0;

        public static void UpgradeStats() {
            
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

        public static Item[] itemsDictionary = new Item[] { };

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

    public class SaveManager {
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);

        public static void Save() {
            StaticScriptableObject gameDetails = new StaticScriptableObject();
            gameDetails.items = Items.itemsDictionary;
            gameDetails.ships = Ships.ships;
            gameDetails.moneys = Player.moneys;

            string dataPath = string.Format($"{Application.persistentDataPath}/save1.json");

            try {
                if (File.Exists(dataPath)) {
                    File.WriteAllText(dataPath, string.Empty);
                } else {
                    File.Create(dataPath);
                }

                File.WriteAllText(dataPath, JsonUtility.ToJson(gameDetails));

                if (Application.platform == RuntimePlatform.WebGLPlayer) {
                    SyncFiles();
                }
            } catch (Exception e) {
                PlatformSafeMessage("Failed to Save: " + e.Message);
            }
        }

        public static StaticScriptableObject Load() {
            StaticScriptableObject gameDetails = null;
            string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);

            try {
                if (File.Exists(dataPath)) {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream fileStream = File.Open(dataPath, FileMode.Open);

                    gameDetails = (StaticScriptableObject)binaryFormatter.Deserialize(fileStream);
                    fileStream.Close();
                }
            } catch (Exception e) {
                PlatformSafeMessage("Failed to Load: " + e.Message);
            }

            return gameDetails;
        }

        private static void PlatformSafeMessage(string message) {
            if (Application.platform == RuntimePlatform.WebGLPlayer) {
                WindowAlert(message);
            } else {
                Debug.Log(message);
            }
        }
    }
}
