using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerEventBus : MonoBehaviour {
    public event EventHandler<Order> OnOrderAddedToQueue;
    public void InvokeOnOrderAddedToQueue(Order order) {
        OnOrderAddedToQueue?.Invoke(this, order);
    }
    public event EventHandler<Order> OnOrderRemovedFromQueue;
    public void InvokeOnOrderRemovedFromQueue(Order order) {
        OnOrderRemovedFromQueue?.Invoke(this, order);
    }
    public event EventHandler OnWrongOrderDelivered;
    public void InvokeOnWrongOrderDelivered() {
        OnWrongOrderDelivered?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnOrderMissed;
    public void InvokeOnOrderMissed() {
        OnOrderMissed?.Invoke(this, EventArgs.Empty);
        Debug.Log("ORDER EXPIRED");
    }
    public event EventHandler OnCorrectOrderDelivered;
    public void InvokeOnCorrectOrderDelivered() {
        OnCorrectOrderDelivered?.Invoke(this, EventArgs.Empty);
    }
}
