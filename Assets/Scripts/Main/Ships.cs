using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main {
    public class Ships {
        public static Ship[] ships = new Ship[] { };
    }

    [System.Serializable]
    public class Ship {
        public string name = "";
        public GameObject prefab = null;
        public Sprite UI_Sprite = null;
        public Upgrade[] upgrades = null;
    }

    [System.Serializable]
    public class Upgrade {
        public string name = "";
        public LevelUpgrade[] levelUpgrades = null;
        public int currentLevel = 1;

        public Upgrade(string getName, LevelUpgrade[] getLevelUpgrades, int getCurrentLevel) {
            name = getName;
            levelUpgrades = getLevelUpgrades;
            currentLevel = getCurrentLevel;
        }
    }

    [System.Serializable]
    public class LevelUpgrade {
        public int level = 1;
        public float variable = 1f;
        public float price = 1f;
    }
}
