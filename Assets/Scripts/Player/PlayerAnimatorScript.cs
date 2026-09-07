using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour {
    [SerializeField] private PlayerEventBus playerEventBus;
    private Animator animator;
    private const string IS_WALKING = "isWalking";

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        playerEventBus.OnPlayerStartedMoving += StartWalkingAnimation;
        playerEventBus.OnPlayerStopedMoving += StartIdleAnimation;
    }
    void OnDisable() {
        playerEventBus.OnPlayerStartedMoving -= StartWalkingAnimation;
        playerEventBus.OnPlayerStopedMoving -= StartIdleAnimation;
    }

    private void StartWalkingAnimation(object o, EventArgs args) {
        animator.SetBool(IS_WALKING, true);
    }
    private void StartIdleAnimation(object o, EventArgs args) {
        animator.SetBool(IS_WALKING, false);
    }

}
