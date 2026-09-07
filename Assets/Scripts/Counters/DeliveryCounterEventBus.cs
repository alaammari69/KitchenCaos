using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterEventBus : CounterEventBus {
    public event EventHandler OnSuccessOrder;
    public void InvokeOnSuccessOrder() {
        OnSuccessOrder?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnFailOrder;
    public void InvokeOnFailOrder() {
        OnFailOrder?.Invoke(this, EventArgs.Empty);
    }
}
