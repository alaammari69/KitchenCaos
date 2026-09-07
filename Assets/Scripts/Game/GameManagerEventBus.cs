using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEventBus : MonoBehaviour {
    public event EventHandler OnMaxAllowedMissesReached;
    public void InvokeOnMaxAllowedMissesReached() {
        OnMaxAllowedMissesReached?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnGameStart;
    public void InvokeOnGameStart() {
        OnGameStart?.Invoke(this, EventArgs.Empty);
        Debug.Log("********************************** GAME STARTED **********************************");
    }
    public event EventHandler OnGameOver;
    public void InvokeOnGameOver() {
        OnGameOver?.Invoke(this, EventArgs.Empty);
        Debug.Log("********************************** GAME OVER **********************************");
    }
    public event EventHandler OnGameRestart;
    public void InvokeOnGameRestart() {
        OnGameRestart?.Invoke(this, EventArgs.Empty);
        Debug.Log("********************************** GAME RESTART **********************************");
    }
    public event EventHandler OnGameLost;
    public void InvokeOnGameLost() {
        OnGameLost?.Invoke(this, EventArgs.Empty);
        Debug.Log("********************************** GAME LOST **********************************");
    }
    public event EventHandler OnGameWon;
    public void InvokeOnGameWon() {
        OnGameWon?.Invoke(this, EventArgs.Empty);
        Debug.Log("********************************** GAME WON ! **********************************");
    }
}
