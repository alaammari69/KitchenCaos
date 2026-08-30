using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEventBus : MonoBehaviour {
    [SerializeField] private bool enableEventsDebug = false;
    public event EventHandler<PlayerController> OnPlayerInRange;
    public void InvokeOnPlayerInRange(PlayerController player) {
        OnPlayerInRange?.Invoke(this, player);
        PrintDebug(player.name + " IN RANGE: " + transform.name);
    }
    public event EventHandler<PlayerController> OnPlayerOutOfRange;
    public void InvokeOnPlayerOutOfRange(PlayerController player) {
        OnPlayerOutOfRange?.Invoke(this, player);
        PrintDebug(player.name + " OUT OF RANGE: " + transform.name);
    }
    public event EventHandler<PlayerController> OnPlayerInteracted;
    public void InvokeOnPlayerInteracted(PlayerController player) {
        OnPlayerInteracted?.Invoke(this, player);
        PrintDebug(player.name + " Interacted With: " + transform.name);
    }
    public event EventHandler OnInteractAlternate;
    public void InvokeOnInteractedAlternate() {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnKitchenObjectSliced;
    public void InvokeOnKitchenObjectSliced() {
        OnKitchenObjectSliced?.Invoke(this, EventArgs.Empty);
    }
    protected void PrintDebug(object message) {
        if (enableEventsDebug) {
            Debug.Log(message);
        }
    }
}
