using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement_UI : MonoBehaviour {
    [SerializeField] private Image itemImage = null;
    [SerializeField] private Text countText = null;

    public void Setup(string name) {
        Main.Item item = Main.Main.GetItem(name);

        itemImage.sprite = item.UI_Sprite;
        countText.text = $"{Main.Player.inventory[name]}";
    }
}
