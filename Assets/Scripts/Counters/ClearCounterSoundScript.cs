using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounterSoundScript : MonoBehaviour {
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private AudioClip dropSFX;
    [SerializeField] protected float volume = 1f;
    protected CounterEventBus counterEventBus;
    protected virtual void Awake() {
        counterEventBus = GetComponentInParent<CounterEventBus>();
    }
    protected virtual void Start() {
        counterEventBus.OnPickupKitchenObject += PlayPickupSFX;
        counterEventBus.OnDropKitchenObject += PlayDropSFX;
    }
    protected virtual void OnDisable() {
        counterEventBus.OnPickupKitchenObject -= PlayPickupSFX;
        counterEventBus.OnDropKitchenObject -= PlayDropSFX;
    }

    private void PlayPickupSFX(object o, EventArgs args) {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position, volume);
    }
    private void PlayDropSFX(object o, EventArgs args) {
        AudioSource.PlayClipAtPoint(dropSFX, transform.position, volume);
    }

}
