using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingParicles;
    private StoveCounterEventBus stoveCounterEventBus;
    void Awake() {
        stoveCounterEventBus = GetComponentInParent<StoveCounterEventBus>();
    }
    void OnEnable() {
        stoveCounterEventBus.OnCookingStarted += EnableStoveOnVisual;
        stoveCounterEventBus.OnCookingStarted += EnableSizzlingParticles;
        stoveCounterEventBus.OnStoveInterrupted += DisableStoveOnVisual;
        stoveCounterEventBus.OnStoveInterrupted += DisableSizzlingParticles;
        stoveCounterEventBus.OnBurningFinished += DisableStoveOnVisual;
        stoveCounterEventBus.OnCookingFinished += DisableSizzlingParticles;
    }
    void OnDisable() {
        stoveCounterEventBus.OnCookingStarted -= EnableStoveOnVisual;
        stoveCounterEventBus.OnCookingStarted -= EnableSizzlingParticles;
        stoveCounterEventBus.OnStoveInterrupted -= DisableStoveOnVisual;
        stoveCounterEventBus.OnStoveInterrupted -= DisableSizzlingParticles;
        stoveCounterEventBus.OnBurningFinished -= DisableStoveOnVisual;
        stoveCounterEventBus.OnCookingFinished -= DisableSizzlingParticles;
    }

    private void EnableStoveOnVisual(object o, EventArgs args) {
        stoveOnVisual.SetActive(true);
    }
    private void DisableStoveOnVisual(object o, EventArgs args) {
        stoveOnVisual.SetActive(false);
    }
    private void EnableSizzlingParticles(object o, EventArgs args) {
        sizzlingParicles.SetActive(true);
    }
    private void DisableSizzlingParticles(object o, EventArgs args) {
        sizzlingParicles.SetActive(false);
    }
}
