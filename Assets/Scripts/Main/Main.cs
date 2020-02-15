using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main {
    public class Player {
        public static string currentShip = "Betty";
        public static Dictionary<string, int> inventory = new Dictionary<string, int> { };

        public static int inventoryWeight = 25;

        public static float laser = 1;
        public static float fuel = 1;
        public static float engine = 1;
        public static float guns = 1;

        public static int levelFuel = 0;
        public static int levelLaser = 0;
        public static int levelEngine = 0;
        public static int levelGuns = 0;

        public static void UpgradeStats() {
            fuel = Ships.dictionaryShips[currentShip].dicitionaryUpgrades["Fuel"].dictionaryLevels[levelFuel];
            laser = Ships.dictionaryShips[currentShip].dicitionaryUpgrades["Laser"].dictionaryLevels[levelLaser];
            engine = Ships.dictionaryShips[currentShip].dicitionaryUpgrades["Engine"].dictionaryLevels[levelEngine];
            guns = Ships.dictionaryShips[currentShip].dicitionaryUpgrades["Guns"].dictionaryLevels[levelGuns];
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
            
            foreach (string key in Items.itemsDictionary.Keys)
                ret += inventory.Keys.Contains(key) ? $"{key}: {inventory[key]}\t" : "";

            return ret;
        }
    }

    public class Items {
        public static Dictionary<string, Item> itemsDictionary = new Dictionary<string, Item> {
            { "Stone", new Item("Stone", 5f, "") },
            { "Ferrum", new Item("Ferrum", 10f, "") },
            { "Gold", new Item("Gold", 100f, "") },
            { "Iridium", new Item("Iridium", 325f, "") },
            { "GreenCrystal", new Item("GreenCrystal", 1000f, "") },
            { "RedCrystal", new Item("RedCrystal", 3000f, "") },
            { "WhiteCrystal", new Item("WhiteCrystal", 10000f, "") },
        };

        [SerializeField] public enum LootDropItem {
            Stone = 1,
            Ferrum = 2,
            Gold = 3,
            Iridium = 4,
            GreenCrystal = 5,
            WhiteCrystal = 6,
            RedCrystal = 7,
        };
    }

    public class Item {
        public string name = "";
        public float price = 1.0f;
        
        public string destription = "";

        public Item(string getName, float getPrice, string getDescription) {
            name = getName;
            price = getPrice;

            destription = getDescription;
        }
    }
}
