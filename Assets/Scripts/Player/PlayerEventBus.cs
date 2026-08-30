using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventBus : MonoBehaviour {
    [SerializeField] private bool enableEventsDebug = false;
    public event EventHandler OnPlayerStartedMoving;
    public void InvokeOnPlayerStartedMoving() {
        OnPlayerStartedMoving?.Invoke(this, EventArgs.Empty);
        PrintDebug("Player started moving");
    }
    public event EventHandler OnPlayerStopedMoving;
    public void InvokeOnPlayerStopedMoving() {
        OnPlayerStopedMoving?.Invoke(this, EventArgs.Empty);
        PrintDebug("Player stoped moving");
    }
    public event EventHandler OnPlayerInteract;
    public void InvokeOnPlayerInteract(object o, EventArgs args) {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
        PrintDebug("Player trying to Interact !!!");
    }
    public event EventHandler OnPlayerInteractAlternate;
    public void InvokeOnPlayerInteractAlternate(object o, EventArgs args) {
        OnPlayerInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void PrintDebug(object message) {
        if (enableEventsDebug) {
            Debug.Log(message);
        }
    }
}
