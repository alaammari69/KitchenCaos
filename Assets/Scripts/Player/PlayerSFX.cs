using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour {
    [SerializeField] private GameObject walkingSoundEffect;
    private PlayerEventBus playerEventBus;
    void Awake() {
        playerEventBus = GetComponentInParent<PlayerEventBus>();
    }
    void OnEnable() {
        playerEventBus.OnPlayerStartedMoving += EnableWalkingSoundEffect;
        playerEventBus.OnPlayerStopedMoving += DisableWalkingSoundEffect;
    }
    void OnDisable() {
        playerEventBus.OnPlayerStartedMoving -= EnableWalkingSoundEffect;
        playerEventBus.OnPlayerStopedMoving -= DisableWalkingSoundEffect;
    }
    private void EnableWalkingSoundEffect(object o, EventArgs args) {
        walkingSoundEffect.SetActive(true);
    }
    private void DisableWalkingSoundEffect(object o, EventArgs args) {
        walkingSoundEffect.SetActive(false);
    }
}
