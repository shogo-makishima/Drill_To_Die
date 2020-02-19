using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour {
    void Start() {

    }

    void Update() {
        if (Main.Player.fuel <= 0 | Main.Player.health <= 0) {
            EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.isTrigger && collider.tag == "Player") {
            Main.Main.OpenNextLevel();
            EndGame();
        }
    }

    void EndGame() {
        /*foreach (KeyValuePair<string, int> keyValuePair in Main.Player.inventory) {
            Main.Player.moneys += Main.Main.GetItem(keyValuePair.Key).price * keyValuePair.Value;
        }*/

        // Main.Player.inventory.Clear();
        Main.SaveManager.Save();
        LoadMenu();
    }

    void LoadMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Main.Main.GetLevel("Menu").name);
    }
}
