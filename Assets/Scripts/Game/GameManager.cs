using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] private RecipeManagerController recipeManagerController;
    [SerializeField] public int maxAllowedMisses = 5;
    public int MaxAllowedMisses => maxAllowedMisses;
    [SerializeField] private int minOrdersToWin = 10;
    public int MinOrdersToWin => minOrdersToWin;
    public GameManagerEventBus GameManagerEventBus { get; private set; }
    private int currentNbrOfFails = 0;
    public int CurrentNbrOfFails => currentNbrOfFails;
    private int currentCorrectOrdersNbr = 0;
    public int CurrentCorrectOrdersNbr => currentCorrectOrdersNbr;
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameManagerEventBus = GetComponent<GameManagerEventBus>();
    }
    void Start() {
        GameManagerEventBus.InvokeOnGameStart();

        recipeManagerController.RecipeManagerEventBus.OnOrderMissed += AddMissedOrderToCount;
        recipeManagerController.RecipeManagerEventBus.OnOrderRemovedFromQueue += AddSuccessfullOrderToCount;
    }
    void OnDisable() {
        recipeManagerController.RecipeManagerEventBus.OnOrderMissed -= AddMissedOrderToCount;
        recipeManagerController.RecipeManagerEventBus.OnOrderRemovedFromQueue -= AddSuccessfullOrderToCount;
    }

    private void AddMissedOrderToCount(object o, EventArgs args) {
        currentNbrOfFails--;
        if (currentNbrOfFails == maxAllowedMisses) {
            GameManagerEventBus.InvokeOnGameOver();
            GameManagerEventBus.InvokeOnGameLost();
        }
    }
    private void AddSuccessfullOrderToCount(object o, Order order) {
        currentCorrectOrdersNbr++;
        if (currentCorrectOrdersNbr == minOrdersToWin) {
            GameManagerEventBus.InvokeOnGameOver();
            GameManagerEventBus.InvokeOnGameWon();
        }
    }
}
