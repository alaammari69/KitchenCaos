using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimationScript : MonoBehaviour {
    [SerializeField] private Animator animator;
    private CounterEventBus counterEventBus;
    private const string ON_SLICE_TRIGGER = "OnSlice";
    void Awake() {
        counterEventBus = GetComponent<CounterEventBus>();
    }
    void OnEnable() {
        counterEventBus.OnInteractAlternate += StartSliceAnimation;
    }
    void OnDisable() {
        counterEventBus.OnInteractAlternate -= StartSliceAnimation;
    }
    private void StartSliceAnimation(object o, EventArgs args) {
        animator.SetTrigger(ON_SLICE_TRIGGER);
    }

}
