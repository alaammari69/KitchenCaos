using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedEventRelay : MonoBehaviour {
    private CounterEventBus counterEventBus;
    void Awake() {
        counterEventBus = GetComponentInParent<CounterEventBus>();
    }
    public void SliceAnimationFinished() {
        counterEventBus.InvokeOnKitchenObjectSliced();
    }
}
