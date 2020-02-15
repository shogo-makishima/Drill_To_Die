using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main {
    public class Ships {
        public static Dictionary<string, Ship> dictionaryShips = new Dictionary<string, Ship> {
            {
                "Betty",
                new Ship("Betty", new Dictionary<string, Upgrade> {
                    { "Fuel", new Upgrade("Fuel", new Dictionary<int, int> { { 0, 10 }, { 1, 12 }, { 2, 15 }, { 3, 17 }, { 4, 20 }}) },
                    { "Laser", new Upgrade("Laser", new Dictionary<int, int> { { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }}) },
                    { "Engine", new Upgrade("Engine", new Dictionary<int, int> { { 0, 10 }, { 1, 12 }, { 2, 15 }, { 3, 17 }, { 4, 20 }}) },
                    { "Guns", new Upgrade("Guns", new Dictionary<int, int> { { 0, 0 }, { 1, 30 }, { 2, 50 }, { 3, 75 }, { 4, 100 }}) },
                })
            }
        };
    }

    public class Ship {
        public string name = "";

        public Dictionary<string, Upgrade> dicitionaryUpgrades = new Dictionary<string, Upgrade> {
            { "Fuel", new Upgrade("Fuel", new Dictionary<int, int> { { 0, 10 }, { 1, 12 }, { 2, 15 }, { 3, 17 }, { 4, 20 }}) },
        };

        public Ship(string getName, Dictionary<string, Upgrade> upgrades) {
            name = getName;

            dicitionaryUpgrades = upgrades;
        }
    }

    public class Upgrade {
        public string name = "";

        public Dictionary<int, int> dictionaryLevels = new Dictionary<int, int> {};

        public Upgrade(string getName, Dictionary<int, int> levels) {
            name = getName;
            dictionaryLevels = levels;
        }
    }
}
