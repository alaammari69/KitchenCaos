using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSoundScript : ClearCounterSoundScript {
    [SerializeField] private AudioSource sizzleSFX;
    private StoveCounterEventBus stoveCounterEventBus;
    protected override void Awake() {
        base.Awake();
        stoveCounterEventBus = counterEventBus as StoveCounterEventBus;
    }
    protected override void Start() {
        base.Start();
        stoveCounterEventBus.OnCookingStarted += PlaySizzleSFX;
        stoveCounterEventBus.OnStoveInterrupted += PauseSizzleSFX;
        stoveCounterEventBus.OnBurningFinished += PauseSizzleSFX;
    }
    protected override void OnDisable() {
        base.OnDisable();
        stoveCounterEventBus.OnCookingStarted -= PlaySizzleSFX;
        stoveCounterEventBus.OnStoveInterrupted -= PauseSizzleSFX;
        stoveCounterEventBus.OnBurningFinished -= PauseSizzleSFX;
    }
    private void PlaySizzleSFX(object o, EventArgs args) {
        sizzleSFX.Play();
    }
    private void PauseSizzleSFX(object o, EventArgs args) {
        sizzleSFX.Pause();
    }
}
