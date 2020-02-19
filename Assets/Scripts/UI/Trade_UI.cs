using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trade_UI : MonoBehaviour {
    [SerializeField] public Button tradeButton = null;
    [SerializeField] public Text priceText = null;
    [SerializeField] public Text nameText = null;
    [SerializeField] public Text typeText = null;
    [SerializeField] public Image typeImage = null;

    [HideInInspector] private Main.Trader trader = null;

    public void SettingText() {
        priceText.text = $"{trader.price}";
        nameText.text = $"{trader.displayName}";
        typeText.text = $"{trader.buyItem}";

        typeImage.sprite = Main.Main.GetItem($"{trader.buyItem}").UI_Sprite;
    }

    public void SetupButton(string getName) {
        EventTrigger trigger = tradeButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Trade(); });
        trigger.triggers.Add(entry);

        trader = Main.Main.GetTrader(getName);

        SettingText();
    }

    public void Trade() {
        foreach (KeyValuePair<string, int> keyValuePair in Main.Player.inventory) {
            if (keyValuePair.Key == $"{trader.buyItem}") {
                Main.Player.moneys += trader.price * keyValuePair.Value;
                Main.Player.RemoveItem(keyValuePair.Key, keyValuePair.Value);
                Main.SaveManager.Save();
                break;
            }
        }
    }
}
