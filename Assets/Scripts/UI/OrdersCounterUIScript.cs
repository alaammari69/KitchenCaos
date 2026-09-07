using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrdersCounterUIScript : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI maxAllowedMisses;
    [SerializeField] private TextMeshProUGUI currentNbrOfFails;
    [SerializeField] private TextMeshProUGUI minOrdersToWin;
    [SerializeField] private TextMeshProUGUI currentCorrectOrdersNbr;
    private GameManager gameManager;
    void Awake() {
        gameManager = GameManager.Instance;
    }
    void Start() {
        maxAllowedMisses.text = gameManager.maxAllowedMisses.ToString();
        minOrdersToWin.text = gameManager.MinOrdersToWin.ToString();
    }
    void LateUpdate() {
        currentNbrOfFails.text = gameManager.CurrentNbrOfFails.ToString();
        currentCorrectOrdersNbr.text = gameManager.CurrentCorrectOrdersNbr.ToString();
    }
}
