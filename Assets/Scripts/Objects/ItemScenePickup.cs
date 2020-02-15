using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScenePickup : MonoBehaviour {
    [SerializeField] public Main.Items.LootDropItem lootDropItem = Main.Items.LootDropItem.Stone;

    public void PickUp() {
        if (Main.Player.inventoryWeight >= Main.Player.WeightInventory()) {
            Main.Player.AddItem(lootDropItem.ToString(), 1);
            gameObject.SetActive(false);
        }
    }
}
