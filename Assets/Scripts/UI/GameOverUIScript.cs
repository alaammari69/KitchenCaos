using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIScript : MonoBehaviour {
    [SerializeField] private GameObject uiPannel;
    [SerializeField] private GameObject winningText;
    [SerializeField] private GameObject losingText;
    void Start() {
        GameManager.Instance.GameManagerEventBus.OnGameOver += ShowGameOverScreen;
        GameManager.Instance.GameManagerEventBus.OnGameWon += ShowWinningText;
        GameManager.Instance.GameManagerEventBus.OnGameLost += ShowLosingText;
        uiPannel.SetActive(false);
    }
    private void ShowWinningText(object o, EventArgs args) {
        winningText.SetActive(true);
    }
    private void ShowLosingText(object o, EventArgs args) {
        losingText.SetActive(true);
    }
    private void ShowGameOverScreen(object o, EventArgs args) {
        uiPannel.SetActive(true);
    }

}
