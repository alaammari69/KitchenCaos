using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimationScript : MonoBehaviour {
    [SerializeField] private Animator animator;
    private CounterEventBus counterEventBus;
    private const string ON_PLAYER_INTERACTION = "OnPlayerInteraction";
    void Awake() {
        counterEventBus = GetComponent<CounterEventBus>();
    }
    void OnEnable() {
        counterEventBus.OnPlayerInteracted += OpenDoorAnimation;
    }
    void OnDisable() {
        counterEventBus.OnPlayerInteracted -= OpenDoorAnimation;
    }
    private void OpenDoorAnimation(object o, PlayerController player) {
        animator.SetTrigger(ON_PLAYER_INTERACTION);
    }
}
