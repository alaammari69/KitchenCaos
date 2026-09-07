using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterSoundScript : MonoBehaviour {
    [SerializeField] private AudioSource trashSFX;
    [SerializeField] private float volume = 1f;
    private CounterEventBus counterEventBus;
    void Awake() {
        counterEventBus = GetComponentInParent<CounterEventBus>();
    }
    void Start() {
        counterEventBus.OnPlayerInteracted += PlayTrashSFX;
    }
    void OnDisable() {
        counterEventBus.OnPlayerInteracted -= PlayTrashSFX;
    }
    private void PlayTrashSFX(object o, PlayerController playerController) {
        AudioSource.PlayClipAtPoint(trashSFX.clip, transform.position, volume);
    }
}
