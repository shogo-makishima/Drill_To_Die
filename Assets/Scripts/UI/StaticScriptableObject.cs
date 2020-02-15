using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StaticScriptableObject", order = 1)]
public class StaticScriptableObject : ScriptableObject {
    public Main.Ship[] ships = new Main.Ship[] { };

    public Main.Item[] items = new Main.Item[] { };

    public Main.Level[] levels = new Main.Level[] { };

    public float moneys = 0f;
    public string currentShip = "Betty";

    public string currentLevel = "Tutorial";
}