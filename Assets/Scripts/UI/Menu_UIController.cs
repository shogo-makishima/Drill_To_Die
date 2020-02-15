using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_UIController : MonoBehaviour {
    [SerializeField] private Image _shipImage = null;
    [SerializeField] private Button _startButton = null;
    [SerializeField] private Text _moneysText = null;

    void Awake() {
        _startButton.onClick.AddListener(delegate () { StartGame(); });
    }

    void Update() {
        _moneysText.text = $"{Main.Player.moneys}$";
    }
    
    void StartGame() {
        Debug.Log("StartGame");
    }
}
