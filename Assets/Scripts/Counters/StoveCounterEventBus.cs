using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterEventBus : CounterEventBus {
    public event EventHandler OnCookingStarted;
    public void InvokeOnCookingStarted() {
        OnCookingStarted?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnCookingFinished;
    public void InvokeOnCookingFinished() {
        OnCookingFinished?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnBurningStarted;
    public void InvokeOnBurningStarted() {
        OnBurningStarted?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnBurningFinished;
    public void InvokeOnBurningFinished() {
        OnBurningFinished?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnStoveInterrupted;
    public void InvokeOnStoveInterrupted() {
        OnStoveInterrupted?.Invoke(this, EventArgs.Empty);
    }

}
